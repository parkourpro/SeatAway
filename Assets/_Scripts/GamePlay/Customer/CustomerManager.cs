using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : Singleton<CustomerManager>
{
    //public static CustomerManager Instance
    
    private LevelData levelData;

    public GameObject customerListGameObject;
    public GameObject firstInLine;
    public GameObject customerPrefab;
    private Vector3 firstInLinePosition;
    public Queue<Customer> customerList = new();
    public int totalCustomer;

    private Vector3 nextCusPos = new (0.03f, 0, -1f);
    private int customerNumber = 1;

    public static string doneFirstInLine = null;
    public static string doneCustomer = null;
    IEnumerator Start()
    {
        //wait for get data from levelData to get customer data
        yield return new WaitUntil(() => GridManager.Instance.doneLevelData != "");
        levelData = GridManager.Instance.levelData;

        firstInLinePosition = firstInLine.transform.position;
        firstInLinePosition.y += firstInLine.transform.localScale.y/2;
        
        CreateCustomer();
        GenerateCustomer();
        doneCustomer = "done";
    }

    //private void CreateCustomer()
    //{
    //    Customer customer1 = new (Color.yellow);
    //    Customer customer2 = new(Color.red);
    //    Customer customer3 = new(Color.blue);

    //    customerList.Enqueue(customer1);
    //    customerList.Enqueue(customer2);
    //    customerList.Enqueue(customer3);
    //}
    private void CreateCustomer()
    {
        foreach (CustomerData customerData in levelData.customers)
        {
            Customer customer = new (customerData.customerColor);
            customerList.Enqueue(customer);
        }
        totalCustomer = customerList.Count;
    }


    private void GenerateCustomer()
    {
        Vector3 customerPosition = firstInLinePosition;
        int customerCount = customerList.Count;
        for(int i = 0; i < customerCount; i++)
        {
            //Debug.Log(i);
            Customer customer = customerList.Dequeue();
            //tạo gameObject
            Quaternion customerRotation = Quaternion.identity;
            GameObject customerGameobject = Instantiate(customerPrefab, customerPosition, customerRotation, customerListGameObject.transform);
            customerGameobject.name = $"customer{customerNumber}";
            ColorCustomer(customerGameobject, customer);
            //gán dữ liệu:
            customer.associatedObject = customerGameobject;

            customerNumber++;
            customerPosition += nextCusPos;
            customerList.Enqueue(customer);
        }
    }

    public  void LineUpCustomer()
    {
        Vector3 customerPosition = firstInLinePosition;
        int customerCount = customerList.Count;
        for (int i = 0; i < customerCount; i++)
        {
            Customer customer = customerList.Dequeue();
            StartCoroutine(CustomerMovement.Move(customer.associatedObject,
                customerPosition));
            customerPosition += nextCusPos;
            customerList.Enqueue(customer);
        }
    }



    void ColorCustomer(GameObject customerObject, Customer customer)
    {
        Renderer customerRender = customerObject.GetComponentInChildren<SkinnedMeshRenderer>();
        if (customerRender == null)
        {
            Debug.Log("không thấy component render để tạo màu cho người");
            return;
        }
        //Debug.Log(customer.customerColor);
        customerRender.material.color = ColorHelper.GetColor(customer.customerColor);
        //Debug.Log(customerRender.material);
        //customerRender.material.SetColor("_BaseColor", customer.customerColor);
    }

    public Vector3 GetFirstInLinePosition()
    {
        return firstInLinePosition;
    }
}
