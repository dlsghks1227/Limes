using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerB : Miner
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<IronOre>() != null)
        {
            resourceObj = col.gameObject;
            InitMiner(resourceObj);
        }
    }
}