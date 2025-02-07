using UnityEngine;

[CreateAssetMenu(fileName = "SpriteImgs", menuName = "Img/SpriteImg")]
public class SpriteImg : ScriptableObject
{
    public Sprite graySeatCanNotMove;
    public Sprite addTimeHelp;
    public Sprite paintSeat;
    public Sprite chaseAwayCus;
    public Sprite manyColorsSeat;
    public Sprite doubleSeat;

    public Sprite GetSprite(int level)
    {
        switch (level)
        {
            case 1:
                return graySeatCanNotMove;
            case 2:
                return addTimeHelp;
            case 3:
                return paintSeat;
            case 4:
                return chaseAwayCus;
            case 5:
                return manyColorsSeat;
            case 6:
                return doubleSeat;
            default:
                return null;
        }
    }
}

