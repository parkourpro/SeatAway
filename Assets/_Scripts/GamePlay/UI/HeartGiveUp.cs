using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HeartGiveUp : MonoBehaviour
{
    public GameObject giveUpPanelContainer;
    public Image breakLeftImage;
    public Image breakRightImage;

    private Tween breakLeftTween;
    private Tween breakRightTween;

    void Start()
    {
        // Tạo tween cho breakLeftImage
        breakLeftTween = breakLeftImage.transform.DORotate(new Vector3(0, 0, 15), 1f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo) // Yoyo để quay về vị trí ban đầu
            .Pause(); // Tạm dừng tween ban đầu

        // Tạo tween cho breakRightImage
        breakRightTween = breakRightImage.transform.DORotate(new Vector3(0, 0, -15), 1f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo) // Yoyo để quay về vị trí ban đầu
            .Pause(); // Tạm dừng tween ban đầu
    }

    void Update()
    {
        if (giveUpPanelContainer.activeSelf)
        {
            // Nếu giveUpPanelContainer active, tiếp tục chạy hiệu ứng
            if (!breakLeftTween.IsPlaying()) breakLeftTween.Play();
            if (!breakRightTween.IsPlaying()) breakRightTween.Play();
        }
        else
        {
            // Nếu giveUpPanelContainer không active, dừng hiệu ứng và reset vị trí
            if (breakLeftTween.IsPlaying()) breakLeftTween.Pause();
            if (breakRightTween.IsPlaying()) breakRightTween.Pause();

            breakLeftImage.transform.rotation = Quaternion.identity;
            breakRightImage.transform.rotation = Quaternion.identity;
        }
    }
}