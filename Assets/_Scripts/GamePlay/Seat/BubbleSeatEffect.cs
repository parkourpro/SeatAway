using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BubbleSeatEffect : MonoBehaviour
{
    //public GameObject seat;
    //void Start()
    //{
    //    // Bắt đầu với Scale nhỏ
    //    seat.transform.localScale = Vector3.zero;

    //    // Tạo hiệu ứng bọt nổi
    //    seat.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), .7f) // Phóng to hơn một chút
    //             .OnComplete(() =>
    //                 seat.transform.DOScale(Vector3.one, .3f)); // Thu về kích thước bình thường
    //    Debug.Log(1);
    //}

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
                    yield return new WaitForSeconds(0.15f);
                }
            }
        }
        yield return new WaitForSeconds(1f);
    }
}
