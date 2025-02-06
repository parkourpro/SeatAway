using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewFeaturePanelController : MonoBehaviour
{
    public SpriteImg spriteImg;
    public BusController busController;
    public GameObject newFeaturePanel;
    
    private GameObject newFeatureImage;
    private GameObject contentImage;

    private RectTransform newFeatureImageRect;
    private Vector2 initialFIPos;
    private bool isMoveIn = false;
    private void Awake()
    {
        newFeatureImage = newFeaturePanel.transform.Find("NewFeatureImage").gameObject;
        newFeatureImageRect = newFeatureImage.GetComponent<RectTransform>();
        contentImage = newFeatureImage.transform.Find("ContentImage").gameObject;
        initialFIPos = newFeatureImageRect.anchoredPosition;
        DeActivateNewFeaturePanel();
    }
    IEnumerator Start()
    {
        yield return new WaitUntil(() => GridManager.Instance.doneLevelData != "");
        int level = GridManager.Instance.GetLevel();
        yield return new WaitUntil(() => busController.busOpenDoor != "");
        ProcessLevel(level);
    }
    void Update()
    {
        if (isMoveIn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MoveOutFeatureImage();
            }
        }
    }
    void OnDestroy()
    {
        newFeatureImageRect.DOKill();
    }

    void ProcessLevel(int level)
    {
        switch (level)
        {
            case 7:
                SetFeatureImage(spriteImg.GetSprite(1));
                break;
            case 9:
                SetFeatureImage(spriteImg.GetSprite(2));
                break;
            case 11:
                SetFeatureImage(spriteImg.GetSprite(3));
                break;
            case 13:
                SetFeatureImage(spriteImg.GetSprite(4));
                break;
            case 15:
                break;
            default:
                break;
        }
    }


    void SetFeatureImage(Sprite sp)
    {
        ActivateNewFeaturePanel();
        contentImage.GetComponent<Image>().sprite = sp;
        MoveInFeatureImage();
    }

    void MoveInFeatureImage()
    {
        newFeatureImageRect.DOAnchorPosX(0, 0.7f)
            .SetEase(Ease.InOutBack)
            .OnComplete(()=>
            isMoveIn = true
        );
    }

    void MoveOutFeatureImage()
    {
        newFeatureImageRect.DOAnchorPosX(initialFIPos.x, 1f)
            .SetEase(Ease.InOutBack)
            .OnComplete(() =>
                DeActivateNewFeaturePanel()
        );
    }


    void ActivateNewFeaturePanel()
    {
        newFeaturePanel.SetActive(true);
    }
    void DeActivateNewFeaturePanel()
    {
        newFeaturePanel.SetActive(false);
    }
}
