using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreeMoveSeatController : MonoBehaviour
{
    public Button freeMoveButton;
    private void Start()
    {
        freeMoveButton.onClick.AddListener(OnFreeMoveButtonClick);
    }

    void OnFreeMoveButtonClick()
    {
        Debug.Log(2);
    }
}
