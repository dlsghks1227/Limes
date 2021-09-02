using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityScript.Lang;


[Serializable]
public class BuildTechManger
{
    [SerializeField]
    private BuildingData[] buildingDatas = new BuildingData[10];
    public ReadOnlyArray<BuildingData> BuildingDatas
    {
        private set => BuildingDatas = value;
        get => buildingDatas;
    }

    private Dictionary<BuildingData, List<GameObject>> presentBuildingListByType = new Dictionary<BuildingData, List<GameObject>>();
    private Dictionary<BuildingData, List<BuildingEntity>> buildEntityListByType = new Dictionary<BuildingData, List<BuildingEntity>>();
    private Dictionary<BuildingData, List<AttackComponent>> buildAttackCompoListByType = new Dictionary<BuildingData, List<AttackComponent>>();
    private Dictionary<BuildingData, List<SearchComponent>> buildSearchCompoListByType = new Dictionary<BuildingData, List<SearchComponent>>();

    public ReadOnlyCollection<GameObject> GetPresentBuildingListOf(BuildingData buildingType)
    {
        if (presentBuildingListByType.ContainsKey(buildingType))
            return presentBuildingListByType[buildingType].AsReadOnly();
        return null;
    }

    public void InitAllBuildingList()
    {
        foreach (var list in presentBuildingListByType)
            if (list.Value != null)
                list.Value.Clear();
    }

    public void InitBuildingListOf(BuildingData buildingData)
    {
        if (presentBuildingListByType[buildingData] != null)
            presentBuildingListByType[buildingData].Clear();
    }

    public void DeactiveAllBuildings()
    {
        foreach (var list in presentBuildingListByType)
            if (list.Value != null)
                foreach (var unit in list.Value)
                    unit.SetActive(false);
    }

    public void DeactiveBuildingOfType(BuildingData buildData)
    {
        if (!buildData)
            return; 

        var list = presentBuildingListByType[buildData];
        if (list != null)
            foreach (var unit in list)
                unit.SetActive(false);
    }

    public ReadOnlyCollection<BuildingEntity> GetBuildingEntityListOf(BuildingData buildingType)
    {
        if (buildEntityListByType.ContainsKey(buildingType))
            return buildEntityListByType[buildingType].AsReadOnly();
        return null;
    }

    public ReadOnlyCollection<AttackComponent> GetBuildingAttackCompoListOf(BuildingData buildingType)
    {
        if (buildAttackCompoListByType.ContainsKey(buildingType))
            return buildAttackCompoListByType[buildingType].AsReadOnly();
        return null;
    }

    public ReadOnlyCollection<SearchComponent> GetBuildingSearchCompoListOf(BuildingData buildingType)
    {
        if (buildSearchCompoListByType.ContainsKey(buildingType))
            return buildSearchCompoListByType[buildingType].AsReadOnly();
        return null;
    }

    public BuildingData GetBuildingDataFrom(GameObject building)
    {
        string originName = GetOriginDataNameFrom(building);
        return GetBuildingDataFrom(originName);
    }

    public BuildingData GetBuildingDataFrom(string name)
    {
        for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
            if (buildingDatas[i].BuildingPrefab.name.Equals(name))
                return buildingDatas[i];
        return null;
    }

    private string GetOriginDataNameFrom(GameObject building)
    {
        return GetOriginDataNameFrom(building.name);
    }

    private string GetOriginDataNameFrom(string name)
    {
        int strinLength = name.Length;
        //exclude "(Clone)" from end of name
        return name.Substring(0, strinLength - 7);
    }

    public void AddPresentBuildings(GameObject building)
    {
        BuildingData originType = GetBuildingDataFrom(building);
        if (!originType)
            return;

        if (!presentBuildingListByType.ContainsKey(originType))
            InitCacheDataStorage(originType);

        presentBuildingListByType[originType].Add(building);
        CacheBuildingDataCompos(building, originType);
    }

    private void InitCacheDataStorage(BuildingData buildingData)
    {
        presentBuildingListByType.Add(buildingData, new List<GameObject>(200));
        buildEntityListByType.Add(buildingData, new List<BuildingEntity>(200));

        if (buildingData.BuildingPrefab.GetComponent<AttackComponent>())
            buildAttackCompoListByType.Add(buildingData, new List<AttackComponent>(200));
        if (buildingData.BuildingPrefab.GetComponent<SearchComponent>())
            buildSearchCompoListByType.Add(buildingData, new List<SearchComponent>(200));
    }

    private void CacheBuildingDataCompos(GameObject building, BuildingData buildData = null)
    {
        buildEntityListByType[buildData].Add(building.GetComponent<BuildingEntity>());

        if (buildAttackCompoListByType.ContainsKey(buildData))
            buildAttackCompoListByType[buildData].Add(building.GetComponent<AttackComponent>());
        if (buildSearchCompoListByType.ContainsKey(buildData))
            buildSearchCompoListByType[buildData].Add(building.GetComponent<SearchComponent>());
    }

    public void AllowBuildingGeneratePermission(BuildingData buildData)
    {
        buildData.IsGeneratbale = true;
    }

