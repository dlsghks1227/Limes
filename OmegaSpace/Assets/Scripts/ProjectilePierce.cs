using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePierce : Projectile
{
    #region variables
    public int maxPiercingCount=5;
    private int piercingCount;
    #endregion
    #region user function
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(hitLayer))
        {
            if (other.gameObject.CompareTag("Unit"))
            {
                var entity = other.GetComponent<UnitEntity>();
                if (!entity)
                    return;
                entity.TakeDamage(damage, armorPierce);
            }
            else
            {
                var entity = other.GetComponent<BuildingEntity>();
                if (!entity)
                    return;
                entity.TakeDamage(damage);
            }
                    
            piercingCount++;
            DestoryIfMaxPierce();
        }
    }

    private void DestoryIfMaxPierce()
    {
        if (piercingCount >= maxPiercingCount)
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
    }
    #endregion
    private void OnEnable()
    {
        StartCoroutine(DestroyProjectile());
        piercingCount = 0;
    }
}