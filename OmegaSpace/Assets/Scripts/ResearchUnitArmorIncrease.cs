using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Research Unit Armor increase", menuName = "Scriptable Object/Research/Unit Armor Increase", order = 1)]
public class ResearchUnitArmorIncrease : ResearchBase
{
    [SerializeField]
    private UnitData[] specificUnits = new UnitData[10];
    [SerializeField]
    private float ArmorIncreasesRatio = 1;
    [SerializeField]
    private int ArmorIncreaseValue = 0;

    public override void DoResearch()
    {
        bool isTargetSpecificUnits = specificUnits[0] != null;
        if (isTargetSpecificUnits)
        {
            for (int i = 0; specificUnits[i]; i++)
            {
                TechManager.Instance.AdjustUnitArmorPoint(ArmorIncreaseValue, specificUnits[i]);
                TechManager.Instance.AdjustUnitArmorPoint(ArmorIncreasesRatio, specificUnits[i]);
            }
        }
        else
        {
            TechManager.Instance.AdjustUnitArmorPoint(ArmorIncreaseValue);
            TechManager.Instance.AdjustUnitArmorPoint(ArmorIncreasesRatio);
        }
    }
}
