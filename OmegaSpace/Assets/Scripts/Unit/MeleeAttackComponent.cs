using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackComponent : AttackComponent
{
    [SerializeField]
    private ParticleEffecter attackParticle;
    #region user function
    public override void Attack(GameObject target)
    {
        var pos = target.transform.position;
        base.Attack(target);
        if (IsInRange(pos))
        {
            var particlePos = transform.position + (pos - transform.position) * 0.5f;
            attackParticle.SetParticleAs(0);
            attackParticle.ActivateParticle(particlePos);
            var unitEntity = target.GetComponent<UnitEntity>();
            if (unitEntity)
                unitEntity.TakeDamage(modifiedAttackDamage, 0, gameObject);
            else
            {
                var buildingEntity = target.GetComponent<BuildingEntity>();
                buildingEntity.TakeDamage(modifiedAttackDamage, 0, gameObject);
            }
        }
    }
    #endregion
}

