using UnityEngine;

public static class ColorHelper
{
    public static Color GetColor(EnumColor seatColor)
    {
        return seatColor switch
        {
            EnumColor.Red => Color.red,
            EnumColor.Green => Color.green,
            EnumColor.Blue => new Color(0.129f, 0.549f, 1f),
            EnumColor.White => Color.white,
            EnumColor.Black => Color.black,
            EnumColor.Yellow => Color.yellow,
            EnumColor.Cyan => Color.cyan,
            EnumColor.Magenta => Color.magenta,
            EnumColor.Grey => new Color(.9f, .9f, .8f),
            EnumColor.Orange => new Color(1.0f, 0.647f, 0.0f), // RGB for Orange
            EnumColor.Purple => new Color(0.5f, 0.0f, 0.5f), // RGB for Purple
            EnumColor.Brown => new Color(0.545f, 0.271f, 0.075f), // RGB for Brown
            EnumColor.Pink => new Color(1.0f, 0.75f, 0.8f), // RGB for Pink
            EnumColor.Teal => new Color(0.0f, 0.5f, 0.5f), // RGB for Teal
            EnumColor.Lavender => new Color(0.901f, 0.901f, 0.980f), // RGB for Lavender
            EnumColor.Maroon => new Color(0.5f, 0.0f, 0.0f), // RGB for Maroon
            EnumColor.Olive => new Color(0.501f, 0.501f, 0.0f), // RGB for Olive
            EnumColor.Navy => new Color(0.0f, 0.0f, 0.5f), // RGB for Navy
            EnumColor.Coral => new Color(1.0f, 0.498f, 0.314f), // RGB for Coral
            EnumColor.Salmon => new Color(0.980f, 0.502f, 0.447f), // RGB for Salmon
            _ => Color.white // Default value if not in enum
        };
    }
}