    public void AdjustBuildingIronCost(float ratio, BuildingData specificBuilding = null)
    {
        if (!specificBuilding)
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].AdjustIronCost(ratio);
        else
            specificBuilding.AdjustIronCost(ratio);
    }

    public void AdjustBuildingIronCost(int value, BuildingData specificBuilding = null)
    {
        if (!specificBuilding)
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].AdjustIronCost(value);
        else
            specificBuilding.AdjustIronCost(value);
    }

    public void AdjustBuildingGoldCost(float ratio, BuildingData specificBuilding = null)
    {
        if (!specificBuilding)
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].AdjustGoldCost(ratio);
        else
            specificBuilding.AdjustGoldCost(ratio);
    }

    public void AdjustBuildingGoldCost(int value, BuildingData specificBuilding = null)
    {
        if (!specificBuilding)
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].AdjustGoldCost(value);
        else
            specificBuilding.AdjustGoldCost(value);
    }

    public void AdjustBuildingGenrateTime(float ratio, BuildingData specificBuilding = null)
    {
        if (!specificBuilding)
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].AdjustGenerateTime(ratio);
        else
            specificBuilding.AdjustGenerateTime(ratio);
    }

    public void AdjustBuildingGenrateTime(int value, BuildingData specificBuilding = null)
    {
        if (!specificBuilding)
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].AdjustGenerateTime(value);
        else
            specificBuilding.AdjustGenerateTime(value);
    }

    public void AdjustBuildHealthPoint(float ratio, BuildingData specificBuilding = null)
    {
        if (specificBuilding)
        {
            specificBuilding.BuildingPrefab.GetComponent<BuildingEntity>().AdjustHealthPoint(ratio);
            foreach (var a in buildEntityListByType[specificBuilding])
                a.AdjustHealthPoint(ratio);
        }
        else
        {
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].BuildingPrefab.GetComponent<BuildingEntity>().AdjustHealthPoint(ratio);

            foreach (var a in buildEntityListByType)
                foreach (var b in a.Value)
                    b.AdjustHealthPoint(ratio);
        }
    }

    public void AdjustBuildHealthPoint(int value, BuildingData specificBuilding = null)
    {
        if (specificBuilding)
        {
            specificBuilding.BuildingPrefab.GetComponent<BuildingEntity>().AdjustHealthPoint(value);
            foreach (var a in buildEntityListByType[specificBuilding])
                a.AdjustHealthPoint(value);
        }
        else
        {
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].BuildingPrefab.GetComponent<BuildingEntity>().AdjustHealthPoint(value);

            foreach (var a in buildEntityListByType)
                foreach (var b in a.Value)
                    b.AdjustHealthPoint(value);
        }
    }

    public void AdjustBuildArmorPoint(float ratio, BuildingData specificBuilding = null)
    {
        if (specificBuilding)
        {
            specificBuilding.BuildingPrefab.GetComponent<BuildingEntity>().AdjustArmorPoint(ratio);
            foreach (var a in buildEntityListByType[specificBuilding])
                a.AdjustArmorPoint(ratio);
        }
        else
        {
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].BuildingPrefab.GetComponent<BuildingEntity>().AdjustArmorPoint(ratio);

            foreach (var a in buildEntityListByType)
                foreach (var b in a.Value)
                    b.AdjustArmorPoint(ratio);
        }
    }

    public void AdjustBuildArmorPoint(int value, BuildingData specificBuilding = null)
    {
        if (specificBuilding)
        {
            specificBuilding.BuildingPrefab.GetComponent<BuildingEntity>().AdjustArmorPoint(value);
            foreach (var a in buildEntityListByType[specificBuilding])
                a.AdjustArmorPoint(value);
        }
        else
        {
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].BuildingPrefab.GetComponent<BuildingEntity>().AdjustArmorPoint(value);

            foreach (var a in buildEntityListByType)
                foreach (var b in a.Value)
                    b.AdjustArmorPoint(value);
        }
    }

    public void AdjustBuildAttackDamage(float ratio, BuildingData specificBuilding = null)
    {
        if (specificBuilding)
        {
            specificBuilding.BuildingPrefab.GetComponent<AttackComponent>().AdjustAttackDamage(ratio);
            foreach (var a in buildAttackCompoListByType[specificBuilding])
                a.AdjustAttackDamage(ratio);
        }
        else
        {
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].BuildingPrefab.GetComponent<AttackComponent>().AdjustAttackDamage(ratio);

            foreach (var a in buildAttackCompoListByType)
                foreach (var b in a.Value)
                    b.AdjustAttackDamage(ratio);
        }
    }

    public void AdjustBuildAttackDamage(int value, BuildingData specificBuilding = null)
    {
        if (specificBuilding)
        {
            specificBuilding.BuildingPrefab.GetComponent<AttackComponent>().AdjustAttackDamage(value);
            foreach (var a in buildAttackCompoListByType[specificBuilding])
                a.AdjustAttackDamage(value);
        }
        else
        {
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].BuildingPrefab.GetComponent<AttackComponent>().AdjustAttackDamage(value);

            foreach (var a in buildAttackCompoListByType)
                foreach (var b in a.Value)
                    b.AdjustAttackDamage(value);
        }
    }

    public void AdjustBuildAttackRagne(float ratio, BuildingData specificBuilding = null)
    {
        if (specificBuilding)
        {
            specificBuilding.BuildingPrefab.GetComponent<AttackComponent>().AdjustAttackRange(ratio);
            foreach (var a in buildAttackCompoListByType[specificBuilding])
                a.AdjustAttackRange(ratio);
        }
        else
        {
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].BuildingPrefab.GetComponent<AttackComponent>().AdjustAttackRange(ratio);

            foreach (var a in buildAttackCompoListByType)
                foreach (var b in a.Value)
                    b.AdjustAttackRange(ratio);
        }
    }

    public void AdjustBuildAttackRange(int value, BuildingData specificBuilding = null)
    {
        if (specificBuilding)
        {
            specificBuilding.BuildingPrefab.GetComponent<AttackComponent>().AdjustAttackRange(value);
            foreach (var a in buildAttackCompoListByType[specificBuilding])
                a.AdjustAttackRange(value);
        }
        else
        {
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].BuildingPrefab.GetComponent<AttackComponent>().AdjustAttackRange(value);

            foreach (var a in buildAttackCompoListByType)
                foreach (var b in a.Value)
                    b.AdjustAttackRange(value);
        }
    }

    public void AdjustBuildAttackSpeed(float ratio, BuildingData specificBuilding = null)
    {
        if (specificBuilding)
        {
            specificBuilding.BuildingPrefab.GetComponent<AttackComponent>().AdjustAttackSpeed(ratio);
            foreach (var a in buildAttackCompoListByType[specificBuilding])
                a.AdjustAttackSpeed(ratio);
        }
        else
        {
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].BuildingPrefab.GetComponent<AttackComponent>().AdjustAttackSpeed(ratio);

            foreach (var a in buildAttackCompoListByType)
                foreach (var b in a.Value)
                    b.AdjustAttackSpeed(ratio);
        }
    }

    public void AdjustBuildAttackSpeed(int value, BuildingData specificBuilding = null)
    {
        if (specificBuilding)
        {
            specificBuilding.BuildingPrefab.GetComponent<AttackComponent>().AdjustAttackSpeed(value);
            foreach (var a in buildAttackCompoListByType[specificBuilding])
                a.AdjustAttackSpeed(value);
        }
        else
        {
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].BuildingPrefab.GetComponent<AttackComponent>().AdjustAttackSpeed(value);

            foreach (var a in buildAttackCompoListByType)
                foreach (var b in a.Value)
                    b.AdjustAttackSpeed(value);
        }
    }

    public void AdjustBuildSearchRadius(float ratio, BuildingData specificBuilding = null)
    {
        if (specificBuilding)
        {
            specificBuilding.BuildingPrefab.GetComponent<SearchComponent>().AdjustSearchRadius(ratio);
            foreach (var a in buildSearchCompoListByType[specificBuilding])
                a.AdjustSearchRadius(ratio);
        }
        else
        {
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].BuildingPrefab.GetComponent<SearchComponent>().AdjustSearchRadius(ratio);

            foreach (var a in buildSearchCompoListByType)
                foreach (var b in a.Value)
                    b.AdjustSearchRadius(ratio);
        }
    }

    public void AdjustBuildSearchRadius(int value, BuildingData specificBuilding = null)
    {
        if (specificBuilding)
        {
            specificBuilding.BuildingPrefab.GetComponent<SearchComponent>().AdjustSearchRadius(value);
            foreach (var a in buildSearchCompoListByType[specificBuilding])
                a.AdjustSearchRadius(value);
        }
        else
        {
            for (int i = 0; i < buildingDatas.Length && buildingDatas[i]; i++)
                buildingDatas[i].BuildingPrefab.GetComponent<SearchComponent>().AdjustSearchRadius(value);

            foreach (var a in buildSearchCompoListByType)
                foreach (var b in a.Value)
                    b.AdjustSearchRadius(value);
        }
    }

    public void UpgradeBarricadeElevate()
    {
        var barricadeOrigin = GetBuildingDataFrom("Barricade");
        barricadeOrigin.BuildingPrefab.GetComponent<Barricade>().IsElevatable = true;

        var barricadeList = GetPresentBuildingListOf(barricadeOrigin);
        if (barricadeList != null)
            foreach (var a in barricadeList)
                a.GetComponent<Barricade>().IsElevatable = true;
    }
}


