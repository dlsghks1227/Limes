using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class UnitLayout : LayoutBase
{
    private UnitGeneratorBase barrack;

    [SerializeField]
    private CanvasGroup unitTab = null;

    [SerializeField]
    private List<UnitData> unitdata = null;

    [SerializeField]
    private Button btn_melee = null;
    [SerializeField]
    private Button btn_rangeSplash = null;
    [SerializeField]
    private Button btn_range = null;

    [SerializeField]
    private Image melee = null;
    [SerializeField]
    private Image rangeSplash = null;
    [SerializeField]
    private Image range = null;

    [SerializeField]
    private Button btn_generate = null;
    [SerializeField]
    private Button btn_cancel = null;
    [SerializeField]
    private Button btn_exit = null;

    [SerializeField]
    private Text unit__name = null;
    [SerializeField]
    private Text unit_name = null;
    [SerializeField]
    private Text info = null;
    [SerializeField]
    private Text explain = null;

    [SerializeField]
    private Text gem = null;
    [SerializeField]
    private Text darkmatter = null;

    private UnitData selectedData;
    private InputObjectImage objectImage = null;

    public TechManager techManager;

    private PlayerInputManager manager;
    private PlanetHudLayout hud;

    private TimeBar timeBar;

    private void Start()
    {
        Initialize();
    }

    public void SetBarrack()
    {
        if (!GameObject.FindObjectOfType<UnitGeneratorBarrack>())
        {
            return;
        }
        else
        {
            barrack = GameObject.FindObjectOfType<UnitGeneratorBarrack>();
            timeBar = barrack.transform.GetChild(0).gameObject.GetComponent<TimeBar>();
        }
    }

    public void Initialize()
    {
        manager = FindObjectOfType<PlayerInputManager>();
        hud = FindObjectOfType<PlanetHudLayout>();
        objectImage = GameObject.Find("Pure Prefab").GetComponent<InputObjectImage>();

        btn_melee.onClick.AddListener(PickMelee);
        btn_rangeSplash.onClick.AddListener(PickRangeSplash);
        btn_range.onClick.AddListener(PickRange);
        btn_cancel.onClick.AddListener(Clean);
        btn_exit.onClick.AddListener(CloseUnitTab);
        btn_generate.onClick.AddListener(Generate);

    }

    private void PickSplash()
    {
        selectedData = unitdata[3];
        objectImage.SetSelectedUnit(selectedData.Prefab.name);
        ShowInfo(selectedData, selectedData.Prefab.GetComponent<UnitEntity>());
    }

    private void PickRange()
    {
        selectedData = unitdata[2];
        objectImage.SetSelectedUnit(selectedData.Prefab.name);
        ShowInfo(selectedData, selectedData.Prefab.GetComponent<UnitEntity>());
    }

    private void PickRangeSplash()
    {
        selectedData = unitdata[1];
        objectImage.SetSelectedUnit(selectedData.Prefab.name);
        ShowInfo(selectedData, selectedData.Prefab.GetComponent<UnitEntity>());
    }

    private void PickMelee()
    {
        selectedData = unitdata[0];
        objectImage.SetSelectedUnit(selectedData.name);
        ShowInfo(selectedData, selectedData.Prefab.GetComponent<UnitEntity>());
    }

    private void Generate()
    {
        if (timeBar)
        {
            if (timeBar.IsActive || !(PlayerDataManager.Instance.IsAffordableToPay(selectedData.IronCost) && PlayerDataManager.Instance.IsAffordableToPay(selectedData.GoldCost)))  
            {
                hud.ShowWarning("다른 유닛이 생산 중입니다.");
                return;
            }

            PlayerDataManager.Instance.SubstractPlayerResource(selectedData.IronCost);
            PlayerDataManager.Instance.SubstractPlayerResource(selectedData.GoldCost);
            barrack.GenerateUnit(selectedData, PlayerDataManager.Instance.GetPlayerResourceOf(EResource.RES_IRON), PlayerDataManager.Instance.GetPlayerResourceOf(EResource.RES_GOLD));
            timeBar.SetMaxTime(selectedData.generateTimes);
            unitTab.alpha = 0;
            unitTab.blocksRaycasts = false;
            Clean();
            manager.ClickUnitLayout();
        }
        else if (!timeBar)
        {
            return;
        }
       
    }

    public void ShowUnitTab()
    {
        unitTab.alpha = 1;
        unitTab.blocksRaycasts = true;
    }

    private void Clean()
    {
        GameObject.Destroy(objectImage.curUnitFocus); 
        selectedData = null;
        info.text = "";
        unit_name.text = "";
        unit__name.text = "";
        gem.text = "";
        darkmatter.text = "";
        explain.text = "";
    }

    public void CloseUnitTab()
    {
        Clean();
        unitTab.alpha = 0;
        unitTab.blocksRaycasts = false;
        info.text = "유닛 생산 정보";
        manager.ClickUnitLayout();
    }

    private void ShowInfo(UnitData data, UnitEntity unit)
    {
        info.text = "생산 시간 : " + data.generateTimes + "\n" +
                    "체력 : " + unit.HealthPointMax + "\n" +
                    "보호막 : " + unit.ArmorPointMax + "\n" +
                    "공격력 : " + data.Prefab.GetComponent<AttackComponent>().AttackDamage + "\n" +
                    "공격 속도 : " + data.Prefab.GetComponent<AttackComponent>().AttackSpeed;
        unit_name.text = data.name.ToString();
        unit__name.text = data.name.ToString();
        gem.text = data.GoldCost.resAmount.ToString();
        darkmatter.text = data.IronCost.resAmount.ToString();
        explain.text = unit.Explain;
    }
}
