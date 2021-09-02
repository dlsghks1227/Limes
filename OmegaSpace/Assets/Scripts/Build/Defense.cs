using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 투사체 통과 X
public class Defense : BuildingEntity, ILevelInterface
{
    int healthMax = 50;
    int armorMax = 50;

    private ResourceStorage storage;

    private void Start()
    {
        storage = GameObject.FindObjectOfType<ResourceStorage>();
        level.Value = 1;
        stats = new UnitStats(healthMax, armorMax);
    }

    public void LevelUp(int amount)
    {
        level.Value += amount;
    }

    public override void Destroyed()
    {
        ResourceAmount res = new ResourceAmount(EResource.RES_IRON, 100);
        storage.Store(res);
    }
}