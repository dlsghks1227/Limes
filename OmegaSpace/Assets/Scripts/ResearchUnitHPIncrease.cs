using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Research Unit HP increase", menuName = "Scriptable Object/Research/Unit HP Increase", order = 1)]
public class ResearchSomeUnitHPIncrease : ResearchBase
{
    [SerializeField]
    private UnitData[] specificUnits = new UnitData[10];
    [SerializeField]
    private float HPIncreasesRatio=1;
    [SerializeField]
    private int HPIncreaseValue=0;

    public override void DoResearch()
    {
        bool isTargetSpecificUnits = specificUnits[0] != null;
        if (isTargetSpecificUnits)
        {
            for (int i = 0; specificUnits[i]; i++)
            {
                TechManager.Instance.AdjustUnitHealthPoint(HPIncreaseValue, specificUnits[i]);
                TechManager.Instance.AdjustUnitHealthPoint(HPIncreasesRatio, specificUnits[i]);
            }
        }
        else
        {
            TechManager.Instance.AdjustUnitHealthPoint(HPIncreaseValue);
            TechManager.Instance.AdjustUnitHealthPoint(HPIncreasesRatio);
        }
    }
}
