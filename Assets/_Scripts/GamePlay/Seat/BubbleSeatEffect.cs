using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BubbleSeatEffect : MonoBehaviour
{
    private int numOfSeats;
    IEnumerator Start()
    {
        yield return new WaitUntil(() => GridManager.Instance.doneLevelData != "");
        LevelData levelData = GridManager.Instance.levelData;
        numOfSeats = levelData.seats.Count;
        //Debug.Log(2/ numOfSeats);
        //Debug.Log(2f / numOfSeats);

    }
    public IEnumerator BubbleSeat()
    {
        GridCell[,] gridCells = GridManager.Instance.gridCells;
        int row = GridManager.Instance.GetGridRow();
        int col = GridManager.Instance.GetGridCol();
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (gridCells[i, j].isOccupied)
                {
                    GameObject associatedObject = gridCells[i, j].objectOncell.associatedObject;
                    associatedObject.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), .7f) // Phóng to hơn một chút
                             .OnComplete(() =>
                                 associatedObject.transform.DOScale(Vector3.one, .2f)); // Thu về kích thước bình thường
                    yield return new WaitForSeconds(1f/numOfSeats);
                }
            }
        }
        yield return new WaitForSeconds(1f);
    }
}
