using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class CanvasController : MonoBehaviour
{
    public delegate void MyDelegate();

    public static MyDelegate onWinDo;
    public static MyDelegate onLoseDo;
    public GameObject winPanel;
    public GameObject losePanel;
    public Button continueButton;
    public Button continueButton2;
    //public TextMeshProUGUI levelText;
    private void OnEnable()
    {
        onWinDo += ShowWinPanel;
        onLoseDo += ShowLosePanel;
    }
    private void OnDisable()
    {
        onWinDo -= ShowWinPanel;
        onLoseDo -= ShowLosePanel;
    }
    void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        continueButton.onClick.AddListener(OnContinueButtonClick);
        continueButton2.onClick.AddListener(OnContinueButtonClick);
        //levelText.text = "Level " + PlayerPrefs.GetInt("UnlockLevel").ToString();
    }

    // Update is called once per frame
    private void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }
    private void ShowLosePanel()
    {
        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
    }

    private void OnContinueButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }
}
