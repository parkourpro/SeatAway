using System;
using UnityEngine;

public static class BusInfo
{
    public static Vector2 GetBusSize(BusType busType)
    {
        switch (busType)
        {
            case BusType.Sixx3:
                return new Vector2(6, 3);
            case BusType.Sixx4:
                return new Vector2(6, 4);
            case BusType.Sevenx5:
                return new Vector2(7, 5);
            case BusType.Eightx4:
                return new Vector2(8, 4);
            case BusType.Eightx5:
                return new Vector2(8, 5);
            case BusType.Tenx5:
                return new Vector2(10, 5);
            case BusType.Tenx6:
                return new Vector2(10, 6);
            case BusType.Thirdteenx7:
                return new Vector2(13, 7);
            default:
                throw new ArgumentOutOfRangeException(nameof(busType), busType, "Unknown bus type");
        }
    }
}