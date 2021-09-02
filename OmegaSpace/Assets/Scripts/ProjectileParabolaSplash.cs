using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ProjectileParabolaSplash : ProjectileParabola
 {
    [SerializeField]
    private AudioClip splashSound;
    private ObjectSoundManager soundManager;

    private float splashRadius;
    private int targetLayer;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == hitLayer || other.name.Equals("Plane") || other.name.Equals("Grid"))
        {
            
            Vector3 splashaAreaCenter = transform.position;
            SetTargetLayer();
            particle.SetParticleAs(1);
            if(particle.PresentParticle)
               particle.ActivateParticle(splashaAreaCenter);

            if(splashSound)
               soundManager.ForcePlay(splashSound);
           
            foreach (var a in Physics.SphereCastAll(splashaAreaCenter, splashRadius,Vector3.one, 0.1f ,targetLayer))
            {
                var unitEntity = a.collider.GetComponent<UnitEntity>();
                if (unitEntity)
                    unitEntity.TakeDamage(damage, armorPierce, gameObject);
                else
                    a.collider.GetComponent<BuildingEntity>().TakeDamage(damage, armorPierce, gameObject);
            }
        }
    }

    public void SetSplashRadius(float radius)
    {
        splashRadius = radius;
    }

    private void SetTargetLayer()
    {
        if (hitLayer == 9)
            targetLayer = Layers.PLAYER_UNIT_LAYER;
        else if (hitLayer == 10)
            targetLayer = Layers.ENEMY_UNIT_LAYER;
    }

    protected override void OnEnable()
    {
        startPos = transform.position;
        CalculateProjectileMove();
        ShowFireParticle();
        soundManager = GetComponent<ObjectSoundManager>();

        //StartCoroutine(AnimateProjectileTrail());
    }
}

