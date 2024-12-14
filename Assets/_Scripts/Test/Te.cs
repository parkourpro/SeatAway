using UnityEngine;

public class Te : MonoBehaviour
{
    private void Start()
    {
        Seat a = new SingleSeat(EnumColor.Red, Direction.Up);
        bool b = a is SingleSeat;
        Debug.Log(b);
    }
}