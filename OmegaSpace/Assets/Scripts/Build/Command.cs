using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : BuildingEntity
{
    private ResourceStorage storage;

    private int healthMax = 100;
    private int armorMax = 0;

    private void OnEnable()
    {
        stats = new UnitStats(healthMax, armorMax);
        level.Value = 1;
        storage = GameObject.FindObjectOfType<ResourceStorage>();
        territoryType = ETerritory.ENEMY;
    }

    public override void Destroyed()
    {
        ResourceAmount res = new ResourceAmount(EResource.RES_IRON, 100);
        storage.Store(res);
    }
}