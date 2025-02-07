using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomerController : MonoBehaviour
{
    private GridManager gridManager;
    public BusController busController;
    private int doorRow, doorCol;


    public static int totalCus;
    private Dictionary<string, int> checkMoveSeat = new();
    //private Dictionary<string, bool> initialMovableSeat = new();


    private CustomerMovementSubject movementSubject;

    IEnumerator Start()
    {
        gridManager = GridManager.Instance;
        yield return new WaitUntil(() => gridManager.doneGrid != "");
        yield return new WaitUntil(() => CustomerManager.doneCustomer != "");
        totalCus = CustomerManager.Instance.totalCustomer;
        doorRow = 0;
        doorCol = gridManager.GetGridCol() - 1;

        movementSubject = CustomerMovementSubject.Instance;
    }



    public IEnumerator CustomerGoToSeat()
    {
        while (true)
        {
            Customer customer;
            if (CustomerManager.Instance.customerList.Count != 0)
            {
                customer = CustomerManager.Instance.customerList.Peek();
            }
            else break;
            Stack<GridCell> path = BFS_FindPath.Instance.BFS(gridManager.gridCells[doorRow,
                doorCol], customer.customerColor);

            if (path == null)
            {
                break;
            }
            else if (path.Count > 0)
            {
                if (CustomerManager.Instance.customerList.Count == 1)
                {
                    TimeController.stopTimeDelegate?.Invoke();
                }
                Seat foundSeat = BFS_FindPath.Instance.GetFoundSeat();
                CustomerManager.Instance.customerList.Dequeue();

                string seatName = foundSeat.associatedObject.name;
                //if (!initialMovableSeat.ContainsKey(seatName))
                //{
                //    initialMovableSeat.Add(seatName, foundSeat.movable);
                //}
                if (checkMoveSeat.ContainsKey(seatName))
                {
                    checkMoveSeat[seatName]++;
                }
                else
                {
                    int startCus = 1;
                    checkMoveSeat.Add(seatName, startCus);
                }
                foundSeat.movable = false;
                StartCoroutine(MoveToSeat(customer, path, foundSeat, checkMoveSeat[seatName]));
            }
        }
        CustomerManager.Instance.LineUpCustomer();
        yield return null;

    }

    private IEnumerator MoveToSeat(Customer customer, Stack<GridCell> path, Seat foundSeat, int key)
    {
        // Thông báo bắt đầu di chuyển
        movementSubject.IncrementMovingCustomers();

        yield return StartCoroutine(CustomerMovement.Move(
                customer.associatedObject, CustomerManager.Instance.GetFirstInLinePosition()));

        while (path.Count > 1)
        {

            GridCell cell = path.Pop();
            yield return StartCoroutine(CustomerMovement.Move(
                customer.associatedObject, cell.tile.transform.position
                + gridManager.GetThickTilePrefab() / 2));
        }

        // The final position where the customer sits
        GridCell seatPosition = path.Pop();

        yield return StartCoroutine(CustomerMovement.Sit(
                customer.associatedObject, seatPosition.tile.transform.position
                + gridManager.GetThickTilePrefab() / 2, seatPosition));

        // Thông báo kết thúc di chuyển
        movementSubject.DecrementMovingCustomers();

        totalCus--;
        //Debug.Log(totalCus);
        string seatName = foundSeat.associatedObject.name;
        if (checkMoveSeat[seatName] == key)
        {
            //
            //foundSeat.movable = true;
            //foundSeat.movable = initialMovableSeat[seatName];
            foundSeat.movable = foundSeat.seatColor != EnumColor.Grey;
        }
        else
        {
            foundSeat.movable = false;
        }
        //if (CustomerManager.Instance.customerList.Count == 0)
        if(totalCus == 0)
        {
            StartCoroutine(Win());
        }
    }


    public IEnumerator Win()
    {
        yield return StartCoroutine(busController.CloseDoor());
        CanvasController.onWinDo?.Invoke();
        int unlockLevel = GridManager.Instance.GetLevel();
        PlayerPrefs.SetInt("UnlockLevel", unlockLevel + 1);
    }

}
