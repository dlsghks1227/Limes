using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class ProjectileParabola : Projectile
{
    protected Vector3 startPos;
    protected Vector3 targetPos;
    protected float angle;
    
    protected float vX;
    protected float vY;
    protected float elapseTime;

    /*
    [SerializeField]
    protected float trailMarkPeriod = 0.2f;
    [SerializeField]
    protected GameObject trailPrefab;

    protected IEnumerator AnimateProjectileTrail()
    {
        while (gameObject.activeSelf)
        {
            ProjectileTrailMarker.Instance.MakeTrail(transform.position, trailPrefab);
            yield return WaitTime.GetWaitForSecondOf(trailMarkPeriod);
        }
    }*/

    public virtual void SetHotwitzerInfo(Vector3 targetPos, float angle)
    {
        this.targetPos = targetPos;
        this.angle = angle;
    }

    protected void CalculateProjectileMove()
    {
        float targetDistance = Vector3.Distance(startPos, targetPos);
        float velocity = targetDistance / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / 9.8f);

        vX = Mathf.Sqrt(velocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
        vY = Mathf.Sqrt(velocity) * Mathf.Sin(angle * Mathf.Deg2Rad);

        transform.rotation = Quaternion.LookRotation(targetPos - startPos);

        elapseTime = 0;
    }

    protected void AdjustProjectilePos()
    {
        transform.Translate(0, (vY - (9.8f * elapseTime * speedCoefficient)) * Time.deltaTime , vX * Time.deltaTime * speedCoefficient );
        // transform.LookAt(new Vector3(0, (vY - (9.8f * elapseTime * speedCoefficient)) * Time.deltaTime, vX * Time.deltaTime * speedCoefficient));
        elapseTime += Time.deltaTime;
    }

    protected override void OnEnable()
    {
        startPos = transform.position;
        CalculateProjectileMove();

        if (particle != null)
        {
            particle.SetParticleAs(0);
            particle.ActivateParticle(transform.position);
        }

        ShowFireParticle();
        //StartCoroutine(AnimateProjectileTrail());
        StartCoroutine(DestroyProjectile());
    }


    protected void Update()
    {
        AdjustProjectilePos();
    }
}

