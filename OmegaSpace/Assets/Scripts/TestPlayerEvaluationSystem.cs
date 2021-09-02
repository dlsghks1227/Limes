using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerEvaluationSystem : MonoBehaviour
{
    private PlayEvaluationSystemUnitPart playEvaluateUnitPart;
    private PlayEvaluationSystemTechPart playEvaluateTechPart;
    public GameObject[] unitPrefab=new GameObject[10];
    public int numberOfExampleUnits = 10;

    #region unit part
    public void GenerateExampleUnits()
    {
        TechManager tech =TechManager.Instance;
        for (int i = 0; unitPrefab[i]; i++)
            for (int j = 0; j < numberOfExampleUnits; j++)
                tech.AddPresentUnit(Instantiate(unitPrefab[i], new Vector3(i*20,0,0),Quaternion.identity));
    }

    public void TestGetUnitRatio()
    {
        if (playEvaluateUnitPart.UnitRatioByUnitType != null)
            foreach (var a in playEvaluateUnitPart.UnitRatioByUnitType)
                Debug.Log(a.Key + "'s ratio is " + a.Value);
    }

    public void TestGetTotalUnitCnt()
    {
        Debug.Log("present total unit count is "+playEvaluateUnitPart.TotalUnitCnt);
    }

    public void TestGetTotalUnitDamage()
    {
        Debug.Log("present total attack damage is " + playEvaluateUnitPart.TotalAttackDamage);
    }

    public void TestGetTotalMobility()
    {
        Debug.Log("present total move speed is " + playEvaluateUnitPart.TotalUnitMobility);
    }

    public void TestGetTotalUnitHealth()
    {
        Debug.Log("present total unit HP is " + playEvaluateUnitPart.TotalUnitHealthPoint);
    }

    public void TestGetTotalUnitAP()
    {
        Debug.Log("present total unit AP is " + playEvaluateUnitPart.TotalUnitArmorPoint);
    }
    #endregion
    #region tech part
    public void TestGetResearchRatio()
    {
        foreach (var a in playEvaluateTechPart.ResearchedRatioByTechTrait)
            Debug.Log(a.Key + "'s ratio is " + a.Value);
    }
    #endregion
    private void OnEnable()
    {
        playEvaluateUnitPart = PlayEvaluationSystemUnitPart.Instance;
        playEvaluateTechPart = PlayEvaluationSystemTechPart.Instance;
        //TestGetResearchRatio();
        // GenerateExampleUnits();
        //TestGetTotalUnitCnt();
        //TestGetUnitRatio();
        //TestGetTotalMobility();
        //TestGetTotalUnitDamage();
        //TestGetTotalUnitHealth();
        //TestGetTotalUnitAP();
    }
}
