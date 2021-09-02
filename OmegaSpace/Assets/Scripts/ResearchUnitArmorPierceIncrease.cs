using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Research Unit Armor Pierce increase", menuName = "Scriptable Object/Research/Unit Armor Pierce Increase", order = 1)]

public class ResearchUnitArmorPierceIncrease : ResearchBase
{
    [SerializeField]
    private UnitData[] specificUnits = new UnitData[10];
    [SerializeField]
    private float armorPierceIncreaseValue = 0;

    public override void DoResearch()
    {
        bool isTargetSpecificUnits = specificUnits[0] != null;
        if (isTargetSpecificUnits)
        {
            for (int i = 0; specificUnits[i]; i++)
            {
                TechManager.Instance.AdjustUnitArmorPierce(armorPierceIncreaseValue, specificUnits[i]);
                TechManager.Instance.AdjustUnitArmorPierce(armorPierceIncreaseValue, specificUnits[i]);
            }
        }
        else
        {
            TechManager.Instance.AdjustUnitArmorPierce(armorPierceIncreaseValue);
            TechManager.Instance.AdjustUnitArmorPierce(armorPierceIncreaseValue);
        }
    }
}