[Serializable]
public class UnitTechManager
{
    [SerializeField]
    private UnitData[] unitDatas = new UnitData[10];
    public ReadOnlyArray<UnitData> UnitDatas
    {
        get => unitDatas;
    }
    private Dictionary<UnitData, List<GameObject>> presentUnitListByUnitData = new Dictionary<UnitData, List<GameObject>>();
    private Dictionary<UnitData, List<UnitEntity>> entityListByUnitData = new Dictionary<UnitData, List<UnitEntity>>();
    private Dictionary<UnitData, List<AttackComponent>> attackCompoListByUnitData = new Dictionary<UnitData, List<AttackComponent>>();
    private Dictionary<UnitData, List<MovementComponent>> movementCompoListByUnitData = new Dictionary<UnitData, List<MovementComponent>>();
    private Dictionary<UnitData, List<SearchComponent>> searchCompoListByUnitData = new Dictionary<UnitData, List<SearchComponent>>();

    public ReadOnlyCollection<GameObject> GetUnitListOf(UnitData specificUnit)
    {
        if (presentUnitListByUnitData.ContainsKey(specificUnit))
            return presentUnitListByUnitData[specificUnit].AsReadOnly();
        return null;
    }

    public void InitAllUnitList()
    {
        foreach (var list in presentUnitListByUnitData)
            if (list.Value != null)
                list.Value.Clear();
    }

