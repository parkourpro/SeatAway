using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObstacleOnGridManager : MonoBehaviour
{
    private GridManager gridManager;
    public GameObject trafficConePrefab;
    public GameObject fireFosePrefab;
    public GameObject bus;
    private Vector3 thickTilePrefab;
    private int numObstacle = 1;
    // Update is called once per frame

    private void Start()
    {
        gridManager = GridManager.Instance;
        thickTilePrefab = new Vector3(0, gridManager.whiteTilePrefab.transform.localScale.y, 0);
    }
    void Update()
    {
        
    }

    public void PlaceObstacleOnGrid(Obstacle obstacle, int row, int col)
    {
        if (gridManager.gridCells[row, col].isOccupied == true)
        {
            Debug.Log("đã có vật trên ô này, không thể đặt obstacle");
            return;
        }
        //xử lý hiển thị
        GameObject obstaclePrefab;
        obstaclePrefab = GetPrefab(obstacle);
        Vector3 cellPosition = gridManager.gridCells[row, col].tile.transform.position;
        cellPosition += thickTilePrefab/2;
        Quaternion obstacleRotation = Quaternion.identity;
        GameObject newObstacleObject = Instantiate(obstaclePrefab, cellPosition, obstacleRotation, bus.transform);
        newObstacleObject.name = $"obstacle{numObstacle}";
        numObstacle++;
        // đặt để không nhìn thấy seat tạo hiệu ứng nổi bọt:
        newObstacleObject.transform.localScale = Vector3.zero;
        //xử lí dữ liệu
        obstacle.associatedObject = newObstacleObject;
        obstacle.currentCol = col;
        obstacle.currentRow = row;
        gridManager.gridCells[row, col].isOccupied = true;
        gridManager.gridCells[row, col].objectOncell = obstacle;

    }

    GameObject GetPrefab(Obstacle obstacle)
    {
        switch (obstacle.type)
        {
            case ObstacleType.TraficCone:
                return trafficConePrefab;
            case ObstacleType.FireHose:
                return fireFosePrefab;
            default:
                return null;
        }
    }
}
