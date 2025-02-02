using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HelpActivationController : MonoBehaviour
{
    //script quản lý hiện ẩn số lượng help
    public GameObject helpAreaPanel;
    private int unlockMoreTimeAt = 9;
    //private int unlockFreeMoveSeatAt = 11;
    private int unlockPaintSeatAt = 11;
    private int unlockChaseAwayCustomerAt = 13;

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
            Transform help = helpAreaPanel.transform.GetChild(0);
            if (unlockLevel < unlockPaintSeatAt)
            {
                SetActiveBaseOnParam(help, 1);
            }
            else if (unlockLevel < unlockChaseAwayCustomerAt)
            {
                SetActiveBaseOnParam(help, 2);

            }
            //else if (unlockLevel < unlockChaseAwayCustomerAt)
            //{
            //    SetActiveBaseOnParam(help, 3);

            //}
            else
            {
                SetActiveBaseOnParam(help, 3);
            }
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