    public void InitUnitListOf(UnitData unitData)
    {
        if (presentUnitListByUnitData[unitData] != null)
            presentUnitListByUnitData[unitData].Clear();
    }

    public void DeactiveAllUnit()
    {
        foreach (var list in presentUnitListByUnitData)
            if (list.Value != null)
                foreach (var unit in list.Value)
                    unit.SetActive(false);
    }

    public void DeactiveUnitOfType(UnitData unitData)
    {
        if (!unitData)
            return;

        var list = presentUnitListByUnitData[unitData];
        if (list != null)
            foreach (var unit in list)
                unit.SetActive(false);
    }

    public ReadOnlyCollection<UnitEntity> GetUnitEntitiCompoListOf(UnitData specificUnit)
    {
        return entityListByUnitData[specificUnit].AsReadOnly();
    }

    public ReadOnlyCollection<AttackComponent> GetAttackCompoListOf(UnitData specificUnit)
    {
        if (attackCompoListByUnitData.ContainsKey(specificUnit))
            return attackCompoListByUnitData[specificUnit].AsReadOnly();
        return null;
    }

    public ReadOnlyCollection<SearchComponent> GetSearchCompoListOf(UnitData specificUnit)
    {
        if (searchCompoListByUnitData.ContainsKey(specificUnit))
            return searchCompoListByUnitData[specificUnit].AsReadOnly();
        return null;
    }

    public ReadOnlyCollection<MovementComponent> GetMovementCompoListOf(UnitData specificUnit)
    {
        if (movementCompoListByUnitData.ContainsKey(specificUnit))
            return movementCompoListByUnitData[specificUnit].AsReadOnly();
        return null;
    }

    public UnitData GetUnitDataBy(GameObject unit)
    {
        return GetUnitDataBy(GetUnitDataNameFrom(unit));
    }

    public UnitData GetUnitDataBy(string name)
    {
        for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
            if (unitDatas[i].Prefab.name.Equals(name))
                return unitDatas[i];
        return null;
    }

    private string GetUnitDataNameFrom(GameObject unit)
    {
        int nameLength = unit.name.Length;
        //exclude "(Clone)" string from name
        nameLength -= 7;
        return unit.name.Substring(0, nameLength);
    }

    public void AllowUnitGeneratePermission(UnitData unitData)
    {
        unitData.IsGeneratable = true;
    }

    public void AddPresentUnit(GameObject unit)
    {
        UnitData unitOrigin = GetUnitDataBy(unit);
        if (!presentUnitListByUnitData.ContainsKey(unitOrigin))
            InitPresentUnitCacheList(unitOrigin);

        if (presentUnitListByUnitData[unitOrigin].Contains(unit))
            return;

        presentUnitListByUnitData[unitOrigin].Add(unit);
        CachePresentUnitComponents(unit, unitOrigin);
    }

    private void InitPresentUnitCacheList(UnitData unitData)
    {
        presentUnitListByUnitData.Add(unitData, new List<GameObject>(100));
        entityListByUnitData.Add(unitData, new List<UnitEntity>(100));

        if (unitData.Prefab.GetComponent<AttackComponent>())
            attackCompoListByUnitData.Add(unitData, new List<AttackComponent>(100));
        if (unitData.Prefab.GetComponent<SearchComponent>())
            searchCompoListByUnitData.Add(unitData, new List<SearchComponent>(100));
        if (unitData.Prefab.GetComponent<MovementComponent>())
            movementCompoListByUnitData.Add(unitData, new List<MovementComponent>(100));
    }

    private void CachePresentUnitComponents(GameObject unit, UnitData unitData)
    {
        entityListByUnitData[unitData].Add(unit.GetComponent<UnitEntity>());

        if (attackCompoListByUnitData.ContainsKey(unitData))
            attackCompoListByUnitData[unitData].Add(unit.GetComponent<AttackComponent>());
        if (movementCompoListByUnitData.ContainsKey(unitData))
            movementCompoListByUnitData[unitData].Add(unit.GetComponent<MovementComponent>());
        if (searchCompoListByUnitData.ContainsKey(unitData))
            searchCompoListByUnitData[unitData].Add(unit.GetComponent<SearchComponent>());
    }

    //Delete cached present Unit
    /* public void DeletePresentUnit(GameObject unit)
     {
         if (presentUnits.Contains(unit))
         {
             presentUnits.Remove(unit);
             DeleteCachedComponents(unit);
         }
     }

     private void DeleteCachedComponents(GameObject unit)
     {
         unitEntityList.Remove(unit.GetComponent<UnitEntity>());
         var attackComponent = unit.GetComponent<AttackComponent>();
         if (attackComponent)
             unitAttackCompoList.Remove(attackComponent);
         var moveComponent = unit.GetComponent<MovementComponent>();
         if (moveComponent)
             unitMovementCompoList.Remove(moveComponent);
         var searchComponent = unit.GetComponent<SearchComponent>();
         if (searchComponent)
             unitSearchCompoList.Remove(searchComponent);
     }
    */

