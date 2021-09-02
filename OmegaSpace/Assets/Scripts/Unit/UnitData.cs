using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data", menuName = "Scriptable Object/UnitData", order = int.MaxValue)]
public class UnitData : ScriptableObject
{
    [SerializeField]
    private bool isGeneratable = false;
    public bool IsGeneratable
    {
        set => isGeneratable = value;
        get => isGeneratable;
    }
    [SerializeField]
    private GameObject prefab;
    public GameObject Prefab
    {
        get => prefab;
    }

    [SerializeField]
    private ResourceAmount ironCost = new ResourceAmount(EResource.RES_IRON, 0);
    public ResourceAmount IronCost
    {
        get => ironCost;
    }
    [SerializeField]
    private ResourceAmount goldCost = new ResourceAmount(EResource.RES_GOLD, 0);
    public ResourceAmount GoldCost
    {
        get => goldCost;
    }

    public float generateTimes;
    public bool IsEqualUnitID(EUnitID unitID)
    {
        if (unitID.Equals(prefab.GetComponent<UnitEntity>().UnitID))
            return true;
        else
            return false;
    }

    public void AdjustIronCost(float ratio)
    {
        ironCost.resAmount = (int)(ironCost.resAmount * ratio);
    }

    public void AdjustIronCost(int value)
    {
        ironCost.resAmount += value;
    }

    public void AdjustGoldCost(float ratio)
    {
        goldCost.resAmount = (int)(goldCost.resAmount * ratio);
    }

    public void AdjustGoldCost(int value)
    {
        goldCost.resAmount += value;
    }

    public void AdjustGenerateTime(float ratio)
    {
        generateTimes *= ratio;
    }

    public void AdjustGenerateTime(int value)
    {
        generateTimes += value;
    }
}
