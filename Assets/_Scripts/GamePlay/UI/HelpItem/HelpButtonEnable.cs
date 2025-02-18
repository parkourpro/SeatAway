using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpButtonEnable : MonoBehaviour
{
    public GameObject help;
    public string doneEnableAllHelpButton = null;
    public BusController busController;
    IEnumerator Start()
    {
        DisableButton();
        yield return new WaitUntil(() => busController.busOpenDoor != "");
        EnableButton();
        doneEnableAllHelpButton = "doneEnableAllHelpButton";
    }


    void DisableButton()
    {
        SetEnablement(false);
    }
    void EnableButton()
    {
        SetEnablement(true);
    }

    void SetEnablement(bool enable)
    {
        foreach (Button btn in help.GetComponentsInChildren<Button>())
        {
            btn.enabled = enable;
        }
    }

}