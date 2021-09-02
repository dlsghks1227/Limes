using System;
using System.Collections;
using System.Collections.Generic;

// 자원 이름 변경
// 1. 잼스톤
// 2. 암흑물질
// 3. 공모전 알아서
public enum EResource
{
    RES_NONE=0,
    RES_IRON,
    RES_GOLD,
    RES_ETHER
}


[Serializable]
public struct ResourceAmount
{
    public EResource resType;
    public int resAmount;

    public ResourceAmount(EResource type, int amount)
    {
        resType = type;
        resAmount = amount;
    }

    public void AddAmount(ResourceAmount operand)
    {
        if (resType.Equals(operand.resType))
            resAmount += operand.resAmount;
    }

    public void SubstractAmount(ResourceAmount operand)
    {
        if(resType.Equals(operand.resType))
            resAmount -= operand.resAmount;
    }

    public static bool operator <=(ResourceAmount standard, ResourceAmount comparer)
    {
        bool isSameType = standard.resType.Equals(comparer.resType);
        if (isSameType && (standard.resAmount <= comparer.resAmount))
            return true;
        else
            return false;
    }

    public static bool operator >=(ResourceAmount standard, ResourceAmount comparer)
    {
        bool isSameType = standard.resType.Equals(comparer.resType);
        if (isSameType && (standard.resAmount >= comparer.resAmount))
            return true;
        else
            return false;
    }
}

public class Resource
{
    protected EResource resType;
    protected bool isSpecialResource;

    public EResource ResType { get => resType; }
    public bool IsSpecialRes { get => isSpecialResource; }
}

public class Iron : Resource
{
    public Iron()
    {
        resType = EResource.RES_IRON;
        isSpecialResource = false;
    }
}

public class Gold : Resource
{
    public Gold()
    {
        resType = EResource.RES_GOLD;
        isSpecialResource = false;
    }
}

public class Ether : Resource
{
    public Ether()
    {
        resType = EResource.RES_ETHER;
        isSpecialResource = false;
    }
}