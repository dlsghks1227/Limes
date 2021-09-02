using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Ore : GridEntity
{
    [SerializeField]
    private int amount;

    private bool isMine;

    public int Amount { get => amount; set => amount = value; }
    public bool IsMine { get => isMine; set => isMine = value; }

    public int Mine(int mineAmount)
    {
        if (amount < mineAmount)
        {
            mineAmount = amount;
            amount = 0;
        }
        else
        {
            amount -= mineAmount;
        }
        return mineAmount;
    }

    public virtual EResource GetResourceType() { return EResource.RES_NONE; }

}