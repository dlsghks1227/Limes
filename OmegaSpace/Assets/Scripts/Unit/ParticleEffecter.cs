using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using UnityEngine;

[Serializable]
public class ParticleEffecter
{
    [SerializeField]
    private List<GameObject> particlePrefabs = new List<GameObject>();
    private List<ParticleSystem> particleObject = new List<ParticleSystem>();

    public ParticleSystem PresentParticle
    {
        get;
        private  set;
    }


    private void AllocateParticleObj()
    {
        particleObject.Clear();
        foreach (var particle in particlePrefabs)
        {
            var obj = GameObject.Instantiate(particle, Vector3.zero, Quaternion.identity);
            var particleSystem = obj.GetComponent<ParticleSystem>();

            if (!particleSystem && particleObject.Contains(particleSystem))
                continue;

            particleObject.Add(particleSystem);
            particleSystem.Stop();
            obj.SetActive(false);
        }
    }

    public void ActivateParticle(Vector3 pos)
    {
        if (!PresentParticle)
            return;

        PresentParticle.gameObject.transform.position = pos;
        if (PresentParticle.gameObject.activeSelf)
            PresentParticle.gameObject.SetActive(false);
        PresentParticle.gameObject.SetActive(true);
        StopParticle();
        PresentParticle.Play(true);
    }

    public void StopParticle()
    {
        if (PresentParticle && PresentParticle.isPlaying)
            PresentParticle.Stop();
    }

    public void DeactiveParticleObject()
    {
        if(PresentParticle && PresentParticle.gameObject)
           PresentParticle.gameObject.SetActive(false);
    }

    public void SetParticleAs(int idx)
    {
        int presentParticleCnt = particleObject.Count;
        if (presentParticleCnt == 0 || !particleObject[presentParticleCnt-1])
            AllocateParticleObj();

        if (idx >= particleObject.Count || idx < 0)
            return;
        PresentParticle = particleObject[idx];
    }
}
