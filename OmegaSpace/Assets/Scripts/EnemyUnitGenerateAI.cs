using System.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using System.Linq;


[Serializable]
public class EnemyUnitGenerateAI
{
    public ReadOnlyArray<UnitData> GeneratablUnits
    {
        get;
        set;
    }
    private Dictionary<UnitData, float> unitRatioByUnitType = new Dictionary<UnitData, float>();

    [SerializeField]
    public List<UnitGeneratorBarrack> unitGeneratorsBarrack = new List<UnitGeneratorBarrack>(50);
    [SerializeField]
    public List<UnitGeneratorAirDrop> unitGeneratorsAirDrop = new List<UnitGeneratorAirDrop>(50);
    private int availableUnitGeneratorAirDropIndex = 0;
    Dictionary<UnitData, int> airDropUnitCntByType = new Dictionary<UnitData, int>(10);

    private PlayEvaluationSystemUnitPart playerUnitInfo;
    private PlayEvaluationSystemBuildPart playerBuildInfo;

    [SerializeField]
    public ResourceAmount CurrentIron = new ResourceAmount(EResource.RES_IRON, 0);
    public ResourceAmount CurrentGold = new ResourceAmount(EResource.RES_GOLD, 0);

    private Dictionary<UnitData, List<GameObject>> generatedUnitListByType = new Dictionary<UnitData, List<GameObject>>();
    private Dictionary<UnitData, List<GameObject>> GeneratedUnitListByType 
    {
        get
        {
            CollectGeneratedunits();
            return generatedUnitListByType;
        }
    }

    public ReadOnlyDictionary<UnitData, List<GameObject>> ReadOnlyGeneratedUnitListByType
    {
        get => new ReadOnlyDictionary<UnitData, List<GameObject>>(generatedUnitListByType);
    }

    public float AirDropUnitRatio
    {
        set;
        get;
    }


    public void SetObjectMembersInstance()
    {
        GeneratablUnits = TechManager.Instance.UnitDatas;
        playerUnitInfo = PlayEvaluationSystemUnitPart.Instance;
        playerBuildInfo = PlayEvaluationSystemBuildPart.Instance;
    }

    private void CollectGeneratedunits()
    {
        foreach (var a in unitGeneratorsBarrack)
            for (int i = 0; i < GeneratablUnits.Count && GeneratablUnits[i]; i++) 
            {
                var list = a.GetGeneratedUnitListOf(GeneratablUnits[i]);
                if (list != null)
                {
                    if (!generatedUnitListByType.ContainsKey(GeneratablUnits[i]))
                        generatedUnitListByType.Add(GeneratablUnits[i], new List<GameObject>(150));

                    generatedUnitListByType[GeneratablUnits[i]].Clear();
                    foreach (var b in list)
                        generatedUnitListByType[GeneratablUnits[i]].Add(b);
                }
            }
    }

    public void GenerateUnits(float unitCntCoefficient = 1)
    {
        int idleUnitCnt;
        int barrackUnitcnt;
        int idleUnitCntByType;

        idleUnitCnt = GetIdleUnitCnt();
        barrackUnitcnt = (int)(idleUnitCnt * (1 - AirDropUnitRatio) * unitCntCoefficient);
        SetUnitRatioByType();

        Dictionary<UnitData, int> idleUnitCntByUnitType = new Dictionary<UnitData, int>(3);
        for(int i=0;i < GeneratablUnits.Count && GeneratablUnits[i];i++)
        {
            UnitData unitType = GeneratablUnits[i];
            idleUnitCntByType =  (int)(unitRatioByUnitType[unitType] * barrackUnitcnt) - GetGeneratedBarrackUnitCntOf(unitType);
            idleUnitCntByUnitType.Add(unitType, idleUnitCntByType);
        }
        GenerateUnitsAtBarrack(GetMixedOrderUnitDatas(idleUnitCntByUnitType));
    }

    private int GetIdleUnitCnt()
    {
        float baseCnt = playerBuildInfo.TotalBuildingCnt;
        baseCnt = baseCnt * GetUnitCntCoefficient();
        if (baseCnt <= 9)
            baseCnt = 10;
        return (int)baseCnt;
    }

    private float GetUnitCntCoefficient()
    {
        float baseCoefficient = 1f;
        baseCoefficient += (playerUnitInfo.TotalUnitCnt / 10);
        baseCoefficient += (playerBuildInfo.TotalCommanCenterCnt / 5);
        return baseCoefficient;
    }

    private void SetUnitRatioByType()
    {
        for (int i = 0; i < GeneratablUnits.Count && GeneratablUnits[i]; i++)
            if (!unitRatioByUnitType.ContainsKey(GeneratablUnits[i]))
                unitRatioByUnitType.Add(GeneratablUnits[i], 0.3f);
    }

    private int GetGeneratedBarrackUnitCntOf(UnitData type)
    {
        int sum = 0;
        foreach (var a in unitGeneratorsBarrack)
        {
            var list =  a.GetGeneratedUnitListOf(type);
            if (list != null)
                sum += list.Count;
        }
        return sum;
    }

    private int GetGeneratedAirDropUnitCntOf(UnitData type)
    {
        int sum = 0;
        foreach (var a in unitGeneratorsAirDrop)
        {
            var list = a.GetGeneratedUnitListOf(type);
            if (list != null)
                sum += list.Count;
        }
        return sum;
    } 

