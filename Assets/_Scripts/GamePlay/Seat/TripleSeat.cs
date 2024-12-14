using UnityEngine;

public class TripleSeat : Seat
{
    public TripleSeat(EnumColor color, Direction direction) : base(SeatType.Triple, color, direction, 3)
    {
    } 
}