    public void AdjustUnitIronCost(float ratio, UnitData specificUnit = null)
    {
        if (!specificUnit)
            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].AdjustIronCost(ratio);
        else
            specificUnit.AdjustIronCost(ratio);
    }

    public void AdjustUnitIronCost(int value, UnitData specificUnit = null)
    {
        if (!specificUnit)
            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].AdjustIronCost(value);
        else
            specificUnit.AdjustIronCost(value);
    }

    public void AdjustUnitGoldCost(float ratio, UnitData specificUnit = null)
    {
        if (!specificUnit)
            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].AdjustGoldCost(ratio);
        else
            specificUnit.AdjustGoldCost(ratio);
    }

    public void AdjustUnitGoldCost(int value, UnitData specificUnit = null)
    {
        if (!specificUnit)
            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].AdjustGoldCost(value);
        else
            specificUnit.AdjustGoldCost(value);
    }

    public void AdjustUnitGenrateTime(float ratio, UnitData specificUnit = null)
    {
        if (!specificUnit)
            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].AdjustGenerateTime(ratio);
        else
            specificUnit.AdjustGenerateTime(ratio);
    }

    public void AdjustUnitGenrateTime(int value, UnitData specificUnit = null)
    {
        if (!specificUnit)
            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].AdjustGenerateTime(value);
        else
            specificUnit.AdjustGenerateTime(value);
    }

    public void AdjustUnitDamage(float ratio, UnitData specificUnit = null)
    {
        if (!specificUnit)
        {
            foreach (var a in attackCompoListByUnitData)
                foreach (var b in a.Value)
                    b.AdjustAttackDamage(ratio);

            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].Prefab.GetComponentInChildren<AttackComponent>(true).AdjustAttackDamage(ratio);
        }
        else
        {
            specificUnit.Prefab.GetComponent<AttackComponent>().AdjustAttackDamage(ratio);
            foreach (var a in attackCompoListByUnitData[specificUnit])
                a.AdjustAttackDamage(ratio);
        }
    }

    public void AdjustUnitDamage(int value, UnitData specificUnit = null)
    {
        if (!specificUnit)
        {
            foreach (var a in attackCompoListByUnitData)
                foreach (var b in a.Value)
                    b.AdjustAttackDamage(value);

            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].Prefab.GetComponentInChildren<AttackComponent>(true).AdjustAttackDamage(value);
        }
        else
        {
            specificUnit.Prefab.GetComponent<AttackComponent>().AdjustAttackDamage(value);
            foreach (var a in attackCompoListByUnitData[specificUnit])
                a.AdjustAttackDamage(value);
        }
    }

    public void AdjustUnitAttackSpeed(float ratio, UnitData specificUnit = null)
    {
        if (!specificUnit)
        {
            foreach (var a in attackCompoListByUnitData)
                foreach (var b in a.Value)
                    b.AdjustAttackSpeed(ratio);

            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].Prefab.GetComponentInChildren<AttackComponent>(true).AdjustAttackSpeed(ratio);
        }
        else
        {
            specificUnit.Prefab.GetComponent<AttackComponent>().AdjustAttackSpeed(ratio);
            foreach (var a in attackCompoListByUnitData[specificUnit])
                a.AdjustAttackSpeed(ratio);
        }
    }

    public void AdjustUnitAttackSpeed(int value, UnitData specificUnit = null)
    {
        if (!specificUnit)
        {
            foreach (var a in attackCompoListByUnitData)
                foreach (var b in a.Value)
                    b.AdjustAttackSpeed(value);

            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].Prefab.GetComponentInChildren<AttackComponent>(true).AdjustAttackSpeed(value);
        }
        else
        {
            specificUnit.Prefab.GetComponent<AttackComponent>().AdjustAttackSpeed(value);
            foreach (var a in attackCompoListByUnitData[specificUnit])
                a.AdjustAttackSpeed(value);
        }
    }

    public void AdjustUnitAttackRange(float ratio, UnitData specificUnit = null)
    {
        if (!specificUnit)
        {
            foreach (var a in attackCompoListByUnitData)
                foreach (var b in a.Value)
                    b.AdjustAttackRange(ratio);

            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].Prefab.GetComponentInChildren<AttackComponent>(true).AdjustAttackRange(ratio);
        }
        else
        {
            specificUnit.Prefab.GetComponent<AttackComponent>().AdjustAttackRange(ratio);
            foreach (var a in attackCompoListByUnitData[specificUnit])
                a.AdjustAttackRange(ratio);
        }
    }

    public void AdjustUnitAttackRange(int value, UnitData specificUnit = null)
    {
        if (!specificUnit)
        {
            foreach (var a in attackCompoListByUnitData)
                foreach (var b in a.Value)
                    b.AdjustAttackRange(value);

            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].Prefab.GetComponentInChildren<AttackComponent>(true).AdjustAttackRange(value);
        }
        else
        {
            specificUnit.Prefab.GetComponent<AttackComponent>().AdjustAttackRange(value);
            foreach (var a in attackCompoListByUnitData[specificUnit])
                a.AdjustAttackRange(value);
        }
    }

    public void AdjustUnitMoveSpeed(float ratio, UnitData specificUnit = null)
    {
        if (!specificUnit)
        {
            foreach (var a in movementCompoListByUnitData)
                foreach (var b in a.Value)
                    b.AdjustSpeed(ratio);

            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].Prefab.GetComponentInChildren<MovementComponent>(true).AdjustSpeed(ratio);
        }
        else
        {
            specificUnit.Prefab.GetComponent<MovementComponent>().AdjustSpeed(ratio);
            foreach (var a in movementCompoListByUnitData[specificUnit])
                a.AdjustSpeed(ratio);
        }
    }

    public void AdjustUnitMoveSpeed(int value, UnitData specificUnit = null)
    {
        if (!specificUnit)
        {
            foreach (var a in movementCompoListByUnitData)
                foreach (var b in a.Value)
                    b.AdjustSpeed(value);

            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].Prefab.GetComponentInChildren<MovementComponent>(true).AdjustSpeed(value);
        }
        else
        {
            specificUnit.Prefab.GetComponent<MovementComponent>().AdjustSpeed(value);
            foreach (var a in movementCompoListByUnitData[specificUnit])
                a.AdjustSpeed(value);
        }
    }

    public void AdjustUnitSearchRadius(float ratio, UnitData specificUnit = null)
    {
        if (!specificUnit)
        {
            foreach (var a in searchCompoListByUnitData)
                foreach (var b in a.Value)
                    b.AdjustSearchRadius(ratio);

            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].Prefab.GetComponentInChildren<SearchComponent>(true).AdjustSearchRadius(ratio);
        }
        else
        {
            specificUnit.Prefab.GetComponent<SearchComponent>().AdjustSearchRadius(ratio);
            foreach (var a in searchCompoListByUnitData[specificUnit])
                a.AdjustSearchRadius(ratio);
        }
    }

    public void AdjustUnitSearchRadius(int value, UnitData specificUnit = null)
    {
        if (!specificUnit)
        {
            foreach (var a in searchCompoListByUnitData)
                foreach (var b in a.Value)
                    b.AdjustSearchRadius(value);

            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].Prefab.GetComponentInChildren<SearchComponent>(true).AdjustSearchRadius(value);
        }
        else
        {
            specificUnit.Prefab.GetComponent<SearchComponent>().AdjustSearchRadius(value);
            foreach (var a in searchCompoListByUnitData[specificUnit])
                a.AdjustSearchRadius(value);
        }
    }

    public void AdjustUnitHealthPoint(float ratio, UnitData specificUnit = null)
    {
        if (!specificUnit)
        {
            foreach (var a in entityListByUnitData)
                foreach (var b in a.Value)
                    b.AdjustHealthPoint(ratio);

            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].Prefab.GetComponentInChildren<UnitEntity>(true).AdjustHealthPoint(ratio);
        }
        else
        {
            specificUnit.Prefab.GetComponent<UnitEntity>().AdjustHealthPoint(ratio);
            foreach (var a in entityListByUnitData[specificUnit])
                a.AdjustHealthPoint(ratio);
        }
    }

    public void AdjustUnitHealthPoint(int value, UnitData specificUnit = null)
    {
        if (!specificUnit)
        {
            foreach (var a in entityListByUnitData)
                foreach (var b in a.Value)
                    b.AdjustHealthPoint(value);

            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].Prefab.GetComponentInChildren<UnitEntity>(true).AdjustHealthPoint(value);
        }
        else
        {
            specificUnit.Prefab.GetComponent<UnitEntity>().AdjustHealthPoint(value);
            foreach (var a in entityListByUnitData[specificUnit])
                a.AdjustHealthPoint(value);
        }
    }

    public void AdjustUnitArmorPoint(float ratio, UnitData specificUnit = null)
    {
        if (!specificUnit)
        {
            foreach (var a in entityListByUnitData)
                foreach (var b in a.Value)
                    b.AdjustArmorPoint(ratio);

            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].Prefab.GetComponentInChildren<UnitEntity>(true).AdjustArmorPoint(ratio);
        }
        else
        {
            specificUnit.Prefab.GetComponent<UnitEntity>().AdjustArmorPoint(ratio);
            foreach (var a in entityListByUnitData[specificUnit])
                a.AdjustArmorPoint(ratio);
        }
    }

    public void AdjustUnitArmorPoint(int value, UnitData specificUnit = null)
    {
        if (!specificUnit)
        {
            foreach (var a in entityListByUnitData)
                foreach (var b in a.Value)
                    b.AdjustArmorPoint(value);

            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].Prefab.GetComponentInChildren<UnitEntity>(true).AdjustArmorPoint(value);
        }
        else
        {
            specificUnit.Prefab.GetComponent<UnitEntity>().AdjustArmorPoint(value);
            foreach (var a in entityListByUnitData[specificUnit])
                a.AdjustArmorPoint(value);
        }
    }

    public void AdjustUnitArmorPierce(float value, UnitData specificUnit = null)
    {
        if (!specificUnit)
        {
            foreach (var a in attackCompoListByUnitData)
                foreach (var b in a.Value)
                    b.AdjustArmorPierce(value);

            for (int i = 0; i < unitDatas.Length && unitDatas[i]; i++)
                unitDatas[i].Prefab.GetComponentInChildren<AttackComponent>(true).AdjustArmorPierce(value);
        }
        else
        {
            specificUnit.Prefab.GetComponent<AttackComponent>().AdjustArmorPierce(value);
            foreach (var a in attackCompoListByUnitData[specificUnit])
                a.AdjustArmorPierce(value);
        }
    }
}


