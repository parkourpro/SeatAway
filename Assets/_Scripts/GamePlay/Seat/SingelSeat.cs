using UnityEngine;


public class SingleSeat : Seat
{
    public SingleSeat(EnumColor seatColor, Direction direction) 
        : base(SeatType.Single, seatColor, direction, 1) 
    {
    }
}