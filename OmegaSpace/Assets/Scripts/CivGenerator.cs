using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivGenerator : MonoBehaviour
{
    private MiniMapManager miniMapManager;
    private CivPresetMaker maker;

    private CivGenerateInfo info1;
    private CivGenerateInfo info2;

    [SerializeField]
    private Grid grid = null;
    [SerializeField]
    private RaidPlayerAI raidPlayerAI = null;

    [SerializeField]
    private BarricadeGenerator barricadeGenerator = null;

    [SerializeField]
    private GameObject dummy = null;
    [SerializeField]
    private GameObject command = null;
    [SerializeField]
    private GameObject storage = null;
    [SerializeField]
    private GameObject barrack = null;
    [SerializeField]
    private GameObject airBarrack = null;

    private void Start()
    {
        maker = new CivPresetMaker();
        miniMapManager = new MiniMapManager();
        //info1 = new CivGenerateInfo(5, ECivShape.TRIANGLE, 3);
        //BuildingGenerate(new Vector2(9, 9), Preset(info1), 0);
    }

    public CivPreset Preset(CivGenerateInfo info)
    {
        List<CivPreset> temp = new List<CivPreset>();

        foreach(CivPreset pre in maker.GetList())
        {
            if(info.Scale() == pre.info.Scale())
            {
                temp.Add(pre);
            }
        }
        if (temp.Count <= 0)
        {
            return maker.Default();
        }

        List<CivPreset> tmp = new List<CivPreset>();

        foreach (CivPreset pre in temp)
        {
            if (info.Shape() == pre.info.Shape())
            {
                tmp.Add(pre);
            }
        }
        if (tmp.Count <= 0)
        {
            return maker.Default();
        }

        CivPreset preset = tmp.Find(item => item.info.Density() == info.Density());

        if(preset.info.Scale() == 0)
        {
            return maker.Default();
        }

        return preset;
    }    

    public List<BuildingEntity> BuildingGenerate(Vector2Int position, Vector2Int currentGridSize, CivPreset preset, int noise)
    {
        List<BuildingEntity> enemyBuilding = new List<BuildingEntity>();
        Vector2Int buildPos = position;
        
        for (int i = 0; i < preset.civ.GetLength(0); i++)
        {
            for (int j = 0; j < preset.civ.GetLength(1); j++)
            {
                if (buildPos.x < 0 || buildPos.x > currentGridSize.x ||
                    buildPos.y < 0 || buildPos.y > currentGridSize.y)
                {
                    continue;
                }

                    ECivElement element = preset.civ[i, j];
                switch (element)
                {
                    case ECivElement.EMPTY:
                        break;
                    case ECivElement.DUMMY:
                        BuildingEntity entity = Instantiate(dummy).GetComponent<BuildingEntity>();

                        entity.Territory = 5;
                        entity.gameObject.layer = LayerMask.NameToLayer("Enemy");
                        grid.Locate(entity, buildPos.x, buildPos.y, EBuilding.BUILDING, entity.Territory, false);
                        enemyBuilding.Add(entity);

                        entity.InitStats();
                        //barricadeGenerator.SetBarricadeVec(entity.Territory, new Vector2(entity.transform.position.x, entity.transform.position.z), entity.GetTerritoryType());
                        break;
                    case ECivElement.COMMAND:
                        Command commandEntity = Instantiate(command).GetComponent<Command>();

                        commandEntity.Territory = 5;
                        commandEntity.gameObject.layer = LayerMask.NameToLayer("Enemy");
                        miniMapManager.SetColor(commandEntity.gameObject);

                        grid.Locate(commandEntity, buildPos.x, buildPos.y, EBuilding.COMMAND, commandEntity.Territory, false);
                        commandEntity.InitStats();


                        enemyBuilding.Add(commandEntity);
                        barricadeGenerator.SetBarricadeVec(commandEntity.Territory, new Vector2(commandEntity.transform.position.x, commandEntity.transform.position.z), commandEntity.GetTerritoryType());
                        break;
                    case ECivElement.STORAGE:
                        BuildingEntity storageEntity = Instantiate(storage).GetComponent<BuildingEntity>();

                        storageEntity.Territory = 5;
                        storageEntity.gameObject.layer = LayerMask.NameToLayer("Enemy");
                        miniMapManager.SetColor(storageEntity.gameObject);
                        grid.Locate(storageEntity, buildPos.x, buildPos.y, EBuilding.BUILDING, storageEntity.Territory, false);
                        storageEntity.InitStats();

                        enemyBuilding.Add(storageEntity);
                        break;
                    case ECivElement.BARRACK:
                        BuildingEntity barrackEntity = Instantiate(barrack).GetComponent<BuildingEntity>();

                        barrackEntity.Territory = 5;
                        barrackEntity.gameObject.layer = LayerMask.NameToLayer("Enemy");
                        miniMapManager.SetColor(barrackEntity.gameObject);
                        grid.Locate(barrackEntity, buildPos.x, buildPos.y, EBuilding.BARRACK, barrackEntity.Territory, false);

                        barrackEntity.InitStats();

                        enemyBuilding.Add(barrackEntity);
                        raidPlayerAI.unitGenerateAI.unitGeneratorsBarrack.Add(barrackEntity.gameObject.GetComponent<UnitGeneratorBarrack>());
                        break;
                    case ECivElement.AIRBARRACK:
                        BuildingEntity airBarrackEntity = Instantiate(airBarrack).GetComponent<BuildingEntity>();

                        airBarrackEntity.Territory = 5;
                        airBarrackEntity.gameObject.layer = LayerMask.NameToLayer("Enemy");
                        miniMapManager.SetColor(airBarrackEntity.gameObject);
                        grid.Locate(airBarrackEntity, buildPos.x, buildPos.y, EBuilding.BARRACK, airBarrackEntity.Territory, false);

                        airBarrackEntity.InitStats();

                        enemyBuilding.Add(airBarrackEntity);
                        var barr = airBarrackEntity.gameObject.GetComponent<UnitGeneratorAirDrop>();
                        if(barr)
                           FindObjectOfType<RaidPlayerAI>().unitGenerateAI.unitGeneratorsAirDrop.Add(barr);
                        //raidPlayerAI.unitGenerateAI.unitGeneratorsBarrack.Add(airBarrackEntity.gameObject.GetComponent<UnitGeneratorBarrack>());
                        break;
                }
                buildPos.x += 4;
            }
            buildPos.y -= 4;
            buildPos.x = position.x;
        }

        // barricadeGenerator.Generate(0, grid.GetBarricadeArea());

        return enemyBuilding;
    }

    public void BarricadeGenerator()
    {
        barricadeGenerator.Generate(0, grid.GetBarricadeArea());
    }

}