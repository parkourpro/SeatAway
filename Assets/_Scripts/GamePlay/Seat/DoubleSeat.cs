using UnityEngine;

public class DoubleSeat : Seat
{
    public DoubleSeat(EnumColor seatColor, Direction direction) : base(SeatType.Double, seatColor, direction, 2)
    {
    }
}
