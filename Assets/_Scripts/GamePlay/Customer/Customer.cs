using UnityEngine;

public class Customer
{
    //public Seat currentSeat;
    public int currentRow = -1;
    public int currentCol = -1;
    public EnumColor customerColor;
    public GameObject associatedObject;

    public Customer(EnumColor color)
    {
        this.customerColor = color;
    }
}
