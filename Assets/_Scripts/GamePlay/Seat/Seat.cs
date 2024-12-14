using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Seat : ObjectOncell
{
    protected SeatType type; // Loại ghế (Single, Double, etc.)
    public EnumColor seatColor; // Màu ghế
    public Direction direction; //Up, Down, Left, Right

    public int capacity; // Số lượng hành khách tối đa ghế có thể chứa
    public List<Customer> occupiedCustomers; // Danh sách hành khách ngồi trên ghế


    public Seat(SeatType type, EnumColor seatColor, Direction direction, int capacity)
        : base(ObjectType.Seat, true)
    {
        this.type = type;
        this.seatColor = seatColor;
        this.direction = direction;
        this.capacity = capacity;
        occupiedCustomers = new List<Customer>(new Customer[capacity]);
    }


    public bool AddCustomerAtIndex(Customer customer, int index)
    {
        if (index < 0 || index >= capacity)
        {
            Debug.LogError("Index không hợp lệ");
            return false; // Chỉ số không hợp lệ
        }

        if (occupiedCustomers[index] == null)
        {
            occupiedCustomers[index] = customer;
            return true; // Thêm khách thành công
        }

        Debug.Log("Vị trí đã có người ngồi");
        return false; // Vị trí đã có người
    }

}
