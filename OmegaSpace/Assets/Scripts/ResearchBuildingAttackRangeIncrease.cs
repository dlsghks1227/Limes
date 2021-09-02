using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Research Building Attack Range increase", menuName = "Scriptable Object/Research/Building Attack Range Increase", order = 1)]
public class ResearchBuildingAttackRangeIncrease : ResearchBase
{
    [SerializeField]
    private BuildingData[] specificBuilding = new BuildingData[10];
    [SerializeField]
    private float rangeIncreaseRatio = 1;
    [SerializeField]
    private int rangeIncreaseValue = 0;

    public override void DoResearch()
    {
        bool isTargerSpecificBuilding = specificBuilding[0] != null;
        if (isTargerSpecificBuilding)
        {
            for (int i = 0; specificBuilding[i]; i++)
            {
                TechManager.Instance.AdjustBuildAttackRange(rangeIncreaseValue, specificBuilding[i]);
                TechManager.Instance.AdjustBuildAttackRange(rangeIncreaseRatio, specificBuilding[i]);
            }
        }
        else
        {
            TechManager.Instance.AdjustBuildAttackRange(rangeIncreaseValue);
            TechManager.Instance.AdjustBuildAttackRange(rangeIncreaseRatio);
        }
    }
}
