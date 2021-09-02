using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Research Building Armor increase", menuName = "Scriptable Object/Research/Building Armor Increase", order = 1)]
public class ResearchBuildingArmorIncrease : ResearchBase
{
    [SerializeField]
    private BuildingData[] specificBuilding = new BuildingData[10];
    [SerializeField]
    private float armorIncreasesRatio = 1;
    [SerializeField]
    private int armorIncreaseValue = 0;

    public override void DoResearch()
    {
        bool isTargerSpecificBuilding = specificBuilding[0] != null;
        if (isTargerSpecificBuilding)
        {
            for (int i = 0; specificBuilding[i]; i++)
            {
                TechManager.Instance.AdjustBuildArmorPoint(armorIncreaseValue, specificBuilding[i]);
                TechManager.Instance.AdjustBuildArmorPoint(armorIncreasesRatio, specificBuilding[i]);
            }
        }
        else
        {
            TechManager.Instance.AdjustBuildArmorPoint(armorIncreaseValue);
            TechManager.Instance.AdjustBuildArmorPoint(armorIncreasesRatio);
        }
    }
}
