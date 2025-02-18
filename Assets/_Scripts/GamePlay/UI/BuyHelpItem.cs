using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro; // Thêm namespace DOTween

public class BuyHelpItem : MonoBehaviour
{
    public GameObject buyHelpItemPanelContainer;
    public GameObject buyHelpItemPanel;
    public Button closeButton;
    public Image helpItemImage;
    public Sprite[] helpItemSprites;
    public TextMeshProUGUI helpItemName;
    public TextMeshProUGUI helpItemPrice;
    public Button buyButton;

    public MoreTimeButtonController moreTimeButtonController;
    public PaintSeat paintSeat;
    public ChaseAwayCustomerController chaseAwayCustomerController;

    private int moreTimePrice = 300;
    private int paintSeatPrice = 400;
    private int chaseAwayPrice = 200;
    private int currentPrice;

    private int indexHelpItem;

    private CanvasGroup buyHelpItemPanelContainerCanvasGroup;
    private Vector3 originalScale;

    void Start()
    {
        buyHelpItemPanelContainer.SetActive(false);

        // Lấy component CanvasGroup của buyHelpItemPanelContainer
        buyHelpItemPanelContainerCanvasGroup = buyHelpItemPanelContainer.GetComponent<CanvasGroup>();
        if (buyHelpItemPanelContainerCanvasGroup == null)
        {
            buyHelpItemPanelContainerCanvasGroup = buyHelpItemPanelContainer.AddComponent<CanvasGroup>();
        }

        // Lưu lại scale ban đầu của buyHelpItemPanel
        originalScale = buyHelpItemPanel.transform.localScale;

        // Gán sự kiện cho các nút
        closeButton.onClick.AddListener(OnCloseButtonClick);
        buyButton.onClick.AddListener(OnBuyButtonClick);
    }

    public void ShowBuyHelpItemPanel(int index)
    {
        // Dừng thời gian
        TimeController.Instance.PauseTime();

        indexHelpItem = index;
        helpItemImage.sprite = GetHelpItemSprite(index);
        helpItemName.text = GetHelpItemName(index);
        currentPrice = GetHelpItemPrice(index);
        helpItemPrice.text = currentPrice.ToString();



        // Reset trạng thái ban đầu
        buyHelpItemPanelContainerCanvasGroup.alpha = 0f;
        buyHelpItemPanel.transform.localScale = Vector3.zero;

        buyHelpItemPanelContainer.SetActive(true);

        // Hiệu ứng chuyển màu nền dần dần
        buyHelpItemPanelContainerCanvasGroup.DOFade(1f, 0.3f).SetUpdate(true);

        // Hiệu ứng scale dần dần
        buyHelpItemPanel.transform.DOScale(originalScale, 0.3f)
            .SetEase(Ease.OutBack) // Thêm hiệu ứng bouncy
            .SetUpdate(true);
    }

    void OnCloseButtonClick()
    {
        HideBuyHelpItemPanel();
    }

    public void OnBuyButtonClick()
    {
        if(currentPrice > SaveSystem.GetCoin())
        {
            Debug.Log("Không đủ tiền");
            HideBuyHelpItemPanel();
            return;
        }
        SaveSystem.SaveCoin(SaveSystem.GetCoin() - currentPrice);
        switch (indexHelpItem)
        {
            case 1:
                moreTimeButtonController.AddHelpCount(1);
                break;
            case 2:
                paintSeat.AddHelpCount(1);
                break;
            case 3:
                chaseAwayCustomerController.AddHelpCount(1);
                break;
        }
        //Debug.Log("Buy button clicked!");
        HideBuyHelpItemPanel();
        CoinGamePlay.Instance.UpdateCoinText();
    }

    void HideBuyHelpItemPanel()
    {
        // Tiếp tục thời gian
        TimeController.Instance.ResumeTime();
        // Hiệu ứng fade out
        buyHelpItemPanelContainerCanvasGroup.DOFade(0f, 0.5f).SetUpdate(true)
            .OnComplete(() => {
                buyHelpItemPanelContainer.SetActive(false); // Ẩn panel sau khi fade out hoàn tất
            });

        // Hiệu ứng scale nhỏ dần
        buyHelpItemPanel.transform.DOScale(Vector3.zero, 0.5f)
            .SetEase(Ease.InBack) // Thêm hiệu ứng khi scale nhỏ lại
            .SetUpdate(true);
    }

    void OnDestroy()
    {
        // Dừng tất cả các tween khi đối tượng bị hủy
        DOTween.KillAll();
    }

    Sprite GetHelpItemSprite(int index)
    {
        return helpItemSprites[index - 1];
    }

    string GetHelpItemName(int index)
    {
        switch (index)
        {
            case 1:
                return "time freeze";
            case 2:
                return "paint seat";
            case 3:
                return "chase away";
            default:
                return "Unknown";
        }
    }
    int GetHelpItemPrice(int index)
    {
        switch (index)
        {
            case 1:
                return moreTimePrice;
            case 2:
                return paintSeatPrice;
            case 3:
                return chaseAwayPrice;
            default:
                return 0;
        }
    }
}