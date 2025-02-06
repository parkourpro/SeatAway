using UnityEngine;

[CreateAssetMenu(fileName = "SpriteImgs", menuName = "Img/SpriteImg")]
public class SpriteImg : ScriptableObject
{
    public Sprite lv8;
    public Sprite lv12;
    public Sprite paintSeat;
    public Sprite chaseAwayCus;

    public Sprite GetSprite(int level)
    {
        switch (level)
        {
            case 1:
                return lv8;
            case 2:
                return lv12;
            case 3:
                return paintSeat;
            case 4:
                return chaseAwayCus;
            default:
                return null;
        }
    }
}

