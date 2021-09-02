using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTechTree: MonoBehaviour
{
    public ResourceAmount poorIron = new ResourceAmount(EResource.RES_IRON, 0);
    public ResourceAmount poorGold = new ResourceAmount(EResource.RES_GOLD, 0);
    public ResourceAmount muchIron = new ResourceAmount(EResource.RES_IRON, 9999999);
    public ResourceAmount muchGold = new ResourceAmount(EResource.RES_GOLD, 9999999);
    public ResourceAmount poorNull = new ResourceAmount(EResource.RES_NONE, 0);
    public ResourceAmount muchNull = new ResourceAmount(EResource.RES_NONE, 9999999);

    public List<TechNode> testNode = new List<TechNode>();
    public string nameForGetUnitData;
    public GameObject unitForGetUnitData;
    private TechManager techManager;
    private TechTree techTree;

    public IEnumerator ResearchEveryNode()
    {
        foreach (var a in techTree.TechNodeList)
        {
            while (true)
                if (techTree.IsOnResearching)
                {
                    yield return WaitTime.GetWaitForSecondOf(1f);
                    continue;
                }
                else
                    break;
            techTree.DoResearch(a,  muchIron,  muchGold);
        }
    }

    public void TestGetUnitDataFromName()
    {
        Debug.Log("find unitdata from name  input name: " + nameForGetUnitData + " output: " + techManager.GetUnitDataFrom(nameForGetUnitData));
    }

    public void TestGetUnitDataFromUnit()
    {
        Debug.Log("find unitdata from unit  input obj: " + unitForGetUnitData + "  output: " + techManager.GetUnitDataFrom(unitForGetUnitData));
    }

    public void PurchaseResearchCostWithPoorGold()
    {
        Debug.Log("before iron : " + muchIron.resAmount + " iron cost: " + testNode[0].IronCost.resAmount);
        Debug.Log("before gold : " + poorGold.resAmount + " gold cost: " + testNode[0].GoldCost.resAmount);
        Debug.Log("node achieved before research?: " + testNode[0].IsAcheived);
        TechTree.Instance.DoResearch(testNode[0], muchIron, poorGold);
        Debug.Log("reserching is begin? :" + TechTree.Instance.IsOnResearching);
        Debug.Log("after iron : " + muchIron.resAmount + " after gold : " + poorGold.resAmount);
    }

    public void PurchaseResearchWithPoorBoth()
    {
        Debug.Log("before iron : " + poorIron.resAmount + " iron cost: " + testNode[0].IronCost.resAmount);
        Debug.Log("before gold : " + poorGold.resAmount + " gold cost: " + testNode[0].GoldCost.resAmount);
        Debug.Log("node achieved before research?: " + testNode[0].IsAcheived);
        TechTree.Instance.DoResearch(testNode[0],  poorIron,  poorGold);
        Debug.Log("reserching is begin? :" + TechTree.Instance.IsOnResearching);
        Debug.Log("after iron : " + poorIron.resAmount + " after gold : " + poorGold.resAmount);
    }

    public void PurchaseResearchWithEnoughBoth()
    {
        Debug.Log("before iron : " + muchIron.resAmount + " iron cost: " + testNode[0].IronCost.resAmount);
        Debug.Log("before gold : " + muchGold.resAmount + " gold cost: " + testNode[0].GoldCost.resAmount);
        Debug.Log("node achieved before research?: " + testNode[0].IsAcheived);
        TechTree.Instance.DoResearch(testNode[0],  muchIron,  muchGold);
        Debug.Log("reserching is begin? :" + TechTree.Instance.IsOnResearching);
        Debug.Log("after iron : " + muchIron.resAmount + " after gold : " + muchGold.resAmount);
    }

    public void PurchaseResearchAlreadyDoneNode()
    {

    }

    public void PurchaseUnResearchableNode()
    {

    }


    private void Start()
    {
        techManager = TechManager.Instance;
        techTree = TechTree.Instance;
        //StartCoroutine(ResearchEveryNode());
        //TestGetUnitDataFromName();
        //TestGetUnitDataFromUnit();
        //PurchaseResearchCostWithPoorGold();
    }
}
