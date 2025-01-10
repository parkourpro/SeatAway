using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/LevelData")]
public class LevelData : ScriptableObject
{
    public int timeInSecond;
    public BusType busType;
    public List<SeatData> seats;
    public List<ObstacleData> obstacles;
    public List<CustomerData> customers;
}


[Serializable]
public class SeatData
{
    public SeatType seatType;
    public EnumColor seatColor;
    public Direction direction;
    public int row;
    public int col;
    //public bool movable = true; //this is base on seat color, grey is false, other is true
}

[Serializable]
public class ObstacleData
{
    public ObstacleType type;
    public int x;
    public int y;
}

[Serializable]
public class CustomerData
{
    public EnumColor customerColor;
}
