using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Research Building HP Increase", menuName = "Scriptable Object/Research/Building HP Increase", order = 1)]
public class ResearchBuildingHealthPointIncrease : ResearchBase
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
                TechManager.Instance.AdjustBuildHealthPoint(armorIncreaseValue, specificBuilding[i]);
                TechManager.Instance.AdjustBuildHealthPoint(armorIncreasesRatio, specificBuilding[i]);
            }
        }
        else
        {
            TechManager.Instance.AdjustBuildHealthPoint(armorIncreaseValue);
            TechManager.Instance.AdjustBuildHealthPoint(armorIncreasesRatio);
        }
    }
}
