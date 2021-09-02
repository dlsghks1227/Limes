using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Miner : BuildingEntity, ILevelInterface
{
    protected GameObject resourceObj;
    protected Ore resource;

    protected int healthMax = 100;
    protected int armorMax = 0;

    protected ResourceStorage storage;

    [SerializeField]
    protected int miningAmount = 10;
    [SerializeField]
    protected float miningSpeed = 2.0f;

    [SerializeField]
    private GameObject textPrefab =null;

    private GameObject miningText = null;
    private MiningText mining = null;

    private void Start()
    {
        level = new Level(1);
        stats = new UnitStats(healthMax, armorMax);
        storage = GameObject.FindObjectOfType<ResourceStorage>();
    }

    public void InitMiner(GameObject resObject)
    {
        resourceObj = resObject;
        if (resourceObj == null) 
        {
            Debug.Log("resourceObj = null");
            return; 
        }

        resource = resourceObj.GetComponent<Ore>();
        resource.IsMine = true;
        if (resource == null)
        {
            Debug.Log("resource = null");
            return;
        }

        mining = Instantiate(textPrefab).GetComponent<MiningText>();
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        while (resource.Amount > 0)
        {
            if (storage != null && resource.Amount >= miningAmount)
            {
                int minedAmount = resource.Mine(miningAmount);
                ResourceAmount resAmount = new ResourceAmount(resource.GetResourceType(), minedAmount);
                mining.Activate();
                mining.SetPosition(resource.transform.position);
                mining.SetText("+ " + miningAmount.ToString());
                storage.Store(resAmount);
            }
            else if (storage == null)
            {
                Debug.Log("No storage");
                yield return null;
            }
            else if (resource.Amount < miningAmount && resource.Amount > 0)
            {
                int minedAmount = resource.Mine(resource.Amount);
                ResourceAmount resAmount = new ResourceAmount(resource.GetResourceType(), minedAmount);
                storage.Store(resAmount);
            }
            yield return new WaitForSeconds(miningSpeed);
        }
        if (resource.Amount <= 0) { resource.IsMine = false; Destroy(resourceObj); mining.Deactivate(); }
    }

    public void LevelUp(int amount)
    {
        level.Value += amount;
        miningAmount += amount * 5;
    }

    public override void Destroyed()
    {
        ResourceAmount res = new ResourceAmount(EResource.RES_IRON, 100);
        storage.Store(res);
    }

    public int MiningAmount { get => miningAmount; }
}