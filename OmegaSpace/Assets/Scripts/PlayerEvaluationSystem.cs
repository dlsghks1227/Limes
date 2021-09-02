using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Rendering;


public struct PointWithChangeRate
{
    public float presentPoint;
    public float pointChangeRateFromPrevious;

    public PointWithChangeRate(float point, float rate)
    {
        presentPoint = point;
        pointChangeRateFromPrevious = rate;
    }
}


public interface IPlayEvaluationStatistic
{
    ReadOnlyCollection<PointWithChangeRate> TotalPointOverall
    {
        get;
    }

    float GetTotalPoint();
}


public class PlayEvaluationSystemUnitPart : Singleton<PlayEvaluationSystemUnitPart> , IPlayEvaluationStatistic
{
    private List<PointWithChangeRate> totalPointOverall = new List<PointWithChangeRate>(10000);
    public ReadOnlyCollection<PointWithChangeRate> TotalPointOverall
    {
        get => totalPointOverall.AsReadOnly();
    }
    private TechManager techManager;
    public int TotalUnitCnt
    {
        get
        {
            int sum = 0;
            for (int i = 0; i < techManager.UnitDatas.Count; i++)
            {
                var list = techManager.GetUnitListOf(techManager.UnitDatas[i]);
                if (list != null)
                    sum += list.Count;
            }
            peakTotalUnitCnt = sum > peakTotalUnitCnt ? sum : peakTotalUnitCnt;
            return sum;
        }
    }
    private int peakTotalUnitCnt=0;
    public int PeakTotalUnitCnt
    {
        get => peakTotalUnitCnt;
    }
    public int TotalAttackDamage
    {
        get
        {
            int sum = 0;
            for (int i = 0; i < techManager.UnitDatas.Count; i++)
            {
                var list = techManager.GetAttackCompoListOf(techManager.UnitDatas[i]);
                if (list != null)
                    foreach(var a in list)
                    sum += a.ModifiedAttackDamage;
            }
            peakAttackDamage = peakAttackDamage < sum ? sum : peakAttackDamage;
            return sum;
        }
    }
    public int AverageAttackDamage
    {
        get => (TotalAttackDamage / TotalUnitCnt);
    }
    private int peakAttackDamage = 0;
    public int PeakAttackDamage
    {
        get => peakAttackDamage;
    }
    private float unitOffensiveChangeRate;
    public float TotalUnitMobility
    {
        get
        {
            float sum = 0;
            for (int i = 0; i < techManager.UnitDatas.Count; i++)
            {
                var list = techManager.GetMovementCompoListOf(techManager.UnitDatas[i]);
                if (list != null)
                    foreach (var a in list)
                        sum += a.ModifiedSpeed;
            }
            peakUnitMobility = peakUnitMobility < sum ? sum : peakUnitMobility;
            return sum;
        }
    }
    public float AverageUnitMobility
    {
        get => (TotalUnitMobility / TotalUnitCnt);
    }
    private float peakUnitMobility = 0;
    public float PeakUnitMobility
    {
        get => peakUnitMobility;
    }
    public int TotalUnitHealthPoint
    {
        get
        {
            int sum = 0;
            for (int i = 0; i < techManager.UnitDatas.Count; i++)
            {
                var list = techManager.GetUnitEnityListOf(techManager.UnitDatas[i]);
                if (list != null)
                    foreach (var a in list)
                        sum += a.HealthPoint;
            }
            peakUnitHealthPoint = peakUnitHealthPoint < sum ? sum : peakUnitHealthPoint;
            return sum;
        }
    }
    public int AverageUnitHealthPoint
    {
        get => (TotalUnitHealthPoint / TotalUnitCnt);
    }
    private int peakUnitHealthPoint = 0;
    public int PeakUnitHealthPoint
    {
        get => peakUnitHealthPoint;
    }
    public int TotalUnitArmorPoint
    {
        get
        {
            int sum = 0;
            for (int i = 0; i < techManager.UnitDatas.Count; i++)
            {
                var list = techManager.GetUnitEnityListOf(techManager.UnitDatas[i]);
                if (list != null)
                    foreach (var a in list)
                        sum += a.ArmorPoint;
            }
            peakUnitArmorPoint = peakUnitArmorPoint < sum ? sum : peakUnitArmorPoint;
            return sum;
        }
    }
    public int AverageUnitArmorPoint
    {
        get => (TotalUnitArmorPoint / TotalUnitCnt);
    }
    private int peakUnitArmorPoint = 0;
    public int PeakUnitArmorPoint
    {
        get => peakUnitArmorPoint;
    }
    
