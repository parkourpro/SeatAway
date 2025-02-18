using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public BuyHeartPanel zeroHeartPanel;
    //public GameObject pausePanelContainer;
    //public GameObject pausePanel;
    public GameObject allButtonGameObject;
    public Button playButton;
    private int level;
    public GameObject commingSoonPanel;
    void Start()
    {
        //PlayerPrefs.SetInt("UnlockLevel", 15);
        if (!PlayerPrefs.HasKey("UnlockLevel"))
        {
            PlayerPrefs.SetInt("UnlockLevel", 1);
            level = 1;
        }
        else
        {
            level = PlayerPrefs.GetInt("UnlockLevel");

        }
        //Debug.Log(level);
        Button[] buttons = allButtonGameObject.GetComponentsInChildren<Button>();
        if (level == 1)
        {
            buttons[0].gameObject.SetActive(false);
            RectTransform rectTransform = allButtonGameObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 0); // Set the bottom to 0
            }
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            TextMeshProUGUI text = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            text.text = (i + level - 1).ToString();
        }
        playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + (level).ToString();

        playButton.onClick.AddListener(OnPlayButtonClick);
    }



    void OnPlayButtonClick()
    {
        int heartCount = SaveSystem.GetHeart();
        if(heartCount == 0)
        {
            Debug.Log("Hết tim");
            zeroHeartPanel.ShowZeroHeartPanel();
            return;
        }
        bool a = LevelLoader.Instance.LoadLevel(level);
        if (a)
        {
            SceneManager.LoadScene("CreateMap");
        }
        else
        {
            Debug.Log("Level chưa được tạo");
            commingSoonPanel.SetActive(true);
        }
    }
}
