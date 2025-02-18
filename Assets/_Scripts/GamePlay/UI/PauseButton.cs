using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject giveUpPanelContainer;
    public GameObject giveUpPanel;

    public Button pauseButton;
    public GameObject pausePanelContainer;
    public GameObject pausePanel;
    public Button closePausePanelButton;
    public Button quitButton;
    public Button closeGiveUpPanelButton;
    public Button giveUpButton;


    private CanvasGroup pausePanelContainerCanvasGroup;
    private CanvasGroup giveUpPanelContainerCanvasGroup; // CanvasGroup cho giveUpPanel
    private Vector3 originalScale;
    private Vector3 giveUpPanelOriginalScale; // Scale ban đầu của giveUpPanel


    public BusController busController;
    void Awake()
    {
        pausePanelContainer.SetActive(false);
        giveUpPanelContainer.SetActive(false); // Ẩn giveUpPanel khi khởi động

        pauseButton.enabled = false;
        pauseButton.onClick.AddListener(OnPauseButtonClick);
        closePausePanelButton.onClick.AddListener(OnClosePausePanelButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
        closeGiveUpPanelButton.onClick.AddListener(OnCloseGiveUpPanelButtonClick);
        giveUpButton.onClick.AddListener(OnGiveUpButtonClick);

        // Lấy component CanvasGroup của pausePanelContainer
        pausePanelContainerCanvasGroup = pausePanelContainer.GetComponent<CanvasGroup>();
        if (pausePanelContainerCanvasGroup == null)
        {
            pausePanelContainerCanvasGroup = pausePanelContainer.AddComponent<CanvasGroup>();
        }

        // Lấy component CanvasGroup của giveUpPanelContainer
        giveUpPanelContainerCanvasGroup = giveUpPanelContainer.GetComponent<CanvasGroup>();
        if (giveUpPanelContainerCanvasGroup == null)
        {
            giveUpPanelContainerCanvasGroup = giveUpPanelContainer.AddComponent<CanvasGroup>();
        }

        // Lưu lại scale ban đầu của pausePanel và giveUpPanel
        originalScale = pausePanel.transform.localScale;
        giveUpPanelOriginalScale = giveUpPanel.transform.localScale;
    }

    private void OnApplicationPause()
    {
        if (!pausePanel.activeSelf)
        {
            SaveSystem.SaveTime();
            ShowPausePanel();
        }
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => busController.busOpenDoor != "");
        pauseButton.enabled = true;
    }

    void OnPauseButtonClick()
    {
        TimeController.pauseTimeDelegate?.Invoke();
        MoveSeat.setSelectionEnable?.Invoke(false);
        ShowPausePanel();
    }

    void ShowPausePanel()
    {
        // Reset trạng thái ban đầu
        pausePanelContainerCanvasGroup.alpha = 0f;
        pausePanel.transform.localScale = Vector3.zero;

        pausePanelContainer.SetActive(true);

        // Hiệu ứng chuyển màu nền dần dần
        pausePanelContainerCanvasGroup.DOFade(1f, 0.5f).SetUpdate(true);

        // Hiệu ứng scale dần dần
        pausePanel.transform.DOScale(originalScale, 0.5f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    void OnClosePausePanelButtonClick()
    {
        TimeController.resumeTimeDelegate?.Invoke();
        MoveSeat.setSelectionEnable?.Invoke(true);
        HidePausePanel();
    }
    void OnQuitButtonClick()
    {
        if (!MoveSeat.isDragFirstSeat)
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            HidePausePanel();
            ShowGiveUpPanel();
        }

    }


    void HidePausePanel()
    {
        // Hiệu ứng fade out
        pausePanelContainerCanvasGroup.DOFade(0f, 0.5f).SetUpdate(true)
            .OnComplete(() =>
            {
                pausePanelContainer.SetActive(false);
            });

        // Hiệu ứng scale nhỏ dần
        pausePanel.transform.DOScale(Vector3.zero, 0.5f)
            .SetEase(Ease.InBack)
            .SetUpdate(true);
    }

    void OnCloseGiveUpPanelButtonClick()
    {
        HideGiveUpPanel();
        ShowPausePanel();
    }


    void OnGiveUpButtonClick()
    {
        //Debug.Log("Give up button clicked!");
        if (SaveSystem.GetHeart() == HeartManager.maxHeartCount - 1)
        {
            SaveSystem.SaveTime();
            SaveSystem.SaveRemainderSec(HeartManager.secondsToRecoverHeart);
            //Debug.Log("Save time");
        }
        SceneManager.LoadScene("MainScene");
    }

    void ShowGiveUpPanel()
    {
        // Reset trạng thái ban đầu
        giveUpPanelContainerCanvasGroup.alpha = 0f;
        giveUpPanel.transform.localScale = Vector3.zero;

        giveUpPanelContainer.SetActive(true);

        // Hiệu ứng chuyển màu nền dần dần
        giveUpPanelContainerCanvasGroup.DOFade(1f, 0.5f).SetUpdate(true);

        // Hiệu ứng scale dần dần
        giveUpPanel.transform.DOScale(giveUpPanelOriginalScale, 0.5f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    void HideGiveUpPanel()
    {
        // Hiệu ứng fade out
        giveUpPanelContainerCanvasGroup.DOFade(0f, 0.5f).SetUpdate(true)
            .OnComplete(() =>
            {
                giveUpPanelContainer.SetActive(false);
            });

        // Hiệu ứng scale nhỏ dần
        giveUpPanel.transform.DOScale(Vector3.zero, 0.5f)
            .SetEase(Ease.InBack)
            .SetUpdate(true);
    }

    void OnDestroy()
    {
        // Dừng tất cả các tween khi đối tượng bị hủy
        DOTween.KillAll();
    }
}