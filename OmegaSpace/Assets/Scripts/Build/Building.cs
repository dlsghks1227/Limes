using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Building : BuildingEntity
{
    private int healthMax = 100;
    private int armorMax = 0;

    private void Start()
    {
        stats = new UnitStats(healthMax, armorMax);
        territoryType = ETerritory.ENEMY;
    }

    private void Update()
    {
        
    }

}