    private Dictionary<UnitData, float> unitRatioByUnitType=new Dictionary<UnitData, float>();
    public ReadOnlyDictionary<UnitData, float> UnitRatioByUnitType
    {
        get
        {
            CalculateUnitRatioByUnitType();
            return new ReadOnlyDictionary<UnitData, float>(unitRatioByUnitType);
        }
    }

    #region constructor
    public PlayEvaluationSystemUnitPart()
    {
        techManager = TechManager.Instance;
        for (int i = 0; i < techManager.UnitDatas.Count; i++)
        {
            if (i < techManager.UnitDatas.Count && techManager.UnitDatas[i])
                unitRatioByUnitType.Add(techManager.UnitDatas[i], 0f);
        }
    }
    #endregion
    public float GetTotalPoint()
    {
        float sum = TotalUnitCnt + TotalUnitHealthPoint + TotalUnitArmorPoint + TotalAttackDamage + TotalUnitMobility;
        AddTotalPointToChangeOverall(sum);
        return sum;
    }

    private void AddTotalPointToChangeOverall(float sum)
    {
        PointWithChangeRate pointAndChangeRate = new PointWithChangeRate(sum, 0);
        if (totalPointOverall.Count == 0)
        {
            pointAndChangeRate = new PointWithChangeRate(sum, 0);
            totalPointOverall.Add(pointAndChangeRate);
        }
        else
        {
            pointAndChangeRate = new PointWithChangeRate(sum, 0);
            float changeRate = pointAndChangeRate.presentPoint / totalPointOverall[totalPointOverall.Count - 1].presentPoint;
            pointAndChangeRate.pointChangeRateFromPrevious = changeRate;
            totalPointOverall.Add(pointAndChangeRate);
        }
    }

    private void CalculateUnitRatioByUnitType()
    {
        for (int i = 0; i < techManager.UnitDatas.Count; i++)
        {
            var unitList = techManager.GetUnitListOf(techManager.UnitDatas[i]);

            if (unitList != null)
            {
                unitRatioByUnitType[techManager.UnitDatas[i]] = (float)unitList.Count / TotalUnitCnt;
            }
            else
                unitRatioByUnitType[techManager.UnitDatas[i]] = 0f;
        }
    }
}


public class PlayEvaluationSystemTechPart : Singleton<PlayEvaluationSystemTechPart> , IPlayEvaluationStatistic
{
    private TechTree techTree;
    private Dictionary<ETechTrait, float> researchedRatiosByTechTrait = new Dictionary<ETechTrait, float>();
    public ReadOnlyDictionary<ETechTrait,float> ResearchedRatioByTechTrait
    {
        get
        {
            CaculateResearchedRatioByTraits();
            return new ReadOnlyDictionary<ETechTrait, float>(researchedRatiosByTechTrait);
        }
    }
   
    private List<PointWithChangeRate> researchAcheivementRate = new List<PointWithChangeRate>(100000);
    public ReadOnlyCollection<PointWithChangeRate> TotalPointOverall
    {
        get => researchAcheivementRate.AsReadOnly();
    }
    public int TotalResearchedNodeCnt
    {
        get => techTree.AchievedNodeIndexs.Length;
    }

