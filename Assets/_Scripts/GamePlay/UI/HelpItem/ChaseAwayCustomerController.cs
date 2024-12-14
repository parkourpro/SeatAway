using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaseAwayCustomerController : MonoBehaviour
{
    public Button chaseAwayCustomerButton;
    private void Start()
    {
        chaseAwayCustomerButton.onClick.AddListener(OnChaseAwayCustomerButtonClick);
    }

    void OnChaseAwayCustomerButtonClick()
    {
        Debug.Log(4);
    }
}