public class TechManager : MonoSingleton<TechManager>
{
    [SerializeField]
    private UnitTechManager unitTechManager;
    [SerializeField]
    private BuildTechManger buildTechManager;

    #region unit data structures
    public ReadOnlyArray<UnitData> UnitDatas
    {
        get => unitTechManager.UnitDatas;
    }

    public ReadOnlyCollection<GameObject> GetUnitListOf(UnitData unitData)
    {
        UpdatePresentUnits();
        return unitTechManager.GetUnitListOf(unitData);
    }

    public void InitAllUnitList()
    {
        unitTechManager.InitAllUnitList();
    }

    public void InitUnitListOf(UnitData data)
    {
        unitTechManager.InitUnitListOf(data);
    }

    public void DeactiveAllUnit()
    {
        unitTechManager.DeactiveAllUnit();
    }

    public void DeactiveUnitOfType(UnitData unit)
    {
        unitTechManager.DeactiveUnitOfType(unit);
    }

    public ReadOnlyCollection<UnitEntity> GetUnitEnityListOf(UnitData unitData)
    {
        UpdatePresentUnits();
        return unitTechManager.GetUnitEntitiCompoListOf(unitData);
    }

    public ReadOnlyCollection<AttackComponent> GetAttackCompoListOf(UnitData unitData)
    {
        UpdatePresentUnits();
        return unitTechManager.GetAttackCompoListOf(unitData);
    }

