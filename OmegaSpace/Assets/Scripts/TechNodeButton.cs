using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.UI;
using UnityEngine;

public class TechNodeButton : MonoBehaviour
{
    [SerializeField]
    private TechNode techNode;
    public TechNode TechNode
    {
        get => techNode;
    }
    private Image buttonImage;
    [SerializeField]
    private Sprite techSymbolImage;
    public Sprite TechSymbolImage
    {
        get => techSymbolImage;
    }
    [SerializeField]
    private string techDescribe;
    public string TechDescribe
    {
        get => techDescribe;
    }

    private float gradationValuePerSec;
    private Color OriginColor;
    public void Research()
    {
        PlayerDataManager playerData = PlayerDataManager.Instance;
        bool isEnoughResource = playerData.IsAffordableToPay(techNode.GoldCost) && playerData.IsAffordableToPay(techNode.IronCost);
        if (techNode.IsResearchable && isEnoughResource)
        {
            var playerIron = playerData.GetPlayerResourceOf(EResource.RES_IRON);
            var playerGold = playerData.GetPlayerResourceOf(EResource.RES_GOLD);
            TechTree.Instance.DoResearch(techNode, playerIron, playerGold);
            StartResearchingAnimation();
        }
    }

    private void StartResearchingAnimation()
    {
        OriginColor = buttonImage.color;
        gradationValuePerSec = buttonImage.color.r / (techNode.researchTime * 2);
        StartCoroutine(ShowResearchingAnimation());
    }

    private IEnumerator ShowResearchingAnimation()
    {
        while (true)
        {
            if (buttonImage.color.r > 0)
            {
                float afterRedValue = buttonImage.color.r - gradationValuePerSec;
                Color afterColor = new Color(afterRedValue, buttonImage.color.g, buttonImage.color.b);
                buttonImage.color = afterColor;
            }
            else
                break;
            yield return WaitTime.GetWaitForSecondOf(0.5f);
        }
    }

    public void CancelResearchingAnimation()
    {
        StopAllCoroutines();
        buttonImage.color = OriginColor;
    }

    public void SelecteNode()
    {
        GetTechLayout().SetCurrentNode(this);
    }

    private TechLayout GetTechLayout()
    {
        Transform parent = transform.parent;
        while (parent)
        {
            var techLayout = parent.GetComponent<TechLayout>();
            if (techLayout)
                return techLayout;
            parent = parent.parent;
        }
        return null;
    }

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = techSymbolImage;
    }
}
