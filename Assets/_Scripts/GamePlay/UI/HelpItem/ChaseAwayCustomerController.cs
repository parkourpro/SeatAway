using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChaseAwayCustomerController : MonoBehaviour
{
    public Transform chaseAwayDes;
    public Button chaseAwayCustomerButton;
    public CustomerController customerController;
    public TextMeshProUGUI helpCountText;
    public BuyHelpItem buyHelpItem;


    private int helpCount;


    private void Start()
    {
        // Đăng ký sự kiện khi nút được nhấn
        chaseAwayCustomerButton.onClick.AddListener(OnChaseAwayCustomerButtonClick);
        helpCount = 1;
        UpdateHelpCountUI();

    }

    private void OnChaseAwayCustomerButtonClick()
    {
        if (TimeController.hasWon)
        {
            return;
        }

        if (helpCount <= 0)
        {
            Debug.Log("Hết lượt trợ giúp đuổi khách");
            buyHelpItem.ShowBuyHelpItemPanel(3);
            return;
        }
        // Kiểm tra xem có khách hàng trong hàng đợi không
        if (CustomerManager.Instance != null && CustomerManager.Instance.customerList.Count > 0)
        {
            helpCount--;
            UpdateHelpCountUI();
            // Lấy khách hàng đầu tiên từ hàng đợi
            Customer customer = CustomerManager.Instance.customerList.Dequeue();
            CustomerController.totalCus--;
            StartCoroutine(CustomerMovement.Move(customer.associatedObject, chaseAwayDes.position));
            if (CustomerManager.Instance.customerList.Count == 0)
            {
                StartCoroutine(customerController.Win());
                return;
            }
            CustomerManager.Instance.LineUpCustomer();

        }
        else
        {
            Debug.Log("No customers in the queue.");
        }
    }

    private void UpdateHelpCountUI()
    {
        helpCountText.text = helpCount.ToString();
        if (helpCount <= 0)
        {
            helpCountText.text = "+";
        }
    }

    public void AddHelpCount(int count)
    {
        helpCount += count;
        UpdateHelpCountUI();
    }
}
