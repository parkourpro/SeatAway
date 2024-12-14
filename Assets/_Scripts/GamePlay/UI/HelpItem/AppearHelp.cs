using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AppearHelp : MonoBehaviour
{
    public GameObject help;                    // GameObject cần di chuyển
    public HelpButtonEnable helpButtonEnable; // Đối tượng để kiểm tra trạng thái
    private RectTransform helpRect;           // RectTransform của help

    [Header("Thời gian di chuyển")]
    public float moveDuration = 0.5f;         // Thời gian di chuyển
    public float extraMoveDuration = 0.1f;   // Thời gian cho giai đoạn giảm
    public float reduceAmount = 0.05f;        // Lượng giảm ở giai đoạn thêm

    IEnumerator Start()
    {
        helpRect = help.GetComponent<RectTransform>();

        // Xác định vị trí bắt đầu và kết thúc
        Vector2 startAnchorMin = helpRect.anchorMin;
        Vector2 startAnchorMax = helpRect.anchorMax;
        Vector2 endAnchorMin = new Vector2(helpRect.anchorMin.x, helpRect.anchorMin.y + (1 - helpRect.anchorMax.y));
        Vector2 endAnchorMax = new Vector2(helpRect.anchorMax.x, 1);

        // Tính toán vị trí giảm thêm
        Vector2 reducedAnchorMin = new Vector2(endAnchorMin.x, endAnchorMin.y - reduceAmount);
        Vector2 reducedAnchorMax = new Vector2(endAnchorMax.x, endAnchorMax.y - reduceAmount);

        // Chờ cho đến khi doneEnableAllHelpButton không null
        yield return new WaitUntil(() => helpButtonEnable.doneEnableAllHelpButton != "");

        // Di chuyển đến vị trí tối đa
        helpRect.DOAnchorMin(endAnchorMin, moveDuration).SetEase(Ease.InOutQuad);
        helpRect.DOAnchorMax(endAnchorMax, moveDuration).SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                // Giai đoạn giảm một chút
                helpRect.DOAnchorMin(reducedAnchorMin, extraMoveDuration).SetEase(Ease.InOutQuad);
                helpRect.DOAnchorMax(reducedAnchorMax, extraMoveDuration).SetEase(Ease.InOutQuad);
            });
    }
}
