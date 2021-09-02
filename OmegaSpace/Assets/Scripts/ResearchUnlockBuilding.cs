using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Research Unlock Building", menuName = "Scriptable Object/Research/Unlock Building", order = 5)]
public class ResearchUnlockBuilding : ResearchBase
{
    [SerializeField]
    private BuildingData building;
    public override void DoResearch()
    {
        TechManager.Instance.AllowBuildGeneratePermission(building);
    }
}
