using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laboratory : BuildingEntity
{
    private int healthMax = 100;
    private int armorMax = 0;

    private void Start()
    {
        stats = new UnitStats(healthMax, armorMax);
        level.Value = 1;
    }

}