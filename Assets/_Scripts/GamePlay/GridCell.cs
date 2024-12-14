using System;
using UnityEngine;
[Serializable]
public class GridCell
{
    public int row;
    public int col;
    public bool isOccupied = false ; //có vật nào ở ô này không,
                            //ghế loại 3 mà tâm ở ô khác
                            //mà ghế thứ 2 hoặc 3 nằm
                            //trên ô này thì vẫn tính
    public GameObject tile; //viên gạch trắng hoặc
                            //đen, dùng để tham chiếu đến object trong scene
    public ObjectOncell objectOncell;//vật đang nằm trên cell,
                                     //có thể là ghế hoặc vật cản
    public GridCell parrentNode; //for find path in BFS
    public GridCell(GameObject tile, int row, int col)
    {
        this.tile = tile;
        this.row = row;
        this.col = col;
    }
}
