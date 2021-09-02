using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Combine : BuildingEntity
{
    private ResourceStorage storage;

    private int healthMax = 100;
    private int armorMax = 0;

    private void Start()
    {
        level = new Level(1);
        stats = new UnitStats(healthMax, armorMax);
        storage = GameObject.FindObjectOfType<ResourceStorage>();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.name == "Combine")
                {
                    int combineAmount = 10;
                    EResource matAType = EResource.RES_IRON;
                    EResource matBType = EResource.RES_GOLD;

                    ResourceAmount materialA = new ResourceAmount(matAType, -combineAmount);
                    ResourceAmount materialB = new ResourceAmount(matBType, -combineAmount);
                    CombineRes(materialA, materialB);
                }
            }
        }
    }

    public void CombineRes(ResourceAmount materialA, ResourceAmount materialB)
    {
        Debug.Log("Combine");
        if (storage.GetAmount(materialA.resType) >= Math.Abs(materialA.resAmount) && storage.GetAmount(materialB.resType) >= Math.Abs(materialB.resAmount))
        {
            storage.Store(materialA);
            storage.Store(materialB);
            ResourceAmount resource = new ResourceAmount(EResource.RES_IRON, -materialA.resAmount);
            storage.Store(resource);
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