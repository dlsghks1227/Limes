using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Research Unit Move Speed Increase", menuName = "Scriptable Object/Research/Unit Move Speed Increase", order = 1)]
public class ResearchUnitMoveSpeedIncrease : ResearchBase
{
    [SerializeField]
    private UnitData[] specificUnits = new UnitData[10];
    [SerializeField]
    private float moveSpeedIncreaseRatio = 1;
    [SerializeField]
    private int moveSpeedIncreaseValue = 0;

    public override void DoResearch()
    {
        bool isTargetSpecificUnits = specificUnits[1] == null;
        if (isTargetSpecificUnits)
        {
            for (int i = 0; specificUnits[i]; i++)
            {
                TechManager.Instance.AdjustUnitMoveSpeed(moveSpeedIncreaseValue, specificUnits[i]);
                TechManager.Instance.AdjustUnitMoveSpeed(moveSpeedIncreaseRatio, specificUnits[i]);
            }
        }
        else
        {
            TechManager.Instance.AdjustUnitMoveSpeed(moveSpeedIncreaseValue);
            TechManager.Instance.AdjustUnitMoveSpeed(moveSpeedIncreaseRatio);
        }
    }
}
