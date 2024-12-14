using UnityEngine;
using DG.Tweening;
using System.Collections;
using TMPro;

public class DifficultyController : MonoBehaviour
{
    public GameObject level;       // DifficultyPanel/Level (Image1)
    public GameObject difficulty;  // DifficultyPanel/Difficulty (Image2)
    public GameObject panel;
    public TextMeshProUGUI levelNumber;

    private RectTransform levelRect;
    private RectTransform difficultyRect;

    [Header("Cài đặt chuyển động")]
    public float moveAmount = 0.3f;        // Lượng di chuyển ban đầu
    public float extraMoveAmount = 0.05f; // Lượng di chuyển thêm sau khi chờ
    public float delayTime = 0.7f;        // Thời gian chờ trước khi thực hiện chuyển động bổ sung

    [Header("Thời gian tween")]
    public float moveInDuration = 0.5f;   // Thời gian cho hiệu ứng đi vào
    public float moveBackDuration = 0.5f; // Thời gian cho hiệu ứng quay về vị trí cũ

    IEnumerator Start()
    {
        panel.SetActive(false);
        levelNumber.text = "Level " + PlayerPrefs.GetInt("UnlockLevel").ToString();
        yield return new WaitForSeconds(1);
        panel.SetActive(true);
        levelRect = level.GetComponent<RectTransform>();
        difficultyRect = difficulty.GetComponent<RectTransform>();

        // Di chuyển Image1 (level)
        Vector2 originalLevelAnchorMin = levelRect.anchorMin;
        Vector2 originalLevelAnchorMax = levelRect.anchorMax;

        Vector2 targetLevelAnchorMin = new Vector2(originalLevelAnchorMin.x + moveAmount, originalLevelAnchorMin.y);
        Vector2 targetLevelAnchorMax = new Vector2(originalLevelAnchorMax.x + moveAmount, originalLevelAnchorMax.y);

        Sequence levelSequence = DOTween.Sequence();
        levelSequence.Append(levelRect.DOAnchorMin(targetLevelAnchorMin, moveInDuration).SetEase(Ease.InOutQuad));
        levelSequence.Join(levelRect.DOAnchorMax(targetLevelAnchorMax, moveInDuration).SetEase(Ease.InOutQuad));
        levelSequence.AppendInterval(delayTime);
        levelSequence.Append(levelRect.DOAnchorMin(new Vector2(targetLevelAnchorMin.x + extraMoveAmount, targetLevelAnchorMin.y), moveInDuration).SetEase(Ease.InOutQuad));
        levelSequence.Join(levelRect.DOAnchorMax(new Vector2(targetLevelAnchorMax.x + extraMoveAmount, targetLevelAnchorMax.y), moveInDuration).SetEase(Ease.InOutQuad));
        levelSequence.Append(levelRect.DOAnchorMin(originalLevelAnchorMin, moveBackDuration).SetEase(Ease.InOutQuad));
        levelSequence.Join(levelRect.DOAnchorMax(originalLevelAnchorMax, moveBackDuration).SetEase(Ease.InOutQuad));

        // Di chuyển Image2 (difficulty)
        Vector2 originalDifficultyAnchorMin = difficultyRect.anchorMin;
        Vector2 originalDifficultyAnchorMax = difficultyRect.anchorMax;

        Vector2 targetDifficultyAnchorMin = new Vector2(originalDifficultyAnchorMin.x - moveAmount, originalDifficultyAnchorMin.y);
        Vector2 targetDifficultyAnchorMax = new Vector2(originalDifficultyAnchorMax.x - moveAmount, originalDifficultyAnchorMax.y);

        Sequence difficultySequence = DOTween.Sequence();
        difficultySequence.Append(difficultyRect.DOAnchorMin(targetDifficultyAnchorMin, moveInDuration).SetEase(Ease.InOutQuad));
        difficultySequence.Join(difficultyRect.DOAnchorMax(targetDifficultyAnchorMax, moveInDuration).SetEase(Ease.InOutQuad));
        difficultySequence.AppendInterval(delayTime);
        difficultySequence.Append(difficultyRect.DOAnchorMin(new Vector2(targetDifficultyAnchorMin.x - extraMoveAmount, targetDifficultyAnchorMin.y), moveInDuration).SetEase(Ease.InOutQuad));
        difficultySequence.Join(difficultyRect.DOAnchorMax(new Vector2(targetDifficultyAnchorMax.x - extraMoveAmount, targetDifficultyAnchorMax.y), moveInDuration).SetEase(Ease.InOutQuad));
        difficultySequence.Append(difficultyRect.DOAnchorMin(originalDifficultyAnchorMin, moveBackDuration).SetEase(Ease.InOutQuad));
        difficultySequence.Join(difficultyRect.DOAnchorMax(originalDifficultyAnchorMax, moveBackDuration).SetEase(Ease.InOutQuad));

        ////Chờ cả hai sequence hoàn thành
        //yield return DOTween.Sequence()
        //    .Append(levelSequence)
        //    .Join(difficultySequence)
        //    .WaitForCompletion();

        //// Tắt panel sau khi hiệu ứng hoàn tất
        //panel.SetActive(false);

        DOTween.Sequence().Append(levelSequence)
            .Join(difficultySequence)
            .OnComplete(() =>
            panel.SetActive(false)
        );
    }
}