    #region constructor
    public PlayEvaluationSystemTechPart()
    {
        techTree = TechTree.Instance;
        foreach (ETechTrait a in Enum.GetValues(typeof(ETechTrait)))
            if (!researchedRatiosByTechTrait.ContainsKey(a))
                researchedRatiosByTechTrait.Add(a, 0f);
    }
    #endregion

    #region funcs
    public float GetTotalPoint()
    {
        int buffer = TotalResearchedNodeCnt;
        if (TotalPointOverall.Count == 0)
            researchAcheivementRate.Add(new PointWithChangeRate(buffer, 0f));
        else
        {
            float changeRate = buffer / TotalPointOverall[researchAcheivementRate.Count - 1].presentPoint;
            PointWithChangeRate newPoint = new PointWithChangeRate(buffer, changeRate);
            researchAcheivementRate.Add(newPoint);
        }    
        return buffer;
    }

    private void CaculateResearchedRatioByTraits()
    {
        ETechTrait[] techTraits =(ETechTrait[])Enum.GetValues(typeof(ETechTrait));
        ClearRatioValuesOf(techTraits);
      
        foreach (var a in techTree.AchievedNodeIndexs)
            researchedRatiosByTechTrait[techTree.TechNodeList[a].Trait]++;
        foreach(var a in techTraits)
            researchedRatiosByTechTrait[a] /= TotalResearchedNodeCnt;
    }

    private void ClearRatioValuesOf(ETechTrait[] techTraits)
    {
        foreach (var a in techTraits)
        {
            if (!researchedRatiosByTechTrait.ContainsKey(a))
                researchedRatiosByTechTrait.Add(a, 0f);
            researchedRatiosByTechTrait[a] = 0f;
        }
    }
    #endregion
}


public class PlayEvaluationSystemResourcePart : Singleton<PlayEvaluationSystemResourcePart>, IPlayEvaluationStatistic
{
    private List<PointWithChangeRate> amountOfPurchaseOnUnitOverall = new List<PointWithChangeRate>(100000);
    public ReadOnlyCollection<PointWithChangeRate> AmountOfPurchaseOnUnitOverall
    {
        get => amountOfPurchaseOnUnitOverall.AsReadOnly();
    }

    private List<PointWithChangeRate> amountOfPurchaseOnBuildingOverall = new List<PointWithChangeRate>(100000);
    public ReadOnlyCollection<PointWithChangeRate> AmountOfPurchaseOnBuildingOverall
    {
        get => amountOfPurchaseOnBuildingOverall.AsReadOnly();
    }
    
    private List<PointWithChangeRate> amountOfPurchaseOnTechOverall = new List<PointWithChangeRate>(100000);
    public ReadOnlyCollection<PointWithChangeRate> AmountOfPurchaseOnTechOverall
    {
        get => amountOfPurchaseOnTechOverall.AsReadOnly();
    }

    private List<PointWithChangeRate> totalPointOverall = new List<PointWithChangeRate>(100000);
    public ReadOnlyCollection<PointWithChangeRate> TotalPointOverall
    {
        get
        {
            CaculateTotalPointChangeOverall();
            return totalPointOverall.AsReadOnly();
        }
    }

    public float GetTotalPoint()
    {
        float sum = 0;
        int lastIdxOfTechOverall = amountOfPurchaseOnTechOverall.Count - 1;
        int lastIdxOfUnitOverall = amountOfPurchaseOnUnitOverall.Count - 1;
        int lastIdxOfBuildingOverall = amountOfPurchaseOnBuildingOverall.Count - 1;

        if (lastIdxOfTechOverall > -1)
            sum += amountOfPurchaseOnTechOverall[lastIdxOfTechOverall].presentPoint;
        if (lastIdxOfUnitOverall > -1)
            sum += amountOfPurchaseOnUnitOverall[lastIdxOfUnitOverall].presentPoint;
        if (lastIdxOfBuildingOverall > -1)
            sum += amountOfPurchaseOnBuildingOverall[lastIdxOfBuildingOverall].presentPoint;

        return sum;
    }

