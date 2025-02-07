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
    private int helpCount = 2;


    private void Start()
    {
        // Đăng ký sự kiện khi nút được nhấn
        chaseAwayCustomerButton.onClick.AddListener(OnChaseAwayCustomerButtonClick);
        helpCount = 2;
        UpdateHelpCountUI();

    }

    private void OnChaseAwayCustomerButtonClick()
    {
        if (helpCount <= 0)
        {
            Debug.Log("Hết lượt trợ giúp đuổi khách");
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
            if(CustomerManager.Instance.customerList.Count == 0)
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
        if (helpCountText != null)
        {
            helpCountText.text = helpCount.ToString();
        }
    }
}
