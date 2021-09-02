using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackComponent : AttackComponent
{
    #region variables
    [SerializeField]
    protected float fireAngle = 45f;
    [SerializeField]
    protected float bulletSpeed = 1f;   
    [SerializeField]
    protected GameObject bulletPrefab;
    protected GameObject bullet;
    protected ObjectPool bulletPool;
    #endregion
    #region user function
    public override void Attack(GameObject target)
    {
        base.Attack(target);
        if (IsInRange(target.transform.position))
        {
            transform.LookAt(target.transform.position);
            Fire(target.transform.position);
        }
    }

    protected virtual void Fire(Vector3 targerPos)     
    {
        GetBullet();
        if (bullet)
        {
            bullet.GetComponent<ProjectileParabola>().SetHotwitzerInfo(targerPos, fireAngle);
            bullet.SetActive(true);
        }
    }

    protected void GetBullet()
    {
        if (!bulletPool)
            return;
        bullet = bulletPool.GetPooledObject(bulletPrefab.name);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Projectile>().SetProjectileInfo(modifiedAttackDamage, GetHitLayer(),armorPierce, bulletSpeed, gameObject);
    }

    protected int GetHitLayer()
    {
        if (gameObject.layer == 10)
            return 9;
        else
            return 10;
    }
    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();
        bulletPool = FindObjectOfType<ObjectPool>();
        
        if (!bulletPool.pooledObjectsList.ContainsKey(bulletPrefab.name))
            bulletPool.AddObject(bulletPrefab, bulletPrefab.name, 100);
    }
}


