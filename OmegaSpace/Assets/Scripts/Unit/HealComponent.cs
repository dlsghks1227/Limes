using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealComponent : AttackComponent
{
    #region user function
    public override void Attack(GameObject target)
    {
        if (IsInRange(target.transform.position))
            target.GetComponent<UnitEntity>().TakeHeal(attackDamage);
    }
    #endregion
}
