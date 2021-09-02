using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDeployer : MonoBehaviour
{
    [SerializeField]
    private GameObject domeBarrierPrefab;
    private GameObject domeBarrier;
    private WaitUntil barrierDestroyListner;

    [SerializeField]
    private float barrierRegenCoolTime;
    public bool IsBarrierOn
    {
        private set;
        get;
    }

    public void OnBarrier()
    {
        if (IsBarrierOn || domeBarrier == null)
            return;

        domeBarrier.SetActive(true);
        domeBarrier.gameObject.layer = gameObject.layer;
    }

    public void OffBarrier()
    {
        if (!IsBarrierOn || domeBarrier == null)
            return;

        domeBarrier.SetActive(false);
    }

    private void GenerateNewBarrier()
    {
        domeBarrier = Instantiate(domeBarrierPrefab, transform.position, Quaternion.identity, transform);
        domeBarrier.layer = gameObject.layer;
        domeBarrier.GetComponent<BuildingEntity>().InitStats();
    }

    private IEnumerator ListenBarrierDestroyed()
    {
        while (true)
        {
            yield return barrierDestroyListner;
            yield return WaitTime.GetWaitForSecondOf(barrierRegenCoolTime);
            GenerateNewBarrier();
        }
    }

    private bool IsBarrierDestroyed()
    {
        return domeBarrier == null;
    }

    private void Awake()
    {
        GenerateNewBarrier();
        barrierDestroyListner = new WaitUntil(IsBarrierDestroyed);
        StartCoroutine(ListenBarrierDestroyed());
    }

    public void SetCollider()
    {
        if (domeBarrier.GetComponent<Collider>().enabled)
        {
            domeBarrier.GetComponent<Collider>().enabled = false;
        }
        else
        {
            domeBarrier.GetComponent<Collider>().enabled = true;
        }
    }
}