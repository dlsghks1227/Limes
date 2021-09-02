using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : BuildingEntity, ILevelInterface
{
    int healthMax = 100;
    int armorMax = 0;

    private void Start()
    {
        level.Value = 1;
        stats = new UnitStats(healthMax, armorMax);
    }

    public void LevelUp(int amount)
    {
        level.Value += amount;
        // 체력 증가
        // 공격력 증가
    }
}