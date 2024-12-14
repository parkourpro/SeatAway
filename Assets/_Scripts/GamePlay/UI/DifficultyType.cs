using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyType : MonoBehaviour
{
    public Image difficultyImage;
    public Sprite[] difficultySprite;
    private Sprite thisLevelDifficultySprite;

    public static string doneLevelType = null;
    IEnumerator Start()
    {
        yield return new WaitUntil(() => GridManager.Instance.doneLevelData != "");
        int numberOfCustomer = GridManager.Instance.levelData.customers.Count;
        LevelType levelType = DifficultyLevel(numberOfCustomer);
        thisLevelDifficultySprite = GetSprite(levelType);
        difficultyImage.sprite = thisLevelDifficultySprite;
        doneLevelType = "doneLevelType";
    }



    LevelType DifficultyLevel(int numberOfCustomer)
    {
        if (numberOfCustomer < 15)
        {
            return LevelType.Easy;
        }
        if (numberOfCustomer <25)
        {
            return LevelType.Medium;
        }
        if (numberOfCustomer >= 25)
        {
            return LevelType.Hard;
        }
        else return LevelType.Unknown;
    }

    Sprite GetSprite(LevelType levelType)
    {
        switch (levelType)
        {
            case LevelType.Easy:
                return difficultySprite[0];
            case LevelType.Medium:
                return difficultySprite[1];
            case LevelType.Hard:
                return difficultySprite[2];
            default:
                return null;
        }

    }
}