using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class TechTree : MonoSingleton<TechTree>
{
    #region variables
    [SerializeField]
    private List<TechNode> techNodeList = new List<TechNode>();
    private HashSet<int> acheivedNodeIndexs = new HashSet<int>();
    public int[] AchievedNodeIndexs
    {
        get =>acheivedNodeIndexs.ToArray();
    }
    public ReadOnlyCollection<TechNode> TechNodeList
    {
        get => techNodeList.AsReadOnly();
    }
    [SerializeField]
    private TechNode rootNode;
    public TechNode RootNode
    {
        get => rootNode;
    }
    private TechNode currentSelectedNode;
    private bool isOnResearching;
    public bool IsOnResearching
    {
        get => isOnResearching;
    }

    private IEnumerator researchThreadOnNode;
    private IEnumerator researchThreadOnTechTree;
    #endregion
    public TechNode GetTechNodeBy(string name)
    {
        foreach (var a in techNodeList)
            if (a.TechName.Equals(name))
                return a;
        return null;
    }

    public void AddTechNode(TechNode techNode)
    {
        if (!techNodeList.Contains(techNode))
            techNodeList.Add(techNode);
    }

    public bool CancelCurretnResearching()
    {
        if (isOnResearching && !currentSelectedNode.IsAcheived)
        {
            StopCoroutine(researchThreadOnNode);
            StopCoroutine(researchThreadOnTechTree);
            RefundResearchCost();
            return true;
        }
        return false;

    }

    private void RefundResearchCost()
    {
        PlayerDataManager.Instance.AddPlayerResource(currentSelectedNode.IronCost);
        PlayerDataManager.Instance.AddPlayerResource(currentSelectedNode.GoldCost);
    }

    public void DoResearch(TechNode node, ResourceAmount retainIron, ResourceAmount retainGold)
    {
        currentSelectedNode = node;
        if (IsSelectedNodeResearchable(retainIron, retainGold))
        {
            researchThreadOnTechTree = StartResearch(retainIron, retainGold);
            StartCoroutine(researchThreadOnTechTree);
        }
    }

    public void DoResearch(string techName, ResourceAmount retainIron, ResourceAmount retainGold)
    {
        currentSelectedNode = GetTechNodeBy(techName);
        if (IsSelectedNodeResearchable(retainIron, retainGold))
        {
            researchThreadOnTechTree = StartResearch(retainIron, retainGold);
            StartCoroutine(researchThreadOnTechTree);
        }
    }

    private bool IsSelectedNodeResearchable(ResourceAmount retainIron, ResourceAmount retainGold)
    {
        return (currentSelectedNode && currentSelectedNode.IsResearchable && IsAffordableToResearch(retainIron, retainGold));
    }

    private bool IsAffordableToResearch(ResourceAmount retainIron, ResourceAmount retainGold)
    {
        bool isEnoughIron = retainIron >= currentSelectedNode.IronCost;
        bool isEnoughGold = retainGold >= currentSelectedNode.GoldCost;
        if (isEnoughIron && isEnoughGold)
            return true;
        return false;
    }

    private IEnumerator StartResearch(ResourceAmount retainIron, ResourceAmount retainGold)
    {
        isOnResearching = true;
        researchThreadOnNode = currentSelectedNode.DoResearch(retainIron, retainGold);
        PurchaseReseachCost();
        StartCoroutine(researchThreadOnNode);
        yield return WaitTime.GetWaitForSecondOf(currentSelectedNode.researchTime);
        isOnResearching = false;

        acheivedNodeIndexs.Add(techNodeList.IndexOf(currentSelectedNode));
    }

    private void PurchaseReseachCost()
    {
        PlayerDataManager.Instance.SubstractPlayerResource(currentSelectedNode.IronCost);
        PlayerDataManager.instance.SubstractPlayerResource(currentSelectedNode.GoldCost);
    }
}