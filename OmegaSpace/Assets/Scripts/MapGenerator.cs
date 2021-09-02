using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    enum EResourcePattern
    {
        LINEAR,
        SIN,
        COS,
        TAN,
    }

    private class BuildingSaveData
    {
        public Vector2      position = new Vector2();
        public EBuilding    buildingType = EBuilding.NONE;

        public BuildingSaveData(Vector2 pos, EBuilding type)
        {
            position = pos;
            buildingType = type;
        }
    }
    private class OreSaveData
    {
        public Vector2Int   position        = new Vector2Int();

        public EResource    resourceType    = EResource.RES_NONE;
        public int          amount          = 0;

        public OreSaveData(Vector2Int pos, EResource type, int amt)
        {
            position = pos;
            resourceType = type;
            amount = amt;
        }
    }

    private class MapSaveData
    {
        public Vector2Int   gridSize = new Vector2Int(0, 0);
        public int          id = -1;

        public int          totalGoldCount  = 0;
        public int          totalIronCount  = 0;

        public int          totalGoldAmount = 0;
        public int          totalIronAmount = 0;


        public List<BuildingSaveData>       playerBuildDatas    = new List<BuildingSaveData>();
        public List<BuildingSaveData>       enemyBuildDatas     = new List<BuildingSaveData>();
        public List<OreSaveData>            oreDatas            = new List<OreSaveData>();

        public MapSaveData(int id, Vector2Int size)
        {
            this.id = id;
            gridSize = size;
        }
    }

    [Header("Game objects")]
    [SerializeField] private GameObject     command     = null;
    [SerializeField] private GameObject     barrack     = null;
    [SerializeField] private GameObject     storage     = null;
    [SerializeField] private GameObject     goldOre     = null;
    [SerializeField] private GameObject     ironOre     = null;
    [SerializeField] private GameObject     airBarrack  = null;
    [SerializeField] private GameObject     planetShop  = null;

    [Header("Civ objects")]
    [SerializeField] private CivGenerator   civGenerator    = null;
    [SerializeField] private GameObject     dummy           = null;

    [Header("Game object")]
    [SerializeField] private Grid           grid            = null;
    [SerializeField] private RaidPlayerAI   raidPlayerAI    = null;

    [Header("Object pool")]
    [SerializeField] private ObjectPool     objectPool      = null;

    private Dictionary<int, MapSaveData>    mapInfomations  = new Dictionary<int, MapSaveData>();

    public int      currentID { get; private set; } = -1;
    private bool    isLoading = false;

    private void Start()
    {
        objectPool.AddObject(command, "command", 10, true);
        objectPool.AddObject(barrack, "barrack", 10, true);
        objectPool.AddObject(storage, "storage", 10, true);
        objectPool.AddObject(dummy,   "dummy",   40, true);
        objectPool.AddObject(goldOre, "goldOre", 40, true);
        objectPool.AddObject(ironOre, "ironOre", 40, true);
        objectPool.AddObject(airBarrack, "airBarrack", 40, true);
        objectPool.AddObject(planetShop, "planetShop", 10, true);
    }

    public void OnLoad(Planet loadPlanet)
    { 
        if (isLoading == true)
        {
            return;
        }

        isLoading = true;
        currentID = loadPlanet.objectID;
        Vector2Int gridSize = new Vector2Int(loadPlanet.landSize, loadPlanet.landSize);

        grid.Initialize(gridSize);

        if (mapInfomations.ContainsKey(loadPlanet.objectID) == false)
        {
            mapInfomations.Add(loadPlanet.objectID, new MapSaveData(loadPlanet.objectID, gridSize));

            // 초기 자원 생성
            CreateResources(loadPlanet.objectID, loadPlanet.gold.resAmount, loadPlanet.gold.resAmount);

            // 행성 내 문명 생성
            StartCivSetting(loadPlanet.objectID, loadPlanet.numCivilization);

            // 플레이어 커맨드 생성
            StartAreaSetting(loadPlanet.objectID, new Vector2(0.0f, 0.0f), 10.0f, true);

            // 자원 재분배
            ResetResources(loadPlanet.objectID);

            OnSave();

            if (raidPlayerAI.gameObject.activeSelf)
                StartCoroutine(raidPlayerAI.Play());

            return;
        }

        foreach (var item in mapInfomations[loadPlanet.objectID].oreDatas)
        {
            if (item.resourceType == EResource.RES_GOLD)
            {
                GoldOre goldOre = objectPool.GetPooledObject("goldOre").GetComponent<GoldOre>();
                goldOre.gameObject.SetActive(true);
                goldOre.Amount = item.amount;
                grid.LocatedOre(goldOre, item.position.x, item.position.y);
            }
            else if (item.resourceType == EResource.RES_IRON)
            {
                IronOre ironOre = objectPool.GetPooledObject("ironOre").GetComponent<IronOre>();
                ironOre.gameObject.SetActive(true);
                ironOre.Amount = item.amount;
                grid.LocatedOre(ironOre, item.position.x, item.position.y);
            }
        }

        foreach (var item in mapInfomations[loadPlanet.objectID].playerBuildDatas)
        {
            switch (item.buildingType)
            {
                case EBuilding.COMMAND:
                    {
                        BuildingEntity build = objectPool.GetPooledObject("command").GetComponent<BuildingEntity>();

                        build.gameObject.SetActive(true);
                        build.gameObject.layer = LayerMask.NameToLayer("Player");

                        grid.Locate(build, item.position, EBuilding.COMMAND, 10, true);

                        build.InitStats();

                        TechManager.Instance.AddPresentBuilding(build.gameObject);
                    }
                    break;
                case EBuilding.BARRACK:
                    {
                        BuildingEntity build = objectPool.GetPooledObject("barrack").GetComponent<BuildingEntity>();

                        build.gameObject.SetActive(true);
                        build.gameObject.layer = LayerMask.NameToLayer("Player");

                        build.InitStats();

                        grid.Locate(build, item.position, EBuilding.BARRACK, 5, true);
                    }
                    break;
                case EBuilding.STORAGE:
                    {
                        BuildingEntity build = objectPool.GetPooledObject("storage").GetComponent<BuildingEntity>();

                        build.gameObject.SetActive(true);
                        build.gameObject.layer = LayerMask.NameToLayer("Player");

                        build.InitStats();

                        grid.Locate(build, item.position, EBuilding.STORAGE, 5, true);
                    }
                    break;
                case EBuilding.PLANETSTORE:
                    {
                        BuildingEntity build = objectPool.GetPooledObject("planetShop").GetComponent<BuildingEntity>();

                        build.gameObject.SetActive(true);
                        build.InitStats();

                        grid.Locate(build, item.position, EBuilding.PLANETSTORE);
                    }
                    break;
                case EBuilding.AIRBARRACK:
                    {
                        BuildingEntity build = objectPool.GetPooledObject("airBarrack").GetComponent<BuildingEntity>();

                        build.gameObject.SetActive(true);
                        build.InitStats();

                        grid.Locate(build, item.position, EBuilding.AIRBARRACK);
                    }
                    break;
                default:
                    {
                        BuildingEntity build = objectPool.GetPooledObject("dummy").GetComponent<BuildingEntity>();

                        build.gameObject.SetActive(true);
                        build.gameObject.layer = LayerMask.NameToLayer("Player");

                        build.InitStats();

                        grid.Locate(build, item.position, EBuilding.BUILDING, 3, true);
                    }
                    break;
            }
        }

        foreach (var item in mapInfomations[loadPlanet.objectID].enemyBuildDatas)
        {
            switch (item.buildingType)
            {
                case EBuilding.COMMAND:
                    {
                        BuildingEntity build = objectPool.GetPooledObject("command").GetComponent<BuildingEntity>();

                        build.gameObject.SetActive(true);
                        build.gameObject.layer = LayerMask.NameToLayer("Enemy");

                        build.InitStats();

                        grid.Locate(build, item.position, EBuilding.COMMAND, 10, false);
                    }
                    break;
                case EBuilding.BARRACK:
                    {
                        BuildingEntity build = objectPool.GetPooledObject("barrack").GetComponent<BuildingEntity>();

                        build.gameObject.SetActive(true);
                        build.gameObject.layer = LayerMask.NameToLayer("Enemy");

                        grid.Locate(build, item.position, EBuilding.BARRACK, 5, false);

                        build.InitStats();

                        raidPlayerAI.unitGenerateAI.unitGeneratorsBarrack.Add(build.gameObject.GetComponent<UnitGeneratorBarrack>());
                    }
                    break;
                case EBuilding.STORAGE:
                    {
                        BuildingEntity build = objectPool.GetPooledObject("storage").GetComponent<BuildingEntity>();

                        build.gameObject.SetActive(true);
                        build.gameObject.layer = LayerMask.NameToLayer("Enemy");

                        build.InitStats();

                        grid.Locate(build, item.position, EBuilding.STORAGE, 5, false);
                    }
                    break;
                case EBuilding.AIRBARRACK:
                    {
                        BuildingEntity build = objectPool.GetPooledObject("airBarrack").GetComponent<BuildingEntity>();

                        build.gameObject.SetActive(true);
                        build.gameObject.layer = LayerMask.NameToLayer("Enemy");

                        build.InitStats();

                        grid.Locate(build, item.position, EBuilding.AIRBARRACK, 5, false);
                    }
                    break;
                default:
                    {
                        BuildingEntity build = objectPool.GetPooledObject("dummy").GetComponent<BuildingEntity>();

                        build.gameObject.SetActive(true);
                        build.gameObject.layer = LayerMask.NameToLayer("Enemy");

                        build.InitStats();

                        grid.Locate(build, item.position, EBuilding.BUILDING, 3, false);
                    }
                    break;
            }
        }

       if (raidPlayerAI.gameObject.activeSelf)
            StartCoroutine(raidPlayerAI.Play());
    }

    public void OnSave()
    {
        if (mapInfomations.ContainsKey(currentID) == false)
        {
            return;
        }

        mapInfomations[currentID].playerBuildDatas.Clear();
        mapInfomations[currentID].enemyBuildDatas.Clear();
        mapInfomations[currentID].oreDatas.Clear();

        mapInfomations[currentID].totalGoldAmount = 0;
        mapInfomations[currentID].totalIronAmount = 0;

        foreach (var item in grid.currentPlayerBuildings)
        {
            if (item != null)
            {
                if (item.gameObject.activeSelf == true)
                {
                    mapInfomations[currentID].playerBuildDatas.Add(new BuildingSaveData(
                        new Vector2(
                            item.Center.x,
                            item.Center.y),
                        item.GetBuildingType()));
                }
            }
        }

        foreach (var item in grid.currentEnemyBuildings)
        {
            if (item != null)
            {
                if (item.gameObject.activeSelf == true)
                {
                    mapInfomations[currentID].enemyBuildDatas.Add(new BuildingSaveData(
                        new Vector2(
                            item.Center.x,
                            item.Center.y),
                        item.GetBuildingType()));
                }
            }
        }

        foreach (var item in grid.currentOres)
        {
            if (item != null)
            {
                if (item.gameObject.activeSelf == true)
                {
                    if (item.GetResourceType() == EResource.RES_GOLD)
                    {
                        mapInfomations[currentID].totalGoldAmount += item.Amount;
                    }
                    else if (item.GetResourceType() == EResource.RES_IRON)
                    {
                        mapInfomations[currentID].totalIronAmount += item.Amount;
                    }
                    mapInfomations[currentID].oreDatas.Add(new OreSaveData(grid.GetWorldToGrid(item.Pivot), item.GetResourceType(), item.Amount));
                }
            }
        }
    }

    public void OnExit()
    {
        foreach (var item in grid.currentPlayerBuildings)
        {
            grid.Unbuild(item, true);
        }

        foreach (var item in grid.currentEnemyBuildings)
        {
            grid.Unbuild(item, false);
        }

        foreach (var item in grid.currentOres)
        {
            grid.UnbuildOre(item);
        }

        currentID = -1;

        grid.InitializeCurrentEntity();

        // TechManager 초기화
        TechManager.Instance.InitAllBuildingList();
        TechManager.Instance.DeactiveAllUnit();
        raidPlayerAI.unitGenerateAI.unitGeneratorsBarrack.Clear();

        isLoading = false;
    }

    private void StartCivSetting(int id, int numCiv)
    {
        float avg = ((float)(mapInfomations[id].gridSize.x + mapInfomations[id].gridSize.y) * 0.5f);
        float randStartPosition = 360.0f * Random.value;
        float increase = (360.0f / (float)(numCiv + 1));
        for (int i = 0; i < (numCiv + 1); i++)
        {
            float radius = (avg * 0.25f) + Random.Range(0.0f, (avg * 0.2f));
            CivGenerateInfo civInfo = new CivGenerateInfo(Random.Range(0, 8), (ECivShape)Random.Range(0, 4), 0);
            Vector2 civPos = new Vector2(
                Mathf.Cos(((i * increase) + randStartPosition) * Mathf.Deg2Rad) * radius,
                Mathf.Sin(((i * increase) + randStartPosition) * Mathf.Deg2Rad) * radius);

            if (i >= numCiv)
            {
                BuildingEntity entity = objectPool.GetPooledObject("planetShop").GetComponent<BuildingEntity>();

                entity.gameObject.SetActive(true);
                grid.Locate(entity, civPos, EBuilding.PLANETSTORE);
                entity.InitStats();

                StartAreaSetting(id, civPos, 10.0f, false);
                return;
            }
            civGenerator.BuildingGenerate(grid.GetWorldToGrid(civPos), mapInfomations[id].gridSize, civGenerator.Preset(civInfo), 0);

            StartAreaSetting(id, civPos, 10.0f, false);
        }

        //civGenerator.BarricadeGenerator();
    }

    private void StartAreaSetting(int id, Vector2 center, float radius, bool isPlayer)
    {
        for (int i = 0; i < grid.currentOres.Count; i++)
        {
            Vector2 delta = new Vector2(grid.currentOres[i].gameObject.transform.position.x, grid.currentOres[i].gameObject.transform.position.z) - center;
            float length = Mathf.Sqrt((delta.x * delta.x) + (delta.y * delta.y));
            if (length < radius)
            {
                if (grid.currentOres[i].gameObject.activeSelf == true)
                {
                    if (grid.currentOres[i].GetResourceType().Equals(EResource.RES_GOLD))
                    {
                        mapInfomations[id].totalGoldCount -= 1;
                    }
                    else if (grid.currentOres[i].GetResourceType().Equals(EResource.RES_IRON))
                    {
                        mapInfomations[id].totalIronCount -= 1;
                    }
                    else
                    {
                        mapInfomations[id].totalGoldCount -= 1;
                    }
                }

                grid.SetBuildingTilesByRange(grid.currentOres[i], EBuilding.NONE);
                grid.currentOres[i].gameObject.SetActive(false);
            }
        }

        if (isPlayer == true)
        {
            BuildingEntity entity = objectPool.GetPooledObject("command").GetComponent<BuildingEntity>();

            entity.gameObject.SetActive(true);
            entity.gameObject.layer = LayerMask.NameToLayer("Player");

            grid.Locate(entity, mapInfomations[id].gridSize.x / 2, mapInfomations[id].gridSize.y / 2, EBuilding.COMMAND, 10, true);
            entity.InitStats();

            TechManager.Instance.AddPresentBuilding(entity.gameObject);
        }
    }

    private void ResetResources(int id)
    {
        foreach (var item in grid.currentOres)
        {
            if (item.GetResourceType().Equals(EResource.RES_GOLD))
            {
                item.Amount = mapInfomations[id].totalGoldAmount / mapInfomations[id].totalGoldCount;
            }
            else if (item.GetResourceType().Equals(EResource.RES_IRON))
            {
                item.Amount = mapInfomations[id].totalIronAmount / mapInfomations[id].totalIronCount;
            }
            else
            {
                item.Amount = mapInfomations[id].totalGoldAmount / mapInfomations[id].totalGoldCount;
            }
        }
    }

    private void CreateResources(int id, int goldAmount, int ironAmount)
    {
        mapInfomations[id].totalGoldAmount = goldAmount * 5000;
        mapInfomations[id].totalIronAmount = ironAmount * 5000;

        for (int i = 1; i < ((mapInfomations[id].gridSize.x / 20) + 1); i++)
        {
            for (int j = 1; j < ((mapInfomations[id].gridSize.y / 20) + 1); j++)
            {
                if (Random.Range(0, 2) == 0)
                {
                    if (goldAmount > 0)
                    {
                        CreateResourcePattern(id, EResource.RES_GOLD,
                        new Vector2(
                            ((20.0f * i) - (10.0f * Random.value)) + Random.Range(-5.0f, 5.0f),
                            ((20.0f * j) - (10.0f * Random.value)) + Random.Range(-5.0f, 5.0f)), (EResourcePattern)Random.Range(0, 4));
                    }
                }
                else
                {
                    if (ironAmount > 0)
                    {
                        CreateResourcePattern(id, EResource.RES_IRON,
                        new Vector2(
                            ((20.0f * i) - (10.0f * Random.value)) + Random.Range(-5.0f, 5.0f),
                            ((20.0f * j) - (10.0f * Random.value)) + Random.Range(-5.0f, 5.0f)), (EResourcePattern)Random.Range(0, 4));
                    }
                }
            }
        }
    }

    private void CreateResource(int id, EResource resType, Vector2Int position)
    {
        switch (resType)
        {
            case EResource.RES_GOLD:
                {
                    GoldOre entity = objectPool.GetPooledObject("goldOre").GetComponent<GoldOre>();
                    entity.gameObject.SetActive(true);
                    grid.LocatedOre(entity, position.x, position.y);

                    mapInfomations[id].totalGoldCount += 1;
                }
                break;
            case EResource.RES_IRON:
                {
                    IronOre entity = objectPool.GetPooledObject("ironOre").GetComponent<IronOre>();
                    entity.gameObject.SetActive(true);
                    grid.LocatedOre(entity, position.x, position.y);

                    mapInfomations[id].totalIronCount += 1;
                }
                break;
            default:
                {
                    GoldOre entity = objectPool.GetPooledObject("goldOre").GetComponent<GoldOre>();
                    entity.gameObject.SetActive(true);
                    grid.LocatedOre(entity, position.x, position.y);

                    mapInfomations[id].totalGoldCount += 1;
                }
                break;
        }
    }

    private void CreateResourcePattern(int id, EResource resType, Vector2 position, EResourcePattern ptnType)
    {
        float angle = 360.0f * Random.value;

        switch (ptnType)
        {
            case EResourcePattern.LINEAR:
                {
                    float dispersion = Random.Range(0.6f, 0.8f);
                    float size = Random.Range(3.0f, 6.0f);
                    for (float i = 0.0f; i < 1.0f; i += (1.1f - dispersion))
                    {
                        Vector2Int line = Rotate(new Vector2(
                            (i * size) + Mathf.RoundToInt(position.x - (size / 2.0f)),
                            (i * size) + Mathf.RoundToInt(position.y - (size / 2.0f))), angle, position);
                        if (line.x < 0 || line.x >= mapInfomations[id].gridSize.x ||
                            line.y < 0 || line.y >= mapInfomations[id].gridSize.y)
                        {
                            continue;
                        }

                        if (grid.GetBuildingTileState(line.x, line.y) != EBuilding.NONE)
                        {
                            continue;
                        }

                        CreateResource(id, resType, line);
                    }
                    break;
                }
            case EResourcePattern.SIN:
                {
                    float dispersion = Random.Range(0.8f, 1.0f);
                    float size = Random.Range(5.0f, 8.0f);
                    for (float i = 0.0f; i < 1.0f; i += (1.1f - dispersion))
                    {
                        Vector2Int line = Rotate(new Vector2(
                            (i * size) + Mathf.RoundToInt(position.x - (Mathf.PI / 2.0f)),
                            (Mathf.Sin((i * 360.0f) * Mathf.Deg2Rad) * 3.0f) + Mathf.RoundToInt(position.y - 1.5f)), angle, position);
                        if (line.x < 0 || line.x >= mapInfomations[id].gridSize.x ||
                            line.y < 0 || line.y >= mapInfomations[id].gridSize.y)
                        {
                            continue;
                        }

                        if (grid.GetBuildingTileState(line.x, line.y) != EBuilding.NONE)
                        {
                            continue;
                        }

                        CreateResource(id, resType, line);
                    }
                }
                break;
            case EResourcePattern.COS:
                {
                    float dispersion = Random.Range(0.8f, 1.0f);
                    float size = Random.Range(4.0f, 8.0f);
                    for (float i = 0.0f; i < 1.0f; i += (1.1f - dispersion))
                    {
                        Vector2Int line = Rotate(new Vector2(
                            (i * size) + Mathf.RoundToInt(position.x - (Mathf.PI / 2.0f)),
                            (Mathf.Cos((i * 360.0f) * Mathf.Deg2Rad) * 3.0f) + Mathf.RoundToInt(position.y - 1.5f)), angle, position);
                        if (line.x < 0 || line.x >= mapInfomations[id].gridSize.x ||
                            line.y < 0 || line.y >= mapInfomations[id].gridSize.y)
                        {
                            continue;
                        }

                        if (grid.GetBuildingTileState(line.x, line.y) != EBuilding.NONE)
                        {
                            continue;
                        }

                        CreateResource(id, resType, line);
                    }
                    break;
                }
            case EResourcePattern.TAN:
                {
                    float dispersion = 1.0f;
                    float size = Random.Range(6.0f, 8.0f);
                    for (float i = 0.0f; i < 1.0f; i += (1.1f - dispersion))
                    {
                        Vector2Int line = Rotate(new Vector2(
                            (i * size) + Mathf.RoundToInt(position.x - (Mathf.PI / 2.0f)),
                            (Mathf.Tan((i * 270.0f) * Mathf.Deg2Rad) * 3.0f) + Mathf.RoundToInt(position.y - 1.5f)), angle, position);
                        if (line.x < 0 || line.x >= mapInfomations[id].gridSize.x ||
                            line.y < 0 || line.y >= mapInfomations[id].gridSize.y)
                        {
                            continue;
                        }

                        if (grid.GetBuildingTileState(line.x, line.y) != EBuilding.NONE)
                        {
                            continue;
                        }

                        CreateResource(id, resType, line);
                    }
                }
                break;
        }
    }

    private Vector2Int Rotate(Vector2 line, float angle, Vector2 center)
    {
        float tempX = line.x - center.x;
        float tempY = line.y - center.y;
        float radian = Mathf.Deg2Rad * angle;

        float rotatedX = tempX * Mathf.Cos(radian) - tempY * Mathf.Sin(radian);
        float rotatedY = tempX * Mathf.Sin(radian) + tempY * Mathf.Cos(radian);

        return new Vector2Int(Mathf.FloorToInt(rotatedX + center.x), Mathf.FloorToInt(rotatedY + center.y));
    }
}