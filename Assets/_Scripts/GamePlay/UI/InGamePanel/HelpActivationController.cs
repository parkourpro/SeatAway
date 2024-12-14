using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HelpActivationController : MonoBehaviour
{
    //script quản lý hiện ẩn số lượng help
    public GameObject helpAreaPanel;
    public int unlockMoreTimeAt = 12;
    public int unlockFreeMoveSeatAt = 18;
    public int unlockPaintSeatAt = 25;
    public int unlockChaseAwayCustomerAt = 32;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => GridManager.Instance.doneLevelData != "");

        int unlockLevel = GridManager.Instance.GetLevel();
        if (unlockLevel < unlockMoreTimeAt)
        {
            helpAreaPanel.SetActive(false);
        }
        else
        {
            helpAreaPanel.SetActive(true);
        }
        Transform help = helpAreaPanel.transform.GetChild(0);
        if (unlockLevel >= unlockMoreTimeAt && unlockLevel < unlockFreeMoveSeatAt)
        {
            SetActiveBaseOnParam(help, 1);
        }
        else if (unlockLevel >= unlockFreeMoveSeatAt && unlockLevel < unlockPaintSeatAt)
        {
            SetActiveBaseOnParam(help, 2);

        }
        else if (unlockLevel >= unlockPaintSeatAt && unlockLevel < unlockChaseAwayCustomerAt)
        {
            SetActiveBaseOnParam(help, 3);

        }
        else
        {
            SetActiveBaseOnParam(help, 4);

        }

    }

    void SetActiveBaseOnParam(Transform help, int numActive)
    {
        for (int i = 0; i < help.childCount; i++)
        {
            if(i< numActive)
            {
                help.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                help.GetChild(i).gameObject.SetActive(false);

            }
        }
    }


}
