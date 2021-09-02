using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.Rendering;


class MeleeSplashAttackComponent : AttackComponent
{
    [SerializeField]
    private float splashRadius;

    [SerializeField]
    private ParticleEffecter particle;

    public override void Attack(GameObject target)
    {
        base.Attack(target);
        if (IsInRange(target.transform.position))
        {
            particle.ActivateParticle(transform.position);
            foreach (var a in Physics.SphereCastAll(transform.position, splashRadius,Vector3.one, 0.1f, GetHitLayer()))
            {
                if (a.collider.CompareTag("Unit"))
                    target.GetComponent<UnitEntity>().TakeDamage(modifiedAttackDamage, 0, gameObject);
                else if (a.collider.CompareTag("Building"))
                    target.GetComponent<BuildingEntity>().TakeDamage(modifiedAttackDamage, 0, gameObject);
            }
        }
    }

    protected int GetHitLayer()
    {
        if (gameObject.layer == 10)
            return Layers.PLAYER_UNIT_LAYER;
        else
            return Layers.ENEMY_UNIT_LAYER;
    }
}

