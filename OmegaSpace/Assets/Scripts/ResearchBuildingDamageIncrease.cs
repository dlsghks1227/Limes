using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Research Building Damage increase", menuName = "Scriptable Object/Research/Building Damage Increase", order = 1)]
public class ResearchBuildingDamageIncrease : ResearchBase
{
    [SerializeField]
    private BuildingData[] specificBuilding = new BuildingData[10];
    [SerializeField]
    private float damageIncreaseRatio = 1;
    [SerializeField]
    private int damageIncreaseValue = 0;

    public override void DoResearch()
    {
        bool isTargerSpecificBuilding = specificBuilding[0] != null;
        if (isTargerSpecificBuilding)
        {
            for (int i = 0; specificBuilding[i]; i++)
            {
                TechManager.Instance.AdjustBuildAttackDamage(damageIncreaseValue, specificBuilding[i]);
                TechManager.Instance.AdjustBuildAttackDamage(damageIncreaseRatio, specificBuilding[i]);
            }
        }
        else
        {
            TechManager.Instance.AdjustBuildAttackDamage(damageIncreaseValue);
            TechManager.Instance.AdjustBuildAttackDamage(damageIncreaseRatio);
        }
    }
}
