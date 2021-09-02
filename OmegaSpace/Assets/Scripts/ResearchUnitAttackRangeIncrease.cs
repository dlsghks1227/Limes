using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Research Unit Attack Range increase", menuName = "Scriptable Object/Research/Unit Attack Range Increase", order = 1)]
public class ResearchUnitAttackRangeIncrease : ResearchBase
{
    [SerializeField]
    private UnitData[] specificUnits = new UnitData[10];
    [SerializeField]
    private float attackRangeIncreaseRatio = 1;
    [SerializeField]
    private int attackRangeIncreaseValue = 0;

    public override void DoResearch()
    {
        bool isTargetSpecificUnits = specificUnits[0] != null;
        if (isTargetSpecificUnits)
        {
            for (int i = 0; specificUnits[i]; i++)
            {
                TechManager.Instance.AdjustUnitAttackRange(attackRangeIncreaseValue, specificUnits[i]);
                TechManager.Instance.AdjustUnitAttackRange(attackRangeIncreaseRatio, specificUnits[i]);
            }
        }
        else
        {
            TechManager.Instance.AdjustUnitAttackRange(attackRangeIncreaseValue);
            TechManager.Instance.AdjustUnitAttackRange(attackRangeIncreaseRatio);
        }
    }
}
