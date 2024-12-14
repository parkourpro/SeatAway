using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSeatMap : MonoBehaviour
{
    private LevelData levelData;

    private GridManager gridManager;
    private GridCell[,] gridCells;

    public PlaceObstacleOnGridManager placeObstacleOnGridManager;

    IEnumerator Start()
    {
        gridManager = GridManager.Instance;
        yield return new WaitUntil(() => gridManager.doneGrid != "");
        levelData = gridManager.levelData;
        gridCells = gridManager.gridCells;
        SetUpData();
    }


    //void SetUpData()
    //{
    //    Seat seat1 = new DoubleSeat(Color.blue, Direction.Down);
    //    gridManager.PlaceSeatOnGrid(seat1, 0, 1);
    //    Seat seat2 = new DoubleSeat(Color.blue, Direction.Up);
    //    gridManager.PlaceSeatOnGrid(seat2, 5, 3);
    //    seat2.movable = false;
    //    Seat seat3 = new SingleSeat(Color.yellow, Direction.Right);
    //    gridManager.PlaceSeatOnGrid(seat3, 0, 3);
    //    Seat seat4 = new TripleSeat(Color.green, Direction.Left);
    //    gridManager.PlaceSeatOnGrid(seat4, 3, 1);
    //    Seat seat5 = new TripleSeat(Color.red, Direction.Up);
    //    gridManager.PlaceSeatOnGrid(seat5, 2, 3);
    //    Obstacle obstacle1 = new TrafficCone();
    //    placeObstacleOnGridManager.PlaceObstacleOnGrid(obstacle1, 1, 0);
    //    Obstacle obstacle2 = new FireHose();
    //    placeObstacleOnGridManager.PlaceObstacleOnGrid(obstacle2, 4, 1);
    //}
    void SetUpData()
    {
        foreach (SeatData seatData in levelData.seats)
        {
            Seat seat = null;

            // Tạo ghế theo loại seatType
            switch (seatData.seatType)
            {
                case SeatType.Single:
                    seat = new SingleSeat(seatData.seatColor, seatData.direction);
                    break;
                case SeatType.Double:
                    seat = new DoubleSeat(seatData.seatColor, seatData.direction);
                    break;
                case SeatType.Triple:
                    seat = new TripleSeat(seatData.seatColor, seatData.direction);
                    break;
            }

            // Kiểm tra xem seat có được tạo thành công không
            if (seat != null)
            {
                gridManager.PlaceSeatOnGrid(seat, seatData.x, seatData.y);
                seat.movable = seat.seatColor != EnumColor.Grey;
            }
        }

        foreach (ObstacleData obstacleData in levelData.obstacles)
        {
            Obstacle obstacle = new Obstacle(obstacleData.type);
            placeObstacleOnGridManager.PlaceObstacleOnGrid(obstacle, obstacleData.x, obstacleData.y);
        }
    }


}
