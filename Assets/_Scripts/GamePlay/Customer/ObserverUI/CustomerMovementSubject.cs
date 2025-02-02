using System.Collections.Generic;
using UnityEngine;

public class CustomerMovementSubject : MonoBehaviour
{
    public static CustomerMovementSubject Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }




    private List<ICustomerMovementObserver> observers = new List<ICustomerMovementObserver>();
    private int movingCustomers = 0;


    // Đăng ký observer
    public void RegisterObserver(ICustomerMovementObserver observer)
    {
        observers.Add(observer);
    }

    // Hủy đăng ký observer
    public void UnregisterObserver(ICustomerMovementObserver observer)
    {
        observers.Remove(observer);
    }

    // Thông báo cho tất cả observers khi có sự thay đổi
    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnCustomerMovementChanged(movingCustomers > 0);
        }
    }

    // Tăng số lượng hành khách đang di chuyển
    public void IncrementMovingCustomers()
    {
        movingCustomers++;
        NotifyObservers();
    }

    // Giảm số lượng hành khách đang di chuyển
    public void DecrementMovingCustomers()
    {
        if (movingCustomers > 0)
        {
            movingCustomers--;
            NotifyObservers();
        }
    }
}