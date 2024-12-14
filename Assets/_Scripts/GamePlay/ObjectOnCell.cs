


using UnityEngine;

public class ObjectOncell
{
    public ObjectType objectType;
    public bool movable;
    public int currentCol = -1;
    public int currentRow = -1;
    public GameObject associatedObject; //lúc Instance sẽ đặt sau

    public ObjectOncell(ObjectType objectType, bool movable)
    {
        this.objectType = objectType;
        this.movable = movable;
    }
}