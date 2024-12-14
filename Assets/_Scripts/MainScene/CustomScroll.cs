using UnityEngine;
using UnityEngine.UI;

public class CustomScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float sensitivity = 1.0f; // độ nhạy của kéo ban đầu
    public float maxDragFactor = 5.0f; // tăng độ khó của kéo về sau
    public float returnSpeed = 5.0f; // tốc độ hồi về vị trí ban đầu

    private Vector2 initialPosition;
    private Vector2 dragStartPosition;

    void Start()
    {
        initialPosition = scrollRect.content.anchoredPosition;
    }
    void Update()
    {
        if (!Input.GetMouseButton(0)) // Khi thả chuột
        {
            scrollRect.content.anchoredPosition = Vector2.Lerp(scrollRect.content.anchoredPosition, initialPosition, returnSpeed * Time.deltaTime);
        }
    }
    public void OnBeginDrag()
    {
        dragStartPosition = scrollRect.content.anchoredPosition;
    }

    public void OnDrag()
    {
        Vector2 currentPosition = scrollRect.content.anchoredPosition;
        float distance = Vector2.Distance(currentPosition, dragStartPosition);
        float dragFactor = Mathf.Lerp(1.0f, maxDragFactor, distance / 100); // tăng độ khó kéo

        scrollRect.velocity *= dragFactor * sensitivity;
    }


}
