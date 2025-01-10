using System.Collections;
using UnityEngine;
using DG.Tweening;

public class InstructionPanelController : MonoBehaviour
{
    public BusController busController;
    public GameObject instructionPanel;
    private void Awake()
    {
        DeActivateInstructionPanel();

    }
    IEnumerator Start()
    {
        yield return new WaitUntil(() => GridManager.Instance.doneLevelData != "");
        int level = GridManager.Instance.GetLevel();
        yield return new WaitUntil(() => busController.busOpenDoor != "");
        ProcessLevel(level);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Nhấn chuột hoặc chạm vào màn hình
        {
            DeActivateInstructionPanel();
        }
    }

    void ProcessLevel(int level)
    {
        switch (level)
        {
            case 1:
                MoveHand();
                break;
            //case 2:
            //    break;
            //case 3:
            //    break;
            //case 4:
            //    break;
            default:
                break;
        }
    }

    void MoveHand()
    {
        ActivateInstructionPanel();
        Transform handTransf = instructionPanel.transform.Find("Hand");
        RectTransform arrowTransf = instructionPanel.transform.Find("Arrow").GetComponent<RectTransform>();

        RectTransform parentImg = handTransf.parent.GetComponent<RectTransform>();
        RectTransform handRect = handTransf.gameObject.GetComponent<RectTransform>();
        GameObject bus = GameObject.Find("Bus");
        Transform seat1 = bus.transform.Find("AllSeats/seat1");
        Vector3 seatScreenPosition = Camera.main.WorldToScreenPoint(seat1.position);
        Canvas canvas = FindObjectOfType<Canvas>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentImg, //cần lấy localPoint của image so  với cha
            //để xem image nằm ở vị trí nào so với cha, sau đó set localPoint
            //hoặc anchorPosition cho nó, vì marker của cha tản ra 4 góc nên
            //anchorPosition nằm chính giữa, dẫn đến set anchorPosition 
            //hoặc localPosition đều đúng, lưu ý cần set anchor của hand trùng với
            //tâm của Rect cha (middle -center)
            seatScreenPosition,//screenPoint của 3D object
            canvas.worldCamera,//screen overlay mặc định vẽ lên màn hình nên 
            //thực tế không cần cam, có thể để null như dòng dưới
            //null,
            out Vector2 localPoint
        );
        handRect.anchoredPosition = localPoint + new Vector2(100, -50);
        arrowTransf.anchoredPosition = localPoint + new Vector2(-100, 0);
        // Bắt đầu hiệu ứng di chuyển
        StartHandAnimation(handRect, localPoint);
    }

    void StartHandAnimation(RectTransform handRect, Vector2 startPosition)
    {
        // Di chuyển sang trái 500 và callback khi hoàn thành
        handRect.DOAnchorPosX(handRect.anchoredPosition.x - 300, 1f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                if (!instructionPanel.activeSelf) return;
                handRect.anchoredPosition = startPosition + new Vector2(100, -50);
                // Lặp lại hiệu ứng
                StartHandAnimation(handRect, startPosition);
            });
    }


    void ActivateInstructionPanel()
    {
        instructionPanel.SetActive(true);
    }
    void DeActivateInstructionPanel()
    {
        instructionPanel.SetActive(false);
    }
}
