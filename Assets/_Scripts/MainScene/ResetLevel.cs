using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetLevel : MonoBehaviour
{
    public Button resetLevelButton;
    // Start is called before the first frame update
    void Start()
    {
        resetLevelButton.onClick.AddListener(OnResetLevelButtonClick);
    }

    // Update is called once per frame
    private void OnResetLevelButtonClick()
    {
        //Debug.Log("Set UnlockLevel to 11");
        PlayerPrefs.SetInt("UnlockLevel", 1);
        SceneManager.LoadScene("MainScene");
    }
}
