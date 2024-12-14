using UnityEngine;

[CreateAssetMenu(fileName = "SpriteImgs", menuName = "Img/SpriteImg")]
public class SpriteImg : ScriptableObject
{
    public Sprite lv8;
    public Sprite lv12;

    public Sprite GetSprite(int level)
    {
        switch (level)
        {
            case 8:
                return lv8;
            case 12:
                return lv12;

            default:
                return null;
        }
    }
}

