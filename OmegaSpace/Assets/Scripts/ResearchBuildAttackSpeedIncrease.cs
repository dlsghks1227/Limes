using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Research Building Attack Speed increase", menuName = "Scriptable Object/Research/Building Attack Speed Increase", order = 1)]
public class ResearchBuildAttackSpeedIncrease : ResearchBase
{
    [SerializeField]
    private BuildingData[] specificBuilding = new BuildingData[10];
    [SerializeField]
    private float attackSpeedIncreaseRatio = 1;
    [SerializeField]
    private int attackSpeedIncreaseValue = 0;

    public override void DoResearch()
    {
        bool isTargerSpecificBuilding = specificBuilding[0] != null;
        if (isTargerSpecificBuilding)
        {
            for (int i = 0; specificBuilding[i]; i++)
            {
                TechManager.Instance.AdjustBuildAttackSpeed(attackSpeedIncreaseValue, specificBuilding[i]);
                TechManager.Instance.AdjustBuildAttackSpeed(attackSpeedIncreaseRatio, specificBuilding[i]);
            }
        }
        else
        {
            TechManager.Instance.AdjustBuildAttackSpeed(attackSpeedIncreaseValue);
            TechManager.Instance.AdjustBuildAttackSpeed(attackSpeedIncreaseRatio);
        }
    }
}
