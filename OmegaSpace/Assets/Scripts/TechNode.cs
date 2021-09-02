using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public enum ETechTrait
{
    offese,
    defense,
    extend,
    neutral
}


public abstract class ResearchBase : ScriptableObject
{
    public abstract void DoResearch();
}


[CreateAssetMenu(fileName ="TechNode",menuName ="Scriptable Object/Tech Node",order = 2)]
public class TechNode : ScriptableObject
{
    [SerializeField]
    private List<ResearchBase> researchList = new List<ResearchBase>();
    [SerializeField]
    private ETechTrait trait;
    public ETechTrait Trait
    {
        get => trait;
    }
    [SerializeField]
    private string techName;
    public string TechName
    {
        get => techName;
    }
   
    [SerializeField]
    private ResourceAmount ironCost = new ResourceAmount(EResource.RES_IRON, 0);
    public ResourceAmount IronCost
    {
        get => ironCost;
    }
    [SerializeField]
    private ResourceAmount goldCost = new ResourceAmount(EResource.RES_GOLD, 0);
    public ResourceAmount GoldCost
    {
        get => goldCost;
    }
   
    [SerializeField]
    private bool isAchieved = false;
    public bool IsAcheived
    {
        get => isAchieved;
    }
    public float researchTime;
    [SerializeField]
    private List<TechNode> requireNodes = new List<TechNode>();
    public ReadOnlyCollection<TechNode> RequireNodes
    {
        get => requireNodes.AsReadOnly();
    }
    [SerializeField]
    private List<TechNode> descendantNodes = new List<TechNode>();
    public ReadOnlyCollection<TechNode> DescendantNodes
    {
        get => descendantNodes.AsReadOnly();
    }
    public bool IsResearchable
    {
        get => (!IsAcheived && AreRequireNodesResearached);
    }
    private bool AreRequireNodesResearached
    {
        get
        {
            foreach (var a in requireNodes)
                if (!a.IsAcheived)
                    return false;
            return true;
        }
    }

    public void AddRequireNode(TechNode node)
    {
        if (!requireNodes.Contains(node))
            requireNodes.Add(node);
    } 

    public void AddDescendantNode(TechNode node)
    {
        if (!requireNodes.Contains(node))
            descendantNodes.Add(node);
    }

    public void DeleteRequireNode(TechNode node)
    {
        if (requireNodes.Contains(node))
            requireNodes.Remove(node);
    }

    public void DeleteDescendantNode(TechNode node)
    {
        if (descendantNodes.Contains(node))
            descendantNodes.Remove(node);
    }

    public IEnumerator DoResearch(ResourceAmount retainIron, ResourceAmount retainGold)
    {
        if (!isAchieved && AreRequireNodesResearached && IsAffordableToResearch(retainIron, retainGold))
        {
            yield return WaitTime.GetWaitForSecondOf(researchTime);
            foreach (var a in researchList)
                a.DoResearch();
            isAchieved = true;
        }
    }

    public bool IsAffordableToResearch(ResourceAmount retainIron, ResourceAmount retainGold)
    {
        return retainIron >= ironCost && retainGold >= GoldCost;
    }
}
