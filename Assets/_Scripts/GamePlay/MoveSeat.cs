using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MoveSeat : MonoBehaviour
{
    //public GridManager gridManager;

    private Seat selectedSeat = null;  // Ghế đang được kéo
    private GameObject selectedSeatObject = null; // Object của ghế đang được kéo
    private Vector3 offset; //độ lệch giữa tâm ghế và vị trí bấm
    private float heightDraggingSeat;
    private bool isDragging = false;
    private Rigidbody rb;
    public float moveSpeed = 5f;
    private Vector3 higher = new(0, 0.5f, 0);
    //private Vector3 velocity = Vector3.zero; // Tốc độ mượt cho SmoothDamp
    private float rayDistanceFromSeat = 3f;
    private Vector3 rayDirection = Vector3.down;
    private Animator seatAnimator;
    private bool startedTime = false;

    public CustomerController customerController;

    private bool isSelectionEnabled = true;
    public delegate void MoveSeatDelegate(bool state);
    public static MoveSeatDelegate setSelectionEnable;
    
    public static bool isDragFirstSeat;

    private void OnEnable()
    {
        setSelectionEnable += SetSelectionSeat;
    }

    private void OnDisable()
    {
        setSelectionEnable -= SetSelectionSeat;
    }

    private void Start()
    {
        isDragFirstSeat = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isSelectionEnabled)
        {
            DetectSeatSelection();
            if (selectedSeatObject != null)
            {
                isDragging = true;
                seatAnimator.SetBool("isDragging", true);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            if (selectedSeatObject != null)
            {
                //Debug.Log("Drop");
                DropSeat();
                seatAnimator.SetBool("isDragging", false);
            }
        }
        if (isDragging)
        {
            //Debug.Log("Drag");
            DragSeat();
        }
    }


    void DetectSeatSelection()
    {
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.name.Contains("seat"))
                {
                    if (!startedTime)
                    {
                        startedTime = true;
                        TimeController.startTimeDelegate?.Invoke();
                    }
                    if (!isDragFirstSeat)
                    {
                        isDragFirstSeat = true; //dùng để kích hoạt sự kiện trừ lượt chơi nếu thua
                        if(SaveSystem.GetHeart() == HeartManager.maxHeartCount)
                        {
                            //Debug.Log("Save time");
                            SaveSystem.SaveTime();
                            SaveSystem.SaveRemainderSec(HeartManager.secondsToRecoverHeart);
                        }
                        SaveSystem.SaveHeart(SaveSystem.GetHeart() - 1);
                        //Debug.Log("Sub to: " + SaveSystem.GetHeart());
                    }
                    //Debug.Log(hitObject.gameObject.name); //in ra tên ghế
                    if (hitObject.GetComponent<SeatDataa>().seat.movable == false)
                    {
                        Debug.Log("Cannot move this seat");
                        return;
                    }
                    SoundPlayer.Instance.PlaySoundSelect();
                    selectedSeatObject = hitObject; //gán vật đang chọn
                    selectedSeat = hitObject.GetComponent<SeatDataa>().seat; //lấy data ghế đang chọn
                    selectedSeatObject.transform.position += higher; //nâng ghế lên độ cao so với sàn
                    heightDraggingSeat = selectedSeatObject.transform.position.y;
                    offset = hit.point - selectedSeatObject.transform.position;
                    offset.y = 0; //đặt offset là khoảng cách chỉ có row và z
                    rb = selectedSeatObject.GetComponent<Rigidbody>();
                    seatAnimator = selectedSeatObject.GetComponent<Animator>();

                    rb.isKinematic = false;
                }

            }
        }

    }

    void DragSeat()
    {
        if (selectedSeatObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits any object in the scene (e.g., the ground or grid area)
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 destinationPosition = hit.point - offset;
                destinationPosition.y = heightDraggingSeat; //destinationPosition vẫn ở độ cao, chưa hạ
                Vector3 moveDimension = destinationPosition - selectedSeatObject.transform.position;
                float distance = Vector3.Distance(selectedSeatObject.transform.position, destinationPosition);
                if (distance > 0.2f)
                {
                    //Debug.Log("move");
                    rb.velocity = moveDimension.normalized * moveSpeed * (distance + 1f);
                    //selectedSeatObject.transform.position = Vector3.Lerp(selectedSeatObject.transform.position, destinationPosition, Time.deltaTime*moveSpeed);
                    //rb.MovePosition(Vector3.Lerp(selectedSeatObject.transform.position, destinationPosition, Time.deltaTime * moveSpeed));
                }
                else
                {
                    //Debug.Log("stop");
                    rb.velocity = Vector3.zero;
                }
            }
        }
    }



    void DropSeat()
    {
        //đặt ghế đúng chỗ ở đây
        Vector3 rayOrigin = selectedSeatObject.transform.position;
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayDistanceFromSeat))
        {
            string objectName = hit.collider.name;

            // Sử dụng regex để lấy hai số từ tên
            Match match = Regex.Match(objectName, @"^r(\d+)_(\d+)$");
            if (match.Success)
            {
                SoundPlayer.Instance.PlaySoundDrop();
                // Lấy hai số từ nhóm bắt được trong regex
                int firstNumber = int.Parse(match.Groups[1].Value);
                int secondNumber = int.Parse(match.Groups[2].Value);
                //GridCell cell = gridManager.gridCells[firstNumber, secondNumber];s
                //Debug.Log("Hit object: " + firstNumber + ", " + secondNumber);
                //Debug.Log(cell.tile.transform.position);
                GridManager.Instance.MoveSeat(firstNumber, secondNumber, selectedSeat);
                //selectedSeatObject.transform.position = new Vector3(cell.tile.transform.position.row, selectedSeatObject.transform.position.col, cell.tile.transform.position.z);
                //LogMap();
            }
            //
            //selectedSeatObject.transform.position -= higher;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            ClearDataSeat();
        }
        StartCoroutine(customerController.CustomerGoToSeat());

    }

    private void ClearDataSeat()
    {
        selectedSeatObject = null;
        selectedSeat = null;
    }

    private void SetSelectionSeat(bool state)
    {
        isSelectionEnabled = state;
        //Debug.Log("Seat selection: " + state);
    }
}
