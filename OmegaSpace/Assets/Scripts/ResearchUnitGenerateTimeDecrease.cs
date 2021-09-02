using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Research Unit Generate Time Decrease", menuName = "Scriptable Object/Research/Unit Generate Time Decrease", order = 1)]
public class ResearchUnitGenerateTimeDecrease : ResearchBase
{
    [SerializeField]
    private UnitData[] specificUnits = new UnitData[10];
    [SerializeField]
    private float timeDecreaseRatio = 0f;
    [SerializeField]
    private int timeDecreaseValue = 0;
    public override void DoResearch()
    {
        bool isTargetSpecificUnits = specificUnits[0] != null;
        if (isTargetSpecificUnits)
        {
            for (int i = 0; specificUnits[i]; i++)
            {
                TechManager.Instance.AdjustUnitGenerateTime(-timeDecreaseValue, specificUnits[i]);
                TechManager.Instance.AdjustUnitGenerateTime((1-timeDecreaseRatio), specificUnits[i]);
            }
        }
        else
        {
            TechManager.Instance.AdjustUnitGenerateTime(-timeDecreaseValue);
            TechManager.Instance.AdjustUnitGenerateTime((1 - timeDecreaseRatio));
        }
    }
}