    private void CaculateTotalPointChangeOverall()
    {
        float sum = GetTotalPoint();
        if (totalPointOverall.Count == 0)
            totalPointOverall.Add(new PointWithChangeRate(sum, 0f));
        else
        {
            float latestIncreaseValue = GetLatestIncreaseValue(totalPointOverall);
            float changeRate = sum / latestIncreaseValue;
            float accumulatedValue = totalPointOverall[totalPointOverall.Count - 1].presentPoint;
            totalPointOverall.Add(new PointWithChangeRate(sum + accumulatedValue, changeRate));
        }
    }

    public void AddAmountOfPurchaseOnTech(uint value) 
    {
        if (amountOfPurchaseOnTechOverall.Count == 0)
            amountOfPurchaseOnTechOverall.Add(new PointWithChangeRate(value, 0f));
        else
        {
            float latestIncreaseValue = GetLatestIncreaseValue(amountOfPurchaseOnTechOverall);
            float changeRate = value / latestIncreaseValue;
            float accumulatedValue = amountOfPurchaseOnTechOverall[amountOfPurchaseOnTechOverall.Count - 1].presentPoint;
            amountOfPurchaseOnTechOverall.Add(new PointWithChangeRate(value + accumulatedValue, changeRate));
        }
    }

    public void AddAmountOfPurchaseOnUnit(uint value)
    {
        if (amountOfPurchaseOnUnitOverall.Count == 0)
            amountOfPurchaseOnUnitOverall.Add(new PointWithChangeRate(value, 0f));
        else
        {
            float latestIncreaseValue = GetLatestIncreaseValue(amountOfPurchaseOnUnitOverall);
            float changeRate = value / latestIncreaseValue;
            float accumulatedValue = amountOfPurchaseOnUnitOverall[amountOfPurchaseOnUnitOverall.Count - 1].presentPoint;
            amountOfPurchaseOnUnitOverall.Add(new PointWithChangeRate(value + accumulatedValue, changeRate));
        }
    }

    public void AddAmountOfPurchaseOnBuilding(uint value)
    {
        if (amountOfPurchaseOnBuildingOverall.Count == 0)
            amountOfPurchaseOnBuildingOverall.Add(new PointWithChangeRate(value, 0f));
        else
        {
            float latestIncreaseValue = GetLatestIncreaseValue(amountOfPurchaseOnBuildingOverall);
            float changeRate = value / latestIncreaseValue;
            float accumulatedValue = amountOfPurchaseOnBuildingOverall[amountOfPurchaseOnBuildingOverall.Count - 1].presentPoint;
            amountOfPurchaseOnBuildingOverall.Add(new PointWithChangeRate(value + accumulatedValue, changeRate));
        }
    }

    private float GetLatestIncreaseValue(List<PointWithChangeRate> list)
    {
        if (list.Count == 1)
            return list[0].presentPoint;
        else
        {
            float latestIncreaseValue = list[list.Count - 1].presentPoint - list[list.Count - 2].presentPoint;
            return latestIncreaseValue;
        }
    }
}


public class PlayEvaluationSystemBuildPart : Singleton<PlayEvaluationSystemBuildPart> , IPlayEvaluationStatistic
{
    private TechManager techManager;
    public float tuerrtToBarricadeRatio
    {
        get => GetTurretRatioToBarricade();
    }
    public int TotalBuildingCnt
    {
        get => GetTotalBuildingCnt();
    }
    public int TotalDefenseBuildingCnt
    {
        get => GetTotalDefenseBuildingCnt();
    }
    public int TotalBuildingHealthPoint
    {
        get => GetTotalBuildingHealthPoint();
    }
    public int TotalBuildingArmorPoint
    {
        get => GetTotalBuildingArmorPoint();
    }
    public int TotalCommanCenterCnt
    {
        get
        {
            var commandCenterOrigin = techManager.GetBuildingDataFrom("Command Center");
            if(commandCenterOrigin)
               return techManager.GetPresentBuildingListOf(commandCenterOrigin).Count;
            return 1;
        }
    }
    public int TotalBuildingDamge
    {
        get => GetTotalBuildingAttackDamage();
    }
    private List<PointWithChangeRate> totalPointOverall = new List<PointWithChangeRate>(10000);
    public ReadOnlyCollection<PointWithChangeRate> TotalPointOverall
    {
        get => totalPointOverall.AsReadOnly();
    }

