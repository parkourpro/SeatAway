using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeTimeImage : MonoBehaviour
{
    public Image freezeTimeImage;
    void Start()
    {
        freezeTimeImage.color = new Color(255, 255, 255, 0);
    }

    public IEnumerator AnimateFreeze(float freezeDuration)
    {
        // Nếu freezeDuration nhỏ hơn 2 thì đảm bảo không âm cho interval
        float holdTime = Mathf.Max(freezeDuration - 2f, 0f);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(freezeTimeImage.DOFade(1f, 1f));       // Fade in trong 1 giây
        sequence.AppendInterval(holdTime);                    // Giữ nguyên alpha = 1
        sequence.Append(freezeTimeImage.DOFade(0f, 1f));       // Fade out trong 1 giây

        yield return sequence.WaitForCompletion();
    }

}
