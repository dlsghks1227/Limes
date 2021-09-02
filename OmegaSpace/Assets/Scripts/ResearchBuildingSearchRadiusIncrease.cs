using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Research Building Search Radius Increase", menuName = "Scriptable Object/Research/Building Search Radius Increase", order = 1)]
public class ResearchBuildingSearchRadiusIncrease : ResearchBase
{
    [SerializeField]
    private BuildingData[] specificBuilding = new BuildingData[10];
    [SerializeField]
    private float searchRadiusIncreaseRatio = 1;
    [SerializeField]
    private int searchRadiusIncreaseValue = 0;

    public override void DoResearch()
    {
        bool isTargerSpecificBuilding = specificBuilding[0] != null;
        if (isTargerSpecificBuilding)
        {
            for (int i = 0; specificBuilding[i]; i++)
            {
                TechManager.Instance.AdjustBuildSearchRadius(searchRadiusIncreaseValue, specificBuilding[i]);
                TechManager.Instance.AdjustBuildSearchRadius(searchRadiusIncreaseRatio, specificBuilding[i]);
            }
        }
        else
        {
            TechManager.Instance.AdjustBuildSearchRadius(searchRadiusIncreaseValue);
            TechManager.Instance.AdjustBuildSearchRadius(searchRadiusIncreaseRatio);
        }
    }
}
