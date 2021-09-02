using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class RaidPlayerAI : MonoBehaviour
{
    #region variables
    [SerializeField]
    public EnemyUnitGenerateAI unitGenerateAI;

    private Dictionary<UnitData, int> presentIndexByUnitType = new Dictionary<UnitData, int>();

    private ReadOnlyCollection<GameObject> commandCenterList;
    private Dictionary<Vector3, float> attackSizeRatioPerLocation = new Dictionary<Vector3, float>();

    [Header("Raid PreSetting")]
    [SerializeField]
    private float raidStartDelay = 100f;
    [SerializeField]
    private float playerBaseSizeScanRadius = 50f;
    [SerializeField]
    private float raidPeriod = 30f;
    [Header("Attack Size Adjustment")]
    [SerializeField]
    private int attackSizeThreshold = 15;
    [SerializeField]
    private float attackSizeScaleUpPerPeriod = 0.1f;
    [SerializeField]
    private float attackSizeScaleUpPeriod = 10f;
    private float attackSizeCoefficient = 1f;

    [Header("AirDrop Ratio Adjustment")]
    [SerializeField]
    private float airDropRatioIncreasePeriod = 20f;
    [SerializeField]
    private float airDropRatioIncease = 0.05f;

    public ReadOnlyDictionary<UnitData, List<GameObject>> GeneratedUnitListByType
    {
        get => unitGenerateAI.ReadOnlyGeneratedUnitListByType;
    }
    #endregion
    #region user func

    private void LoadPlayerCommandCenters()
    {
        var commandCenterData = TechManager.Instance.GetBuildingDataFrom("Command");
        commandCenterList = TechManager.Instance.GetPresentBuildingListOf(commandCenterData);
    }

    public IEnumerator Play()
    {
        yield return WaitTime.GetWaitForSecondOf(raidStartDelay);
        //StartCoroutine(AdjustAirDropRatio());
        StartCoroutine(ScaleUpAttackSize());
        while (true)
        {
            LoadPlayerCommandCenters();
            unitGenerateAI.GenerateUnits(attackSizeCoefficient);
            yield return WaitTime.GetWaitForSecondOf(raidPeriod);
            InitUnitsIndex();
            InitAttackSizePerLocation();
            AttackPlayerBase();
        }
    }

    private IEnumerator ScaleUpAttackSize()
    {
        while (true)
        {
            attackSizeCoefficient += attackSizeScaleUpPerPeriod;
            yield return WaitTime.GetWaitForSecondOf(attackSizeScaleUpPeriod);
        }
    }

    private IEnumerator AdjustAirDropRatio()
    {
        while (true)
        {
            unitGenerateAI.AirDropUnitRatio += airDropRatioIncease;
            if (unitGenerateAI.AirDropUnitRatio > 1f)
                break;
            yield return WaitTime.GetWaitForSecondOf(airDropRatioIncreasePeriod);
        }
    }

    public void InitAttackSizePerLocation()
    {
        RaycastHit[] dummyResult = new RaycastHit[100];
        attackSizeRatioPerLocation.Clear();
        foreach (var a in commandCenterList)
        {
            if (a == null)
                continue;
            Vector3 centerPos = a.transform.position;
            int size = Physics.SphereCastNonAlloc(centerPos, playerBaseSizeScanRadius, Vector3.one.normalized, dummyResult, 0.1f, Layers.PLAYER_UNIT_LAYER);
            if (!attackSizeRatioPerLocation.ContainsKey(a.transform.position))
                attackSizeRatioPerLocation.Add(centerPos, 0);
            attackSizeRatioPerLocation[centerPos] = size;
        }
    }

    private void AttackPlayerBase()
    {
        SetAttackSizeRatio();
        foreach (var a in attackSizeRatioPerLocation)
            MoveUnitsByRatio(a.Key, a.Value);

        //unitGenerateAI.OrderAirDropUnits(attackSizeRatioPerLocation);
    }

    private void SetAttackSizeRatio()
    {
        float sum = 0f;
        foreach (var a in attackSizeRatioPerLocation)
            sum += a.Value;
        foreach (var a in attackSizeRatioPerLocation.Keys.ToList())
            attackSizeRatioPerLocation[a] /= sum;
    }

    private void MoveUnitsByRatio(Vector3 pos, float ratio)
    {
        foreach (var a in unitGenerateAI.ReadOnlyGeneratedUnitListByType)
            if (a.Value != null && a.Value.Count > 0)
            {
                int attackSize = (int)((float)a.Value.Count * ratio);
                if (attackSize >= attackSizeThreshold)
                {
                    foreach (var b in a.Value)
                    {
                        int idx = presentIndexByUnitType[a.Key];
                        for (int i = 0; i < attackSize; i++)
                        {
                            idx += i;
                            if (idx >= a.Value.Count)
                                break;
                            GameObject unit = a.Value[idx];
                            if (unit)
                                unit.GetComponent<MovementComponent>().Move(pos);
                        }
                    }
                    presentIndexByUnitType[a.Key] += attackSize;
                }
            }
    }

    private void InitUnitsIndex()
    {
        for (int i = 0; i < unitGenerateAI.GeneratablUnits.Count && unitGenerateAI.GeneratablUnits[i]; i++)
            if (!presentIndexByUnitType.ContainsKey(unitGenerateAI.GeneratablUnits[i]))
                presentIndexByUnitType.Add(unitGenerateAI.GeneratablUnits[i], 0);

        foreach (var a in presentIndexByUnitType.Keys.ToList())
            presentIndexByUnitType[a] = 0;
        foreach (var a in unitGenerateAI.ReadOnlyGeneratedUnitListByType)
            if (a.Value != null)
                a.Value.Clear();
    }
    #endregion

    private void OnEnable()
    {
        unitGenerateAI.SetObjectMembersInstance();
        unitGenerateAI.AirDropUnitRatio = 0;
    }
}
