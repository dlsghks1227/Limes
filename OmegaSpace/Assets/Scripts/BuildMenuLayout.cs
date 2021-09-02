using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BuildMenuLayout : LayoutBase
{
    private PlayerInputManager manager;

    private InputMainState mainState;

    private Grid grid;

    [SerializeField]
    private CanvasGroup buildTab = null;
    [SerializeField]
    private CanvasGroup militaryList = null;
    [SerializeField]
    private CanvasGroup minerList = null;
    [SerializeField]
    private CanvasGroup buildingList = null;
    [SerializeField]
    private CanvasGroup totalList = null;

    [SerializeField]
    private Button btn_total = null;
    [SerializeField]
    private Button btn_military = null;
    [SerializeField]
    private Button btn_mine = null;
    [SerializeField]
    private Button btn_building = null;

    [SerializeField]
    private Button btn_command = null;
    [SerializeField]
    private Button btn_barrack = null;
    [SerializeField]
    private Button btn_barricade = null;
    [SerializeField]
    private Button btn_meleesplash = null;
    [SerializeField]
    private Button btn_range = null;
    [SerializeField]
    private Button btn_rangesplash = null;
    [SerializeField]
    private Button btn_protector = null;

    [SerializeField]
    private Button btn_minerA = null;
    [SerializeField]
    private Button btn_minerB = null;

    [SerializeField]
    private Button btn_storage = null;
    [SerializeField]
    private Button btn_exchange = null;

    [SerializeField]
    private Button btn_command2 = null;
    [SerializeField]
    private Button btn_barrack2 = null;
    [SerializeField]
    private Button btn_barricade2 = null;
    [SerializeField]
    private Button btn_meleesplash2 = null;
    [SerializeField]
    private Button btn_range2 = null;
    [SerializeField]
    private Button btn_rangesplash2 = null;
    [SerializeField]
    private Button btn_protector2 = null;

    [SerializeField]
    private Button btn_miner2A = null;
    [SerializeField]
    private Button btn_miner2B = null;

    [SerializeField]
    private Button btn_storage2 = null;
    [SerializeField]
    private Button btn_exchange2 = null;

    [SerializeField]
    private Button btn_cancel = null;
    [SerializeField]
    private Button btn_build = null;
    
    [SerializeField]
    private Button btn_exit = null;

    [SerializeField]
    private Text info = null;
    [SerializeField]
    private Text category = null;
    [SerializeField]
    private Text buildingName = null;
    [SerializeField]
    private Text building_name = null;
    [SerializeField]
    private Text gold = null;
    [SerializeField]
    private Text iron = null;
    [SerializeField]
    private Text explain = null;

    private GameObject structure;
    private BuildingData selectedData;
    private GameObject ghost;

    private Vector2Int structurePos;

    private bool isGhost = false;

    [SerializeField]
    private List<BuildingData> militaryData = null;

    [SerializeField]
    private BuildingData minerA = null;
    [SerializeField]
    private BuildingData minerB = null;

    [SerializeField]
    private List<BuildingData> structData = null;

    private InputObjectImage objectImage = null;

    public void Initialize(InputMainState main, PlayerInputManager inputManager, Grid planetGrid)
    {
        manager = inputManager;
        mainState = main;
        grid = planetGrid;

        objectImage = GameObject.Find("Pure Prefab").GetComponent<InputObjectImage>();

        btn_total.onClick.AddListener(ShowTotalList);
        btn_military.onClick.AddListener(ShowMilitaryList);
        btn_mine.onClick.AddListener(ShowMiner);
        btn_building.onClick.AddListener(ShowBuildingList);

        btn_command.onClick.AddListener(PickCommand);
        btn_barrack.onClick.AddListener(PickBarrack);
        btn_barricade.onClick.AddListener(PickBarricade);
        btn_meleesplash.onClick.AddListener(PickMelee);
        btn_range.onClick.AddListener(PickRange);
        btn_rangesplash.onClick.AddListener(PickRangeSplash);
        btn_protector.onClick.AddListener(PickProtector);

        btn_minerA.onClick.AddListener(PickMinerA);
        btn_minerB.onClick.AddListener(PickMinerB);

        btn_storage.onClick.AddListener(PickStorage);
        btn_exchange.onClick.AddListener(PickExchange);

        btn_command2.onClick.AddListener(PickCommand);
        btn_barrack2.onClick.AddListener(PickBarrack);
        btn_barricade2.onClick.AddListener(PickBarricade);
        btn_meleesplash2.onClick.AddListener(PickMelee);
        btn_range2.onClick.AddListener(PickRange);
        btn_rangesplash2.onClick.AddListener(PickRangeSplash);
        btn_protector2.onClick.AddListener(PickProtector);

        btn_miner2A.onClick.AddListener(PickMinerA);
        btn_miner2B.onClick.AddListener(PickMinerB);

        btn_storage2.onClick.AddListener(PickStorage);
        btn_exchange2.onClick.AddListener(PickExchange);

        btn_cancel.onClick.AddListener(Clean);
        btn_build.onClick.AddListener(Build);
        btn_exit.onClick.AddListener(Exit);

        militaryList.alpha = 0;
        minerList.alpha = 0;
        buildingList.alpha = 0;
        totalList.alpha = 1;
        militaryList.blocksRaycasts = false;
        minerList.blocksRaycasts = false;
        buildingList.blocksRaycasts = false;
        totalList.blocksRaycasts = true;

    }

    private void Update()
    {
        if (isGhost)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            int layermask = 1 << 13;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
            {
                ghost.transform.position = hit.point;

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    var item = ghost.GetComponent<BuildingEntity>();
                    if (item)
                    {
                        if (item.GetBuildingType().Equals(EBuilding.BARRICADE))
                            item.Rotate(90.0f);
                    }
                }

                if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    Ray ray2 = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                    RaycastHit hit2;
                    structurePos = grid.GetWorldToGrid(new Vector2(hit.point.x, hit.point.z));

                    if (!selectedData.BuildingPrefab.GetComponent<Miner>() && grid.GetTerritoryTileState(structurePos.x, structurePos.y).Equals(ETerritory.OUR) ||
                       (Physics.Raycast(ray2, out hit2, Mathf.Infinity) && hit2.collider.gameObject.GetComponent<GoldOre>() && selectedData.BuildingPrefab.GetComponent<MinerA>()) && !hit2.collider.gameObject.GetComponent<GoldOre>().IsMine ||
                       (Physics.Raycast(ray2, out hit2, Mathf.Infinity) && hit2.collider.gameObject.GetComponent<IronOre>() && selectedData.BuildingPrefab.GetComponent<MinerB>()) && !hit2.collider.gameObject.GetComponent<IronOre>().IsMine) 
                    {
                        selectedData.BuildingPrefab.transform.rotation = ghost.transform.rotation;
                        BuildingConstructorManager.Instance.Build(selectedData, structurePos, grid);
                      
                        if (ghost.GetComponent<BarrierDeployer>()) { ghost.GetComponent<BarrierDeployer>().SetCollider(); }
                        Clean ();

                        DestroyImmediate(ghost, true);
                        manager.ChangeState(mainState);
                    }
                }
            }
        }
    }
    private void ShowTotalList()
    {
        totalList.alpha = 1;
        militaryList.alpha = 0;
        minerList.alpha = 0;
        buildingList.alpha = 0;
        militaryList.blocksRaycasts = false;
        minerList.blocksRaycasts = false;
        buildingList.blocksRaycasts = false;
        totalList.blocksRaycasts = true;
        category.text = "전체";
    }

    private void ShowMilitaryList()
    {
        totalList.alpha = 0;
        militaryList.alpha = 1;
        minerList.alpha = 0;
        buildingList.alpha = 0;
        militaryList.blocksRaycasts = true;
        minerList.blocksRaycasts = false;
        buildingList.blocksRaycasts = false;
        totalList.blocksRaycasts = false;
        category.text = "군사";

    }
    private void ShowMiner()
    {
        totalList.alpha = 0;
        militaryList.alpha = 0;
        minerList.alpha = 1;
        buildingList.alpha = 0;
        militaryList.blocksRaycasts = false;
        minerList.blocksRaycasts = true;
        buildingList.blocksRaycasts = false;
        totalList.blocksRaycasts = false;
        category.text = "채굴";

    }
    private void ShowBuildingList()
    {
        totalList.alpha = 0;
        militaryList.alpha = 0;
        minerList.alpha = 0;
        buildingList.alpha = 1;
        militaryList.blocksRaycasts = false;
        minerList.blocksRaycasts = false;
        buildingList.blocksRaycasts = true;
        totalList.blocksRaycasts = false;
        category.text = "건물";

    }

    private void PickCommand()
    {
        IsEmpty();
        Debug.Log("command");
        structure = militaryData[6].BuildingPrefab;
        structure.GetComponent<BuildingEntity>().Initialize(militaryData[6].GridScale);
        objectImage.SetSelectedBuild(structure);
        selectedData = militaryData[6];
        ShowInfo(selectedData, structure.GetComponent<BuildingEntity>());
    }

    private void PickBarrack()
    {
        IsEmpty();
        Debug.Log("barrack");
        structure = militaryData[0].BuildingPrefab;
        structure.GetComponent<BuildingEntity>().Initialize(militaryData[0].GridScale);
        objectImage.SetSelectedBuild(structure);
        selectedData = militaryData[0];
        ShowInfo(selectedData, structure.GetComponent<BuildingEntity>());
    }

    private void PickBarricade()
    {
        IsEmpty();
        Debug.Log("barricade");
        structure = militaryData[2].BuildingPrefab;
        structure.GetComponent<BuildingEntity>().Initialize(militaryData[2].GridScale);
        objectImage.SetSelectedBuild(structure);
        selectedData = militaryData[2];
        ShowInfo(selectedData, structure.GetComponent<BuildingEntity>());
    }
    private void PickRangeSplash()
    {
        IsEmpty();
        structure = militaryData[3].BuildingPrefab;
        structure.GetComponent<BuildingEntity>().Initialize(militaryData[3].GridScale);
        objectImage.SetSelectedBuild(structure);
        selectedData = militaryData[3];
        ShowInfo(selectedData, structure.GetComponent<BuildingEntity>());
    }
    private void PickRange()
    {
        IsEmpty();
        structure = militaryData[4].BuildingPrefab;
        structure.GetComponent<BuildingEntity>().Initialize(militaryData[4].GridScale);
        objectImage.SetSelectedBuild(structure);
        selectedData = militaryData[4];
        ShowInfo(selectedData, structure.GetComponent<BuildingEntity>());
    }
    private void PickMelee()
    {
        IsEmpty();
        structure = militaryData[5].BuildingPrefab;
        structure.GetComponent<BuildingEntity>().Initialize(militaryData[5].GridScale);
        objectImage.SetSelectedBuild(structure);
        selectedData = militaryData[5];
        ShowInfo(selectedData, structure.GetComponent<BuildingEntity>());
    }
    private void PickProtector()
    {
        IsEmpty();
        structure = militaryData[1].BuildingPrefab;
        structure.GetComponent<BuildingEntity>().Initialize(militaryData[1].GridScale);
        objectImage.SetSelectedBuild(structure);
        selectedData = militaryData[1];
        ShowInfo(selectedData, structure.GetComponent<BuildingEntity>());
    }

    private void PickMinerA()
    {
        IsEmpty();
        Debug.Log("miner");
        structure = minerA.BuildingPrefab;
        structure.GetComponent<BuildingEntity>().Initialize(minerA.GridScale);
        objectImage.SetSelectedBuild(structure);
        selectedData = minerA;
        ShowInfo(minerA, structure.GetComponent<BuildingEntity>());
    }

    private void PickMinerB()
    {
        IsEmpty();
        Debug.Log("miner");
        structure = minerB.BuildingPrefab;
        structure.GetComponent<BuildingEntity>().Initialize(minerB.GridScale);
        objectImage.SetSelectedBuild(structure);
        selectedData = minerB;
        ShowInfo(minerB, structure.GetComponent<BuildingEntity>());
    }

    private void PickStorage()
    {
        IsEmpty();
        Debug.Log("storage");
        structure = structData[0].BuildingPrefab;
        structure.GetComponent<BuildingEntity>().Initialize(structData[0].GridScale);
        objectImage.SetSelectedBuild(structure);
        selectedData = structData[0];
        ShowInfo(selectedData, structure.GetComponent<BuildingEntity>());
    }

    private void PickExchange()
    {
        IsEmpty();
        Debug.Log("exchange");
        structure = structData[1].BuildingPrefab;
        structure.GetComponent<BuildingEntity>().Initialize(structData[1].GridScale);
        objectImage.SetSelectedBuild(structure);
        selectedData = structData[1];
        ShowInfo(selectedData, structure.GetComponent<BuildingEntity>());
    }

    private void Build()
    {
        if (structure == null)
        {
            return;
        }

        buildTab.alpha = 0;
        buildTab.blocksRaycasts = false;
        ghost = Instantiate(structure);

        if (ghost == null)
        {
            return;
        }

        ghost.GetComponent<Collider>().enabled = false;

        if (ghost.GetComponent<BarrierDeployer>()) { ghost.GetComponent<BarrierDeployer>().SetCollider(); }
        isGhost = true;
    }

    private void Exit()
    {
        if (!IsEmpty())
        {
            DestroyImmediate(structure, true);
            Clean();
        }
        buildTab.alpha = 0;
        buildTab.blocksRaycasts = false;
        totalList.alpha = 0;
        militaryList.alpha = 0;
        minerList.alpha = 0;
        buildingList.alpha = 0;
        militaryList.blocksRaycasts = false;
        minerList.blocksRaycasts = false;
        buildingList.blocksRaycasts = false;
        totalList.blocksRaycasts = false;
        selectedData = new BuildingData();
        manager.ChangeState(mainState);
    }

    private void ShowInfo(BuildingData data, BuildingEntity building)
    {
        info.text = "건설 시간 : " + data.BuildTime + "\n" +
                    "레벨 : " + building.Level.ToString() + "\n" +
                    "체력 : " + building.HealthPoint + " / " + building.HealthPointMax + "\n" +
                    "보호막 : " + building.ArmorPoint + " / " + building.ArmorPointMax + "\n" + 
                    "영역 : " + building.Territory.ToString();
        building_name.text = data.Name;
        buildingName.text = data.Name;
        gold.text = data.GoldCost.resAmount.ToString();
        iron.text = data.IronCost.resAmount.ToString();
        explain.text = data.Explain;
    }

    private bool IsEmpty()
    {
        if (!structure)
        {
            structure = new GameObject();
            return true;
        }
        if (structure.GetComponent<BuildingEntity>() == null)
        {
            return true;
        }
        else 
        {
            structure = new GameObject();
            return false;
        }
    }

    private void Clean()
    {
        if (!IsEmpty())
        {
            DestroyImmediate(structure, true);
        }

        isGhost = false;

        selectedData = new BuildingData();
        info.text = "";

        GameObject.Destroy(objectImage.curbuildFocus);
        category.text = "Category";
        buildingName.text = "이름";
        building_name.text = "이름";
        gold.text = "";
        iron.text = "";
        explain.text = "";

    }
}