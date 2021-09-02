using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Research Unit Attack Speed increase", menuName = "Scriptable Object/Research/Unit Attack Speed Increase", order = 1)]
public class ResearchUnitAttackSpeedIncrease : ResearchBase
{
    [SerializeField]
    private UnitData[] specificUnits = new UnitData[10];
    [SerializeField]
    private float attackSpeedIncreaseRatio = 1;
    [SerializeField]
    private int attackSpeedIncreaseValue = 0;

    public override void DoResearch()
    {
        bool isTargetAllUnit = specificUnits[0] == null ;
        if (isTargetAllUnit)
        {
           TechManager.Instance.AdjustUnitAttackSpeed(attackSpeedIncreaseValue);
           TechManager.Instance.AdjustUnitAttackSpeed(attackSpeedIncreaseRatio);
        }
        else
        {
            for (int i = 0; specificUnits[i]; i++)
            {
               TechManager.Instance.AdjustUnitAttackSpeed(attackSpeedIncreaseValue, specificUnits[i]);
               TechManager.Instance.AdjustUnitAttackSpeed(attackSpeedIncreaseRatio, specificUnits[i]);
            }
        }
    }
}
