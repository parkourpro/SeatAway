using System.Collections;
using UnityEngine;

public class BusController : MonoBehaviour
{
    public BubbleSeatEffect bubbleSeatEffect;
    public BusMovement busMovement;
    public GameObject bus;
    private Transform floor = null;
    private Transform door = null;
    public string busOpenDoor = null;
    private void Awake()
    {
        floor = bus.transform.Find("Floor");
    }
    IEnumerator Start()
    {
        yield return new WaitUntil(() => GridManager.Instance.doneBus != "");
        MoveSeat.setSelectionEnable?.Invoke(false);
        //dù doneBus đã được gán giá trị nhưng BusPrefab cần 1 khoảng thời gian mới
        //có thể được sinh ra trong hierachy trong unity
        yield return new WaitUntil(() => bus.transform.Find("BusPrefab/bus/Door") != null);
        door = bus.transform.Find("BusPrefab/bus/Door");

        //đợi sau khi scene chạy 1 giây rồi cho xe bus di chuyển cho dễ quan sát
        yield return new WaitForSeconds(1);

        //đợi các vị trí dừng xe được thiét lập ròi mới di chuyển xe
        yield return new WaitUntil(() => BusManager.Instance.doneAllBusPosition != "");
        SoundPlayer.Instance.PlaySoundBusCome();

        yield return StartCoroutine(busMovement.MoveToTheBusStop(GridManager.Instance.bus,
            BusManager.Instance.busStopPosition));
        if (BgSound.Instance != null)
        {
            BgSound.Instance.PlaySoundGamePlay();
        }
        //nổi bọt ghế
        yield return StartCoroutine(bubbleSeatEffect.BubbleSeat());
        // mở cửa
        yield return StartCoroutine(OpenDoor());
        //xóa tấm che để có thể drag ghế
        MoveSeat.setSelectionEnable?.Invoke(true);
        busOpenDoor = "busOpenDoor";
    }
    private void OnEnable()
    {
        CanvasController.onWinDo += BusMove;
        CanvasController.onLoseDo += BusMove;
        //CanvasController.onWinDo += CloseDoor;
    }

    private void OnDisable()
    {
        CanvasController.onWinDo -= BusMove;
        CanvasController.onLoseDo -= BusMove;
        //CanvasController.onWinDo -= CloseDoor;

    }

    void BusMove()
    {
        StartCoroutine(busMovement.MoveOutOfTheBusStop(GridManager.Instance.bus,
            BusManager.Instance.endBusPosition));

    }

    IEnumerator OpenDoor()
    {
        yield return StartCoroutine(busMovement.RotateDoorSmoothly(door, 70f, .7f));
    }
    public IEnumerator CloseDoor()
    {
        yield return StartCoroutine(busMovement.RotateDoorSmoothly(door, -180f, .7f));
    }
}
