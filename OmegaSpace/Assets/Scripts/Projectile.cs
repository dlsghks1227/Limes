using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region variables
    protected int damage;
    protected float armorPierce = 0;
    protected int hitLayer;
    protected float speedCoefficient;
    protected GameObject attacker;

    [SerializeField]
    protected float projectileRemainTime = 3f;

    [SerializeField]
    protected ParticleEffecter particle;
    #endregion
    #region user function
    public void SetProjectileInfo(int dmg, int hitLayer, float armorPierce, float speedCoefficient, GameObject attacker)
    {
        damage = dmg;
        this.hitLayer = hitLayer;
        this.armorPierce = armorPierce;
        this.attacker = attacker;
        this.speedCoefficient = speedCoefficient;
    }

    protected IEnumerator DestroyProjectile()
    {
        yield return WaitTime.GetWaitForSecondOf(projectileRemainTime);
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(hitLayer))
        {

            var unitEntity = other.GetComponent<UnitEntity>();
            if (unitEntity)
                other.GetComponent<UnitEntity>().TakeDamage(damage, armorPierce, attacker);
            else
            {
                var buildingEntity = other.GetComponent<BuildingEntity>();
                if (buildingEntity)
                    buildingEntity.TakeDamage(damage, armorPierce, attacker);
                else
                    return;
            }

            particle.SetParticleAs(1);
            if (particle.PresentParticle)
                particle.ActivateParticle(transform.position);

            StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }

    protected void ShowFireParticle()
    {
        particle.SetParticleAs(0);
        if (particle.PresentParticle)
            particle.ActivateParticle(transform.position);
    }
    #endregion
    protected virtual void OnEnable()
    {
        ShowFireParticle();
        StartCoroutine(DestroyProjectile());
    }
}