using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Research Unlock Unit",menuName ="Scriptable Object/Research/Unlock Unit",order = 5)]
public class ResearchUnlockUnit : ResearchBase
{
    [SerializeField]
    private UnitData additionalUnit;
    public override void DoResearch()
    {
       TechManager.Instance.AllowUnitGeneatePermission(additionalUnit);
    }
}
