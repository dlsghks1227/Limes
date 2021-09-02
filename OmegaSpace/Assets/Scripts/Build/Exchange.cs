using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Exchange : BuildingEntity
{
    private int healthMax = 100;
    private int armorMax = 0;

    private ResourceStorage storage;

    private void Start()
    {
        level = new Level(1);
        stats = new UnitStats(healthMax, armorMax);
        storage = GameObject.FindObjectOfType<ResourceStorage>();
    }

    private void Update()
    {
        
    }

    public void ExchangeRes(ResourceAmount material, ResourceAmount outcome)
    {
        Debug.Log("Exchange");
        if (storage.GetAmount(material.resType) >= Math.Abs(material.resAmount))
        {
            storage.Store(material);
            storage.Store(outcome);
        }
        else
        {
            Debug.Log("Not Enough Resource!");
            return;
        }
    }

    public override void Destroyed()
    {
        ResourceAmount res = new ResourceAmount(EResource.RES_IRON, 100);
        storage.Store(res);
    }
}