using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoreTimeButtonController : MonoBehaviour
{
    public TimeController timeController;
    public Button moreTimeButton;
    private void Start()
    {
        moreTimeButton.onClick.AddListener(OnMoreTimeButtonClick);
    }

    void OnMoreTimeButtonClick()
    {
        Debug.Log(1);
        timeController.AddTime(2);
    }

}
