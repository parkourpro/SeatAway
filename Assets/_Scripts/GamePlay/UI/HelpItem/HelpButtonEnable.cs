using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpButtonEnable : MonoBehaviour
{
    public GameObject help;
    public string doneEnableAllHelpButton = null;
    public GameObject capCollider;

    private void Awake()
    {
        capCollider.SetActive(true);
    }
    IEnumerator Start()
    {
        DisableButton();
        yield return new WaitUntil(() => !capCollider.activeSelf);
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