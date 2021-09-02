using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : BuildingEntity
{
    private static List<ResourceAmount> stores;

    private int healthMax = 100;
    private int armorMax = 0;

    private void Start()
    {
        level = new Level(1);
        stats = new UnitStats(healthMax, armorMax);
        stores = new List<ResourceAmount>();
        foreach (EResource res in Enum.GetValues(typeof(EResource)))
        {
            ResourceAmount elem = new ResourceAmount(res, 0);
            stores.Add(elem);
        }
    }

    public void Store(ResourceAmount res)
    {
        for (int i = 0; i < stores.Count; i++)
        {
            ResourceAmount elem = stores[i];
            if (elem.resType == res.resType)
            {
                if (res.resAmount < 0 && elem.resAmount < Math.Abs(res.resAmount))
                {
                    return;
                }
                elem.resAmount += res.resAmount;
                PlayerDataManager.Instance.AddPlayerResource(res);
                stores[i] = elem;
            }
        }
    }

    public int GetAmount(EResource res)
    {
        int amount = 0;
        for (int i = 0; i < stores.Count; i++)
        {
            ResourceAmount elem = stores[i];
            if (elem.resType == res)
            {
                amount = elem.resAmount;
            }
        }
        return amount;
    }

    public override void Destroyed()
    {
        for (int i = 0; i < stores.Count; i++)
        {
            int amount = (int)(stores[i].resAmount * 0.9);
        }
    }
}