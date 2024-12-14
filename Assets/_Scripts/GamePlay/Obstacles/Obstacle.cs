

using UnityEngine;

public class Obstacle : ObjectOncell
{
    public ObstacleType type;
    public Obstacle(ObstacleType type) : base (ObjectType.Obstacle, false)
    {
        this.type = type;
    }
}