using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSplashAttackComponent : RangeAttackComponent
{
    #region variables
    public float splashRadius = 0;
    private int targetLayer;
    #endregion
    #region user function
    protected override void Fire(Vector3 targerPos)
    {
        GetBullet();
        if (bullet)
        {
            Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();
            var projectile = bullet.GetComponent<ProjectileParabolaSplash>();
            projectile.SetHotwitzerInfo(targerPos, fireAngle);
            projectile.SetSplashRadius(splashRadius);
            bullet.SetActive(true);
        }
    }
    #endregion
}


