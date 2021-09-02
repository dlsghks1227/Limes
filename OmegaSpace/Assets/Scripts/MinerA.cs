using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerA : Miner
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<GoldOre>() != null)
        {
            resourceObj = col.gameObject;
            InitMiner(resourceObj);
        }
    }
}