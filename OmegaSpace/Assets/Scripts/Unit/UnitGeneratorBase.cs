using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class UnitGeneratorBase : MonoBehaviour
{
    #region variables
    protected UnitData currentGeneratingUnit;

    [SerializeField]
    protected int generatingQueueSize = 10;
    protected ReadOnlyArray<UnitData> generatableUnits;
    protected Queue<UnitData> generatingQueue;
    public bool CanOrderGenerating
    {
        get
        {
            if (generatingQueue == null)
                InitGeneratingQueueSize();
            return (generatingQueue.Count < generatingQueueSize);
        }
    }

    public Vector3 emergePos;
    protected Vector3 emergePosCollideCheckSize = new Vector3(1.5f, 0.1f, 1.5f);
    protected Vector3 emergePosBoxCastVec = Vector3.one.normalized;
    public bool IsOnGenerating
    {
        protected set;
        get;
    }

    protected Dictionary<UnitData, List<GameObject>> generatedUnits = new Dictionary<UnitData, List<GameObject>>();
    #endregion

    #region user functions

    public ReadOnlyCollection<GameObject> GetGeneratedUnitListOf(UnitData unitType)
    {
        RemoveDeadUnitsFromList(unitType);
        if (generatedUnits.ContainsKey(unitType))
            return generatedUnits[unitType].AsReadOnly();
        return null;
    }

    private void RemoveDeadUnitsFromList(UnitData unitType)
    {
        if (generatedUnits.ContainsKey(unitType))
        {
            var list = generatedUnits[unitType];
            if (list != null)
            {
                List<GameObject> deleteList = new List<GameObject>(50);
                foreach (var a in list)
                    if (a == null)
                        deleteList.Add(a);

                foreach (var a in deleteList)
                    list.Remove(a);
            }
        }
    }

    protected void AddUnitsToGeneratedUnits(GameObject unit)
    {
        if (!generatedUnits.ContainsKey(currentGeneratingUnit))
            generatedUnits.Add(currentGeneratingUnit, new List<GameObject>(100));
        generatedUnits[currentGeneratingUnit].Add(unit);
    }

    protected void InitGeneratingQueueSize()
    {
        generatingQueue = new Queue<UnitData>(generatingQueueSize);
    }

    protected void GetUnitDatas()
    {
        generatableUnits = TechManager.Instance.UnitDatas;
    }
    public virtual void GenerateUnit(int idx, ResourceAmount iron, ResourceAmount gold)
    {
        if (!CanOrderGenerating && idx >= generatableUnits.Count)
            return;

        currentGeneratingUnit = generatableUnits[idx];
        if (currentGeneratingUnit.IsGeneratable && IsAffordableToCost(iron, gold))
        {
            generatingQueue.Enqueue(currentGeneratingUnit);
            if (!IsOnGenerating)
                StartCoroutine(DeployUnit());
        }
    }

    public virtual void GenerateUnit(UnitData unit, ResourceAmount iron, ResourceAmount gold)
    {
        if (!CanOrderGenerating && unit.IsGeneratable)
            return;

        currentGeneratingUnit = unit;
        if (IsAffordableToCost(iron, gold))
        {
            generatingQueue.Enqueue(currentGeneratingUnit);
            if (!IsOnGenerating)
                StartCoroutine(DeployUnit());
        }
    }

    protected bool IsAffordableToCost(ResourceAmount iron, ResourceAmount gold)
    {
        bool isEnoughIron = iron >= currentGeneratingUnit.IronCost;
        bool isEnoughGold = gold >= currentGeneratingUnit.GoldCost;
        return isEnoughIron && isEnoughGold;
    }

    protected void PayUnitCost(ref ResourceAmount iron, ref ResourceAmount gold)
    {
        iron.SubstractAmount(currentGeneratingUnit.IronCost);
        gold.SubstractAmount(currentGeneratingUnit.GoldCost);
    }

    protected virtual IEnumerator DeployUnit() { yield return null; }
    #endregion
}