using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSprite : MonoBehaviour
{
    public GameObject img;
    void Start()
    {
        RectTransform rect = img.GetComponent<RectTransform>();
        Tween a;
        a = rect.DOAnchorPosY(0, 3).SetEase(Ease.InOutBack);
    }


}
