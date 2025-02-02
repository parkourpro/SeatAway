using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS_FindPath : Singleton<BFS_FindPath>
{
    private GridManager gridManager;
    private CustomerManager customerManager;
    //public CustomerManager customerManager;
    //private Queue<Customer> customerList;
    private Seat foundSeat = null;
    IEnumerator Start()
    {
        gridManager = GridManager.Instance;
        yield return new WaitUntil(() => gridManager.doneGrid != "");
        yield return new WaitUntil(() => CustomerManager.doneCustomer != "");
        customerManager = CustomerManager.Instance;
        //customerList = customerManager.customerList;

    }



    public Stack<GridCell> BFS(GridCell doorCell, EnumColor customerColor)
    {

        Queue<GridCell> cellsQueue = new ();
        Stack<GridCell> path = new ();

        // Initialize all cells' parent nodes to null
        for (int i = 0; i < gridManager.GetGridRow(); i++)
        {
            for (int j = 0; j < gridManager.GetGridCol(); j++)
            {
                gridManager.gridCells[i, j].parrentNode = null;
            }
        }

        bool[,] visitedNode = new bool[gridManager.GetGridRow(), gridManager.GetGridCol()];
        int[] d1 = { 1, 0, -1, 0 };
        int[] d2 = { 0, 1, 0, -1 };

        visitedNode[doorCell.row, doorCell.col] = true;

        if (!doorCell.isOccupied)
        {
            cellsQueue.Enqueue(doorCell);
        }
        else if (doorCell.objectOncell is Seat seat 
            && (seat.seatColor == customerColor
            || (seat.seatColor == EnumColor.Grey && customerColor == EnumColor.Blue))
            && CheckEmptySeat(doorCell))
        {
            //Debug.Log(1);
            path.Push(doorCell);
            SetSeatForCustomer(seat, doorCell);
            foundSeat = seat;
            return path;
        }
        else
        {
            return null;
        }

        while (cellsQueue.Count > 0)
        {
            GridCell node = cellsQueue.Dequeue();

            for (int i = 0; i < 4; i++)
            {
                int adjacentRow = node.row + d1[i];
                int adjacentCol = node.col + d2[i];

                // Check if the cell is within bounds
                if (adjacentCol < 0 || adjacentCol >= gridManager.GetGridCol() || adjacentRow < 0 || adjacentRow >= gridManager.GetGridRow())
                    continue;

                // If already visited, skip this cell
                if (visitedNode[adjacentRow, adjacentCol])
                    continue;
                //nếu chưa được thăm
                GridCell adjacentCell = gridManager.gridCells[adjacentRow, adjacentCol];
                adjacentCell.parrentNode = node;
                //if has an obstacle
                if(adjacentCell.objectOncell is Obstacle)
                {
                    //Debug.Log($"found obstacle at {adjacentRow}{adjacentCol}");
                    visitedNode[adjacentRow, adjacentCol] = true;
                    continue;
                }

                // Check if adjacent cell has an empty seat of the correct color
                if (adjacentCell.objectOncell is Seat adjSeat
                    &&( adjSeat.seatColor == customerColor
                    ||(adjSeat.seatColor == EnumColor.Grey && customerColor == EnumColor.Blue)))
                {
                    bool empty = CheckEmptySeat(adjacentCell);
                    //if (empty)
                    //{
                    //    Debug.Log($"seat at {adjacentRow}{adjacentCol} is empty");
                    //}
                    //bool direction = CheckDirrecion(adjacentCell, node);
                    //if (!direction)
                    //{
                    //    Debug.Log($"can go to seat at {adjacentRow}{adjacentCol}");
                    //}
                    if (empty && CheckDirrecion(adjacentCell, node))
                    {
                        // Trace path back to start
                        GridCell pathNode = adjacentCell;
                        while (pathNode != null)
                        {
                            path.Push(pathNode);
                            pathNode = pathNode.parrentNode;
                        }

                        //thêm khách vào ghế
                        SetSeatForCustomer(adjSeat, adjacentCell);
                        foundSeat = adjSeat;
                        return path;
                    }
                    continue;
                }

                // Enqueue cell if it is empty
                if (!adjacentCell.isOccupied)
                {
                    visitedNode[adjacentRow, adjacentCol] = true;
                    cellsQueue.Enqueue(adjacentCell);
                }

            }
        }
        //for (int i = 0; i < gridManager.rows; i++)
        //{
        //    for (int j = 0; j < gridManager.cols; j++)
        //    {
        //        Debug.Log($"r{i}c{j}: " + visitedNode[i, j]);
        //    }
        //}
        // Return null if no path was found
        return null;
    }

    public void SetSeatForCustomer(Seat seat, GridCell cell)
    {
        int index = Mathf.Abs(seat.currentRow - cell.row) +
                            Mathf.Abs(seat.currentCol - cell.col);
        seat.AddCustomerAtIndex(customerManager.customerList.Peek(), index);
        //đặt khách là con của ghế
        customerManager.customerList.Peek().associatedObject.transform.SetParent(seat.associatedObject.transform);
    }


    public bool CheckEmptySeat(GridCell gridCell) //trả về true nếu empty
    {
        Seat seat = (Seat)gridCell.objectOncell;
        if (seat != null)
        {
            int index = Mathf.Abs(seat.currentRow - gridCell.row) + Mathf.Abs(seat.currentCol - gridCell.col);
            if(seat.occupiedCustomers[index] == null)
            {
                //Debug.Log($"ghế {gridCell.row}{gridCell.col} chưa có người ngồi");
                //seat.AddCustomerAtIndex(customerList.Peek(), index);
                return true;
            }
            else
            {
                //Debug.Log($"ghế {gridCell.row}{gridCell.col} đã có người ngồi");
                return false;
            }
            //return seat.occupiedCustomers[index] == null;
        }
        return true;
    }

    public bool CheckDirrecion(GridCell cellSeat, GridCell cellCustomer)
    {
        Seat seat = (Seat)cellSeat.objectOncell;
        Direction direction = seat.direction;
        switch (direction)
        {
            case Direction.Up:
                return cellSeat.row + 1 != cellCustomer.row;
            case Direction.Down:
                return cellSeat.row - 1 != cellCustomer.row;
            case Direction.Left:
                return cellSeat.col + 1 != cellCustomer.col;
            case Direction.Right:
                return cellSeat.col - 1 != cellCustomer.col;
            default:
                throw new ArgumentException("Can not check dirrection");
        }
    }

    public Seat GetFoundSeat()
    {
        return foundSeat;
    }
}
