using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellLog : MonoBehaviour
{
    public GridManager gridManager;
    private GridCell[,] gridCells;
    public BFS_FindPath BFS_FindPath;

    //public int row, col;
    public int r1, c1, r2, c2;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(() => GridManager.Instance.doneGrid != "");
        gridCells = gridManager.gridCells;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool a = BFS_FindPath.CheckDirrecion(gridCells[r1, c1], gridCells[r2, c2]);
            Debug.Log(a);
        }
    }
    //void PrintIndex()
    //{
    //    Seat seat = (Seat)gridCells[row, col].objectOncell;
    //    if (seat != null)
    //    {
    //        //GridCell gridCell1 = gridCells[seat.currentRow, seat.currentCol];
    //        int index = Mathf.Abs(seat.currentRow - gridCells[row, col].row) + Mathf.Abs(seat.currentCol - gridCells[row, col].col);
    //        Debug.Log(index);
    //    }
    //}
}
