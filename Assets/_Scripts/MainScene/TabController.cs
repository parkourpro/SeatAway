using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public GameObject[] panel = new GameObject[3];
    public Button[] button = new Button[3];

    void Start()
    {
        // Đăng ký sự kiện OnClick cho từng button
        for (int i = 0; i < button.Length; i++)
        {
            int index = i; // Lưu index để truyền vào hàm ShowTab khi bấm nút
            button[i].onClick.AddListener(() => ShowTab(index));
        }

        // Mặc định hiển thị panel đầu tiên, ẩn các panel khác
        ShowTab(1);
    }

    // Hàm bật/tắt các panel dựa trên index
    void ShowTab(int index)
    {
        for (int i = 0; i < panel.Length; i++)
        {
            panel[i].SetActive(i == index); // Chỉ hiển thị panel có index được chọn
        }
    }
}