    public ReadOnlyCollection<SearchComponent> GetSearchCompoListOf(UnitData unitData)
    {
        UpdatePresentUnits();
        return unitTechManager.GetSearchCompoListOf(unitData);
    }

    public ReadOnlyCollection<MovementComponent> GetMovementCompoListOf(UnitData unitData)
    {
        UpdatePresentUnits();
        return unitTechManager.GetMovementCompoListOf(unitData);
    }

    public UnitData GetUnitDataFrom(string name)
    {
        return unitTechManager.GetUnitDataBy(name);
    }

    public UnitData GetUnitDataFrom(GameObject unit)
    {
        return unitTechManager.GetUnitDataBy(unit);
    }

    private void UpdatePresentUnits()
    {
        var barrackData = GetBuildingDataFrom("Barrack");
        var barrackList = GetPresentBuildingListOf(barrackData);
        if (barrackList == null)
            return;

        foreach (var a in barrackList)
        {
            if (a == null)
                continue;
            for (int i = 0; i < UnitDatas.Count && UnitDatas[i]; i++)
            {
                var list = a.GetComponent<UnitGeneratorBarrack>().GetGeneratedUnitListOf(UnitDatas[i]);
                if (list != null)
                    foreach (var b in list)
                        AddPresentUnit(b);
            }
        }
    }
    #endregion

    #region build data structures

    public ReadOnlyArray<BuildingData> BuildingDatas
    {
        get => buildTechManager.BuildingDatas;
    }

    public ReadOnlyCollection<GameObject> GetPresentBuildingListOf(BuildingData building)
    {
        return buildTechManager.GetPresentBuildingListOf(building);
    }

    public void InitAllBuildingList()
    {
        buildTechManager.InitAllBuildingList();
    }

    public void InitBuildingListOf(BuildingData data)
    {
        buildTechManager.InitBuildingListOf(data);
    }

    public void DeactiveAllBuilding()
    {
        buildTechManager.DeactiveAllBuildings();
    }

    public void DeactiveBuildingOfTyPE(BuildingData data)
    {
        buildTechManager.DeactiveBuildingOfType(data);
    }

    public ReadOnlyCollection<BuildingEntity> GetBuildingEntityListOf(BuildingData building)
    {
        return buildTechManager.GetBuildingEntityListOf(building);
    }

    public ReadOnlyCollection<SearchComponent> GetBuildingSearchCompoListOf(BuildingData building)
    {
        return buildTechManager.GetBuildingSearchCompoListOf(building);
    }

    public ReadOnlyCollection<AttackComponent> GetBuildingAttackCompoListOf(BuildingData building)
    {
        return buildTechManager.GetBuildingAttackCompoListOf(building);
    }

    public BuildingData GetBuildingDataFrom(GameObject building)
    {
        return buildTechManager.GetBuildingDataFrom(building);
    }

    public BuildingData GetBuildingDataFrom(string name)
    {
        return buildTechManager.GetBuildingDataFrom(name);
    }
    #endregion

