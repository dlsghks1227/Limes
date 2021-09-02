using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Research Unit Damage  Increase",menuName ="Scriptable Object/Research/Unit Damage Increase",order =3)]
public class ResearchIncreaseUnitDamage : ResearchBase
{
    [SerializeField]
    private int damageIncreaseValue=0;
    [SerializeField]
    private float damageIncreaseRatio = 1;
    [SerializeField]
    private UnitData[] specificUnits = new UnitData[10];
    public override void DoResearch()
    {
        bool isTargetSpecificUnits = specificUnits[0] != null;
        if (isTargetSpecificUnits)
        {
            for (int i = 0; specificUnits[i]; i++)
            {
                TechManager.Instance.AdjustUnitAttackDamage(damageIncreaseValue, specificUnits[i]);
                TechManager.Instance.AdjustUnitAttackDamage(damageIncreaseRatio, specificUnits[i]);
            }
        }
        else
        {
            TechManager.Instance.AdjustUnitAttackDamage(damageIncreaseValue);
            TechManager.Instance.AdjustUnitAttackDamage(damageIncreaseRatio);
        }
    }
}
