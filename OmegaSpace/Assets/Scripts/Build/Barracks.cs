using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : BuildingEntity, ILevelInterface
{
    int healthMax = 200;
    int armorMax = 0;

    [SerializeField]
    private GameObject unitPrefab;

    private void Start()
    {
        level.Value = 1;
        stats = new UnitStats(healthMax, armorMax);
    }

    public void MakeUnit()
    {
    }
    
    public void LevelUp(int amount)
    {
        level.Value += amount;
        // 체력 증가
    }
}