    #region unit data edit methods
    public void AllowUnitGeneatePermission(UnitData unitData)
    {
        unitTechManager.AllowUnitGeneratePermission(unitData);
    }

    public void AdjustUnitIronCost(float ratio, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitIronCost(ratio, specificUnit);
    }

    public void AdjustUnitIronCost(int value, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitIronCost(value, specificUnit);
    }

    public void AdjustUnitGoldCost(float ratio, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitGoldCost(ratio, specificUnit);
    }

    public void AdjustUnitGoldCost(int value, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitGoldCost(value, specificUnit);
    }

    public void AdjustUnitGenerateTime(float ratio, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitGenrateTime(ratio, specificUnit);
    }

    public void AdjustUnitGenerateTime(int value, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitGenrateTime(value, specificUnit);
    }

    public void AddPresentUnit(GameObject unit)
    {
        unitTechManager.AddPresentUnit(unit);
    }

    public void AdjustUnitHealthPoint(float ratio, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitHealthPoint(ratio, specificUnit);
    }

    public void AdjustUnitHealthPoint(int value, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitHealthPoint(value, specificUnit);
    }

    public void AdjustUnitArmorPoint(int value, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitArmorPoint(value, specificUnit);
    }

    public void AdjustUnitArmorPoint(float ratio, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitArmorPoint(ratio, specificUnit);
    }

    public void AdjustUnitAttackDamage(float ratio, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitDamage(ratio, specificUnit);
    }

    public void AdjustUnitAttackDamage(int value, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitDamage(value, specificUnit);
    }

    public void AdjustUnitAttackRange(float ratio, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitAttackRange(ratio, specificUnit);
    }

    public void AdjustUnitAttackRange(int value, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitAttackRange(value, specificUnit);
    }

    public void AdjustUnitAttackSpeed(float ratio, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitAttackSpeed(ratio, specificUnit);
    }

    public void AdjustUnitAttackSpeed(int value, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitAttackSpeed(value, specificUnit);
    }

    public void AdjustUnitSearchRadius(float ratio, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitSearchRadius(ratio, specificUnit);
    }

    public void AdjustUnitSearchRadius(int value, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitSearchRadius(value, specificUnit);
    }

    public void AdjustUnitMoveSpeed(float ratio, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitMoveSpeed(ratio, specificUnit);
    }

    public void AdjustUnitMoveSpeed(int value, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitMoveSpeed(value, specificUnit);
    }
    #endregion

    #region build data edit methods
    public void AddPresentBuilding(GameObject build)
    {
        buildTechManager.AddPresentBuildings(build);
    }

    public void AllowBuildGeneratePermission(BuildingData building)
    {
        buildTechManager.AllowBuildingGeneratePermission(building);
    }

    public void AdjustBuildingIronCost(float ratio, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildingIronCost(ratio, specificBuilding);
    }

    public void AdjustBuildingIronCost(int value, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildingIronCost(value, specificBuilding);
    }

    public void AdjustBuildingGoldCost(float ratio, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildingGoldCost(ratio, specificBuilding);
    }

    public void AdjustBuildingGoldCost(int value, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildingGoldCost(value, specificBuilding);
    }

    public void AdjustBuildingGenerateTime(float ratio, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildingGenrateTime(ratio, specificBuilding);
    }

    public void AdjustBuildingGenerateTime(int value, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildingGenrateTime(value, specificBuilding);
    }

    public void AdjustBuildHealthPoint(float ratio, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildHealthPoint(ratio, specificBuilding);
    }

    public void AdjustBuildHealthPoint(int value, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildHealthPoint(value, specificBuilding);
    }

    public void AdjustBuildArmorPoint(float ratio, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildArmorPoint(ratio, specificBuilding);
    }

    public void AdjustBuildArmorPoint(int value, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildArmorPoint(value, specificBuilding);
    }

    public void AdjustBuildAttackDamage(float ratio, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildAttackDamage(ratio, specificBuilding);
    }

    public void AdjustBuildAttackDamage(int value, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildAttackDamage(value, specificBuilding);
    }

    public void AdjustBuildAttackRange(float ratio, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildAttackRagne(ratio, specificBuilding);
    }

    public void AdjustBuildAttackRange(int value, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildAttackRagne(value, specificBuilding);
    }

    public void AdjustBuildAttackSpeed(float ratio, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildAttackSpeed(ratio, specificBuilding);
    }

    public void AdjustBuildAttackSpeed(int value, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildAttackSpeed(value, specificBuilding);
    }

    public void AdjustBuildSearchRadius(float ratio, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildSearchRadius(ratio, specificBuilding);
    }

    public void AdjustBuildSearchRadius(int value, BuildingData specificBuilding = null)
    {
        buildTechManager.AdjustBuildSearchRadius(value, specificBuilding);
        buildTechManager.AdjustBuildSearchRadius(value, specificBuilding);
    }

    public void AdjustUnitArmorPierce(float value, UnitData specificUnit = null)
    {
        unitTechManager.AdjustUnitArmorPierce(value, specificUnit);
    }

    public void UpgradeBarricadeElevate()
    {
        buildTechManager.UpgradeBarricadeElevate();
    }
    #endregion
}