    #region constructor
    public PlayEvaluationSystemBuildPart()
    {
        techManager = TechManager.Instance;
    }
    #endregion

    public float GetTotalPoint()
    {
        int sum = TotalBuildingCnt + TotalBuildingDamge + TotalBuildingArmorPoint + TotalBuildingHealthPoint;
        if (totalPointOverall.Count == 0)
            totalPointOverall.Add(new PointWithChangeRate(sum, 0f));
        else
        {
            float changeRate = sum / totalPointOverall[totalPointOverall.Count - 1].presentPoint;
            PointWithChangeRate newPoint = new PointWithChangeRate(sum, changeRate);
            totalPointOverall.Add(newPoint);
        }
        return sum;
    }

    private float GetTurretRatioToBarricade()
    {
        int turretnCnt=0;
        for (int i = 0; i < techManager.BuildingDatas.Count; i++)
        { 
            var list = techManager.GetBuildingAttackCompoListOf(techManager.BuildingDatas[i]);
            if (list != null)
                turretnCnt += list.Count;
        }
        BuildingData barricadeOriginData = techManager.GetBuildingDataFrom("Barricade");
        return turretnCnt / techManager.GetPresentBuildingListOf(barricadeOriginData).Count;
    }

    private int GetTotalBuildingCnt()
    {
        int sum = 0;
        for(int i=0; i < techManager.BuildingDatas.Count;i++)
        {
            var list = techManager.GetPresentBuildingListOf(techManager.BuildingDatas[i]);
            if (list == null)
                continue;
            sum += list.Count;
        }
        return sum;
    }

    private int GetTotalDefenseBuildingCnt()
    {
        int sum = 0;
        for(int i = 0; i < techManager.BuildingDatas.Count; i++)
        {
            var list = techManager.GetBuildingAttackCompoListOf(techManager.BuildingDatas[i]);
            if (list != null)
                sum += list.Count;
            else if (techManager.BuildingDatas[i].BuildingPrefab.CompareTag("Barricade"))
                sum += techManager.GetPresentBuildingListOf(techManager.BuildingDatas[i]).Count;
        }
        return sum;
    }

    private int GetTotalBuildingHealthPoint()
    {
        int sum = 0;
        for (int i = 0; i < techManager.BuildingDatas.Count; i++)
        {
            var list = techManager.GetBuildingEntityListOf(techManager.BuildingDatas[i]);
            if (list != null)
                foreach (var a in list)
                    sum += a.HealthPoint;
        }
        return sum;
    }

    private int GetTotalBuildingArmorPoint()
    {
        int sum = 0;
        for (int i = 0; i < techManager.BuildingDatas.Count; i++)
        {
            var list = techManager.GetBuildingEntityListOf(techManager.BuildingDatas[i]);
            if (list != null)
                foreach (var a in list)
                    sum += a.ArmorPoint;
        }
        return sum;
    }

    private int GetTotalBuildingAttackDamage()
    {
        int sum = 0;
        foreach (var a in techManager.BuildingDatas)
        {
            var list = techManager.GetBuildingAttackCompoListOf(a);
            if (list == null)
                continue;
            foreach (var b in list)
                sum += b.ModifiedAttackDamage;
        }
        return sum;
    }
}
