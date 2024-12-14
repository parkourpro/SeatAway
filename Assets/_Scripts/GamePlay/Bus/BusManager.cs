using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusManager : Singleton<BusManager>
{
    public Vector3 startBusPosition;
    public Vector3 busStopPosition;
    public Vector3 endBusPosition;
    public string doneAllBusPosition = null;
    IEnumerator Start()
    {
        //wait for get data from levelData to get rows and cols
        //to determine which position bus should be there
        yield return new WaitUntil(() => GridManager.Instance.doneLevelData != "");
        busStopPosition = SetBusStopPosition();
        startBusPosition = busStopPosition + Vector3.back * 20;
        endBusPosition = busStopPosition + Vector3.forward * 30;
        doneAllBusPosition = "doneAllBusPosition";
    }

    void Update()
    {
        
    }

    private Vector3 SetBusStopPosition() {
        Vector3 firstInLinePosition = CustomerManager.Instance.firstInLine.transform.position;
        float a = (float)GridManager.Instance.GetGridCol() / 2;
        Vector3 busPos = firstInLinePosition + Vector3.left * (a + 1.5f);
        busPos += Vector3.back * (GridManager.Instance.GetGridRow() - 1)/2;
        
        //giữ độ cao của floor bằng trong viewport
        float busY = GridManager.Instance.bus.transform.position.y;
        busPos.y = busY;
        return busPos;
    }
}
