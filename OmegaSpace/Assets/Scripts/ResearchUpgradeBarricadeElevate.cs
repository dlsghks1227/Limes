using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Research Barricade Elevate", menuName = "Scriptable Object/Barriacade Elevate", order = 1)]
public class ResearchUpgradeBarricadeElevate : ResearchBase
{
    public override void DoResearch()
    {
        TechManager.Instance.UpgradeBarricadeElevate();
    }
}