    private void GenerateUnitsAtBarrack(Queue<UnitData> generateQueue)
    {
        int queueSize = generateQueue.Count;
        int generateCnt = 0;
        int busyBarrackCnt = 0;
        while (generateCnt < queueSize && busyBarrackCnt < unitGeneratorsBarrack.Count)
            foreach (var a in unitGeneratorsBarrack)
            {
                if (a.CanOrderGenerating)
                {
                    a.GenerateUnit(generateQueue.Dequeue(), CurrentIron, CurrentGold);
                    generateCnt++;
                }
                else
                    busyBarrackCnt++;
            }
    }

    private Queue<UnitData> GetMixedOrderUnitDatas(Dictionary<UnitData, int> unitCntByType)
    {
        int sum = 0;
        foreach (var unit in unitCntByType)
            sum += unit.Value;

        Queue<UnitData> generateQueue = new Queue<UnitData>(300);
        bool hasNoUnitData = false;
        int i = 0;
        while(i<sum)
        {
            foreach(var unitData in unitCntByType.Keys.ToList())
            {
                if (!unitCntByType.ContainsKey(unitData))
                {
                    unitCntByType.Remove(unitData);
                    continue;
                }

                if (unitCntByType.Count <= 0)
                {
                    hasNoUnitData = true;
                    break;
                }
              
                generateQueue.Enqueue(unitData);
                i++;
            }

            if (hasNoUnitData)
                break;
        }

        return generateQueue;
    }

    public void OrderAirDropUnits(Dictionary<Vector3, float> attackPosWithAttackSize)
    {
        int unitCnt = (int)(GetIdleUnitCnt() * AirDropUnitRatio);
        foreach (var a in attackPosWithAttackSize)
        {
            int idleUnitCntPerLocation = (int)(unitCnt * a.Value);
            GenerateAirDropUnit(a.Key, a.Value);
        }
    }

    private void GenerateAirDropUnit(Vector3 pos, float ratio)
    {
        int totalAirDropUnitCnt = (int)(GetIdleUnitCnt() * AirDropUnitRatio * ratio);
        var barrack = GetGeneratorAirDroptAt(pos);

        if (barrack)
        {
            foreach (var a in unitRatioByUnitType)
                if (airDropUnitCntByType.ContainsKey(a.Key))
                {
                    int presentGeneratedUnitCnt = barrack.GetGeneratedUnitListOf(a.Key).Count;
                    airDropUnitCntByType[a.Key] = (int)(a.Value * totalAirDropUnitCnt) - presentGeneratedUnitCnt;
                    if (airDropUnitCntByType[a.Key] < 0)
                        airDropUnitCntByType[a.Key] = 0;
                }
                else
                    airDropUnitCntByType.Add(a.Key, (int)(a.Value * totalAirDropUnitCnt));

            Queue<UnitData> unitQueues = GetMixedOrderUnitQueue();
            while (unitQueues.Count > 0)
                barrack.GenerateUnit(unitQueues.Dequeue(), CurrentIron, CurrentGold);
        }
        else 
        {
            int availableUnitGeneratorAirDropIndex = 0;
            while (availableUnitGeneratorAirDropIndex < unitGeneratorsAirDrop.Count)
                if (unitGeneratorsAirDrop[availableUnitGeneratorAirDropIndex].IsOnGenerating == false)
                {
                    foreach (var a in unitRatioByUnitType)
                        if (airDropUnitCntByType.ContainsKey(a.Key)) { 
                            airDropUnitCntByType[a.Key] = (int)(a.Value * totalAirDropUnitCnt);

                            if (airDropUnitCntByType[a.Key] < 0)
                                airDropUnitCntByType[a.Key] = 0;
                        }
                        else
                            airDropUnitCntByType.Add(a.Key, (int)(a.Value * totalAirDropUnitCnt));

                    Queue<UnitData> unitQueues = GetMixedOrderUnitQueue();
                    barrack = unitGeneratorsAirDrop[availableUnitGeneratorAirDropIndex];

                    while (unitQueues.Count > 0)
                    {
                        if (unitQueues.Count == 0)
                            return;
                        barrack.GenerateUnit(unitQueues.Dequeue(), CurrentIron, CurrentGold);
                    }
                    barrack.emergePos = pos;
                    break;
                }
                else
                    availableUnitGeneratorAirDropIndex++;
        }
    }

    private UnitGeneratorAirDrop GetGeneratorAirDroptAt(Vector3 pos)
    {
        foreach (var barrack in unitGeneratorsAirDrop)
            if (barrack.emergePos == pos)
                return barrack;
        return null;
    }

    private Queue<UnitData> GetMixedOrderUnitQueue()
    {
        int totalCnt = 0;
        foreach (var a in airDropUnitCntByType)
        {
            if (a.Value < 0)
                continue;
            totalCnt += a.Value;
        }

        Queue<UnitData> unitQueues = new Queue<UnitData>(totalCnt);

        for (int i = 0; i < totalCnt;)
            foreach(var a in airDropUnitCntByType.Keys.ToList())
                if (airDropUnitCntByType[a] > 0)
                {
                    unitQueues.Enqueue(a);
                    airDropUnitCntByType[a]--;
                    i++;
                }
        return unitQueues;
    }
}
