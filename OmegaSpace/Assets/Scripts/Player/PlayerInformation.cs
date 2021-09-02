using System;  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformation
{
    [System.Serializable]
    public class PlayerComposition
    {
        public int maxValue;        // 최대 값
        public int currentValue;    // 현재 값

        public PlayerComposition(int max, int current)
        {
            if (current > max)
            {
                maxValue = max;
                currentValue = max;
            }
            else if (current < 0)
            {
                maxValue = max;
                currentValue = 0;
            }
            else
            {
                maxValue = max;
                currentValue = current;
            }
        }

        public PlayerComposition(int max)
        {
            maxValue = max;
            currentValue = max;
        }

        public float Normalize()
        {
            return (float)currentValue / (float)maxValue;
        }
    }

    #region 1. 플레이어 구성 요소
    [Header("Player")]
    public PlayerComposition hp;                // 체력
    public PlayerComposition food;              // 식량
    public PlayerComposition population;        // 인구
    #endregion

    [SerializeField]
    private ResourceAmount iron;
    public ResourceAmount Iron
    {
        get => iron;
    }
    [SerializeField]
    private ResourceAmount gold;
    public ResourceAmount Gold
    {
        get => gold;
    }

    public void Initialize()
    {
        hp              = new PlayerComposition(100);
        food            = new PlayerComposition(100);
        population      = new PlayerComposition(100);

        iron            = new ResourceAmount(EResource.RES_IRON, 0);
        gold            = new ResourceAmount(EResource.RES_GOLD, 0);
    }

    public void AddResource(ResourceAmount res)
    {
        if (res.resType.Equals(EResource.RES_IRON))
            iron.resAmount += res.resAmount;
        if (res.resType.Equals(EResource.RES_IRON))
            gold.resAmount += res.resAmount;
    }

    public void CostResource(ResourceAmount res)
    {
        if (IsAffordable(res))
        {
            if(res.resType.Equals(EResource.RES_IRON))
                iron.SubstractAmount(res);
            else if (res.resType.Equals(EResource.RES_GOLD))
                gold.SubstractAmount(res);
        }
    }

    private bool IsAffordable(ResourceAmount res)
    {
        if (res.resType.Equals(EResource.RES_IRON))
            return iron.resAmount >= res.resAmount;
        else if(res.resType.Equals(EResource.RES_GOLD))
            return gold.resAmount >= res.resAmount;
        return false;
    }
}