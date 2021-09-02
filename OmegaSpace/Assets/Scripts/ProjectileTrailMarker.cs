using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class ProjectileTrailMarker : MonoSingleton<ProjectileTrailMarker>
{
    private ObjectPool objectPool;
  
    public void MakeTrail(Vector3 pos, GameObject particlePrefab, float remainTime = 0.2f, bool isInStaticPos = false)
    {
        StartCoroutine(MarkTrail(pos, particlePrefab, remainTime, isInStaticPos));
    }

    private IEnumerator MarkTrail(Vector3 pos, GameObject particlePrefab, float remainTime, bool isInStaticPos)
    {
        var trail = objectPool.GetPooledObject(particlePrefab.name);
        if (!trail)
        {
            AddTrailParticleToObjectPool(particlePrefab);
            trail = objectPool.GetPooledObject(particlePrefab.name);
        }

        if(!isInStaticPos)
        {
            float dx = UnityEngine.Random.Range(-1f, 1f);
            float dy = UnityEngine.Random.Range(-1f, 1f);
            float dz = UnityEngine.Random.Range(-1f, 1f);

            pos.x -= dx;
            pos.y -= dy;
            pos.z -= dz;
        }

        trail.transform.position = pos;
        trail.SetActive(true);
        yield return WaitTime.GetWaitForSecondOf(remainTime);
        trail.SetActive(false);
    }

    private void AddTrailParticleToObjectPool(GameObject particlePrefab)
    {
        if(!objectPool.IsContainObject(particlePrefab.name))
           objectPool.AddObject(particlePrefab, particlePrefab.name, 100);
    }

    private void Awake()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }
}
