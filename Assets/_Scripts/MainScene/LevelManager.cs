using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject allButtonGameObject;
    public Button playButton;
    private int level = 1;
    void Start()
    {
        //comment dòng dưới để level tăng dần, dòng dưới sử
        //dụng khi test level cụ thể bằng cách kéo scriptable
        //object level vào inspector
        //PlayerPrefs.SetInt("UnlockLevel", 1);
        if (!PlayerPrefs.HasKey("UnlockLevel"))
        {
            PlayerPrefs.SetInt("UnlockLevel", level);
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
        bool a = LevelLoader.Instance.LoadLevel(level);
        if (a)
        {
            SceneManager.LoadScene("CreateMap");
        }
        else
        {
            Debug.Log("Level chưa được tạo");
        }
    }
}
