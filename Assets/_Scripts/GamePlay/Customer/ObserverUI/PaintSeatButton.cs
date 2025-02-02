using UnityEngine;
using UnityEngine.UI;

public class PaintSeatButton : MonoBehaviour, ICustomerMovementObserver
{
    public Button paintSeatButton;
    public Image buttonImage;
    private Color originalColor;

    private void Start()
    {
        originalColor = buttonImage.color;
        // Đăng ký observer với subject
        CustomerMovementSubject.Instance.RegisterObserver(this);
    }

    // Xử lý sự kiện khi trạng thái di chuyển của hành khách thay đổi
    public void OnCustomerMovementChanged(bool isMoving)
    {
        if (isMoving)
        {
            // Vô hiệu hóa nút và đổi màu
            paintSeatButton.enabled = false;
            buttonImage.color = new Color(150f / 255f, 150f / 255f, 150f / 255f);
        }
        else
        {
            // Kích hoạt lại nút và đổi màu về ban đầu
            paintSeatButton.enabled = true;
            buttonImage.color = originalColor;
        }
    }

    private void OnDestroy()
    {
        // Hủy đăng ký observer khi đối tượng bị hủy
        CustomerMovementSubject.Instance.UnregisterObserver(this);
    }
}