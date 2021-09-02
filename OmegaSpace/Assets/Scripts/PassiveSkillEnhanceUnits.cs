using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Passive Skill Enhance Unit Damage", menuName = "Scriptable Object/Passive Skill/Enhance Unit Damage", order = 1)]
public class PassiveEnhanceUnits : PassiveSkillBase
{
    [SerializeField]
    private float unitDamageIncreaseValue = 15f;

    public override void ApplyPassiveSkillEffect()
    {
        base.ApplyPassiveSkillEffect();
        TechManager.Instance.AdjustUnitAttackDamage(unitDamageIncreaseValue);
    }

}
