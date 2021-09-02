using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Scripting.Pipeline;

public enum ETerritory
{
    NONE,
    OUR,
    ENEMY,
    DISPUTE
}

public class Grid : MonoBehaviour
{
    private Vector2Int gridSize;
    private RectInt gridRect;

    [Header("Rendering")]
    [SerializeField] private Material material;

    public Vector2Int   GridSize { get => gridSize; }
    public RectInt      GridRect { get => gridRect; }

    private EBuilding[,]    buildingTile;
    private ETerritory[,]   territoryTile;
    private int[,]          boundaryTileCount;

    // 현재 행성에 배치되어있는 건물 또는 광물
    [SerializeField] public List<BuildingEntity> currentPlayerBuildings  = new List<BuildingEntity>();
    [SerializeField] public List<BuildingEntity> currentEnemyBuildings   = new List<BuildingEntity>();
    [SerializeField] public List<Ore>            currentOres             = new List<Ore>();

    public void Initialize(Vector2Int size)
    {
        gridSize = size - new Vector2Int(size.x % 2, size.y % 2);

        if (material == null)
        {
            Debug.LogError("variable \"material\" is not invalid");
            gameObject.SetActive(false);
            return;
        }

        Renderer render = gameObject.transform.GetComponent<Renderer>();
        if (render == null)
        {
            Debug.LogError("render cannot find");
            gameObject.SetActive(false);
            return;
        }

        // render.material = material;

        //Renderer render = transform.GetComponent<Renderer>();
        //if (render == null)
        //{
        //    Debug.LogError("render cannot find");
        //    gameObject.SetActive(false);
        //    return;
        //}

        if (gridSize.x <= 0 || gridSize.y <= 0)
        {
            Debug.LogError("gridScale must be positive integer");
            gameObject.SetActive(false);
            return;
        }

        Mesh mesh = new Mesh();
        mesh.Clear();

        Vector3Int positionInt = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        transform.position = positionInt;

        Vector2Int topLeft = new Vector2Int(
            positionInt.x - (gridSize.x / 2),
            positionInt.z + (gridSize.y / 2));
        Vector2Int bottomRight = new Vector2Int(
            positionInt.x + (gridSize.x / 2),
            positionInt.z - (gridSize.y / 2));
        gridRect = new RectInt(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);

        //Vector3[] vertices = new Vector3[4];
        //vertices[0] = new Vector3(-(gridSize.x / 2), 0.0f, -(gridSize.y / 2));
        //vertices[1] = new Vector3( (gridSize.x / 2), 0.0f, -(gridSize.y / 2));
        //vertices[2] = new Vector3(-(gridSize.x / 2), 0.0f,  (gridSize.y / 2));
        //vertices[3] = new Vector3( (gridSize.x / 2), 0.0f,  (gridSize.y / 2));

        //int[] triangles = new int[6];
        //triangles[0] = 0;
        //triangles[1] = 3;
        //triangles[2] = 1;
        //triangles[3] = 3;
        //triangles[4] = 0;
        //triangles[5] = 2;

        //Vector2[] uvs = new Vector2[vertices.Length];
        //uvs[0] = new Vector2(0, 0);
        //uvs[1] = new Vector2(1, 0);
        //uvs[2] = new Vector2(0, 1);
        //uvs[3] = new Vector2(1, 1);

        //mesh.vertices = vertices;
        //mesh.triangles = triangles;
        //mesh.uv = uvs;

        GetComponent<BoxCollider>().size = new Vector3(gridSize.x, 0, gridSize.y);

        //mesh.name = "Grid";
        //GetComponent<MeshFilter>().mesh = mesh;

        gameObject.transform.localScale = new Vector3(gridSize.x / 10.0f, 1.0f, gridSize.y / 10.0f);
        render.material = material;
        render.material.SetVector("Tiling", new Vector4((int)(gridSize.x * 0.2f), (int)(gridSize.y * 0.2f), 0, 0));

        territoryTile       = new ETerritory[gridSize.x + 1, gridSize.y + 1];
        buildingTile        = new EBuilding[gridSize.x + 1, gridSize.y + 1];
        boundaryTileCount   = new int[gridSize.x + 1, gridSize.y + 1];

        territoryTile.Initialize();
        buildingTile.Initialize();
        boundaryTileCount.Initialize();

        InitializeCurrentEntity();
    }

    public void InitializeCurrentEntity()
    {
        currentPlayerBuildings.Clear();
        currentEnemyBuildings.Clear();
        currentOres.Clear();
    }

    private void Update()
    {
        //for (int x = 0; x < gridSize.x; x++)
        //{
        //    for (int y = 0; y < gridSize.y; y++)
        //    {
        //        if (territoryTile[x, y] == ETerritory.ENEMY)
        //        {
        //            Vector2 position = GetGridToWorld(x, y);
        //            Debug.DrawRay(new Vector3(position.x, 0.0f, position.y), Vector3.up * (boundaryTileCount[x, y] + 1), new Color(0.0f, 0.0f, 1.0f));
        //        }
        //        else if (territoryTile[x, y] == ETerritory.OUR)
        //        {
        //            Vector2 position = GetGridToWorld(x, y);
        //            Debug.DrawRay(new Vector3(position.x, 0.0f, position.y), Vector3.up * (boundaryTileCount[x, y] + 1), new Color(1.0f, 0.0f, 1.0f));
        //        }
        //        else if (territoryTile[x, y] == ETerritory.DISPUTE)
        //        {
        //            Vector2 position = GetGridToWorld(x, y);
        //            Debug.DrawRay(new Vector3(position.x, 0.0f, position.y), Vector3.up * (boundaryTileCount[x, y] + 1), new Color(0.5f, 1.0f, 0.5f));
        //        }
        //        else
        //        {
        //            Vector2 position = GetGridToWorld(x, y);
        //            Debug.DrawRay(new Vector3(position.x, 0.0f, position.y), Vector3.up * (boundaryTileCount[x, y] + 1), new Color(1.0f, 0.0f, 0.0f));
        //        }
        //    }
        //}

        //for (int x = 0; x < gridSize.x; x++)
        //{
        //    for (int y = 0; y < gridSize.y; y++)
        //    {
        //        if (buildingTile[x, y] == EBuilding.NONE)
        //        {
        //            Vector2 position = GetGridToWorld(x, y);
        //            Debug.DrawRay(new Vector3(position.x, 0.0f, position.y), Vector3.up * (boundaryTileCount[x, y] + 1), new Color(0.0f, 0.0f, 0.0f));
        //        }
        //        else if (buildingTile[x, y] == EBuilding.ORE)
        //        {
        //            Vector2 position = GetGridToWorld(x, y);
        //            Debug.DrawRay(new Vector3(position.x, 0.0f, position.y), Vector3.up * (boundaryTileCount[x, y] + 1), new Color(0.0f, 0.0f, 1.0f));
        //        }
        //        else if (buildingTile[x, y] == EBuilding.BUILDING)
        //        {
        //            Vector2 position = GetGridToWorld(x, y);
        //            Debug.DrawRay(new Vector3(position.x, 0.0f, position.y), Vector3.up * (boundaryTileCount[x, y] + 1), new Color(0.0f, 1.0f, 0.0f));
        //        }
        //        else if (buildingTile[x, y] == EBuilding.COMMAND)
        //        {
        //            Vector2 position = GetGridToWorld(x, y);
        //            Debug.DrawRay(new Vector3(position.x, 0.0f, position.y), Vector3.up * (boundaryTileCount[x, y] + 1), new Color(0.0f, 1.0f, 1.0f));
        //        }
        //        else if (buildingTile[x, y] == EBuilding.BARRICADE)
        //        {
        //            Vector2 position = GetGridToWorld(x, y);
        //            Debug.DrawRay(new Vector3(position.x, 0.0f, position.y), Vector3.up * (boundaryTileCount[x, y] + 1), new Color(1.0f, 0.0f, 0.0f));
        //        }
        //        else
        //        {
        //            Vector2 position = GetGridToWorld(x, y);
        //            Debug.DrawRay(new Vector3(position.x, 0.0f, position.y), Vector3.up * (boundaryTileCount[x, y] + 1), new Color(1.0f, 0.0f, 1.0f));
        //        }
        //    }
        //}
    }

    private bool IsValid(Vector2Int position)
    {
        if (position.x < 0 || position.x > gridSize.x ||
            position.y < 0 || position.y > gridSize.y)
        {
            Debug.LogError("Index out of range");
            return false;
        }

        return true;
    }


    public bool IsEmpty(Vector2 pivot, Vector2 edge)
    {
        Vector2Int tilePivot = GetWorldToGrid(pivot);
        Vector2Int tileEdge = GetWorldToGrid(edge);

        if (IsValid(tilePivot) == false ||
            IsValid(tileEdge) == false)
        {
            return false;
        }

        for (int x = tilePivot.x; x < tileEdge.x; x++)
        {
            for (int y = tilePivot.y; y < tileEdge.y; y++)
            {
                if (territoryTile[x, y] != ETerritory.NONE)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void SetTerritoryTilesByRange(GridEntity entity, ETerritory type)
    {
        Vector2Int tilePivot = GetWorldToGrid(entity.Pivot);
        Vector2Int tileEdge = GetWorldToGrid(entity.Edge);

        if (IsValid(tilePivot) == false ||
            IsValid(tileEdge) == false)
        {
            return;
        }

        for (int x = tilePivot.x; x < tileEdge.x; x++)
        {
            for (int y = tilePivot.y; y < tileEdge.y; y++)
            {
                territoryTile[x, y] = type;
            }
        }
    }

    public void SetTerritoryTilesByPosition(Vector2 position, ETerritory type)
    {
        Vector2Int tilePivot = GetWorldToGrid(position);
        Vector2Int tileEdge = GetWorldToGrid(position);

        if (IsValid(tilePivot) == false ||
            IsValid(tileEdge) == false)
        {
            return;
        }

        for (int x = tilePivot.x; x <= tileEdge.x; x++)
        {
            for (int y = tilePivot.y; y <= tileEdge.y; y++)
            {
                territoryTile[x, y] = type;
            }
        }
    }

    public void SetBuildingTilesByRange(GridEntity entity, EBuilding type)
    {
        Vector2Int tilePivot = GetWorldToGrid(entity.Pivot);
        Vector2Int tileEdge = GetWorldToGrid(entity.Edge);

        if (IsValid(tilePivot) == false ||
            IsValid(tileEdge) == false)
        {
            return;
        }

        for (int x = tilePivot.x; x < tileEdge.x; x++)
        {
            for (int y = tilePivot.y; y < tileEdge.y; y++)
            {
                buildingTile[x, y] = type;
            }
        }
    }

    public void SetBuildingTilesByPosition(Vector2 position, EBuilding type)
    {
        Vector2Int tilePivot = GetWorldToGrid(position);
        Vector2Int tileEdge = GetWorldToGrid(position);

        if (IsValid(tilePivot) == false ||
            IsValid(tileEdge) == false)
        {
            return;
        }

        for (int x = tilePivot.x; x <= tileEdge.x; x++)
        {
            for (int y = tilePivot.y; y <= tileEdge.y; y++)
            {
                buildingTile[x, y] = type;
            }
        }
    }

    private int CountBarricadeArea()
    {
        int count = 0;
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {

                if (buildingTile[x, y] == EBuilding.BARRICADE)
                {
                    count++;
                }

            }
        }
        return count;
    }

    public Vector2[] GetBarricadeArea()
    {
        Vector2[] barricadeVec = new Vector2[CountBarricadeArea()];
        int count = 0;
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (buildingTile[x, y] == EBuilding.BARRICADE)
                {
                    barricadeVec[count] = GetGridToWorld(x, y);
                    count++;
                }
            }
        }

        return barricadeVec;
    }

    // 영역 표시
    private void SetBoundary(GridEntity entity, int scale, bool isPlayer)
    {
        if (scale <= 0)
        {
            return;
        }

        int start = scale;
        int end = scale;

        Vector2Int entityPos = GetWorldToGrid(entity.Center);

        for (int height = 0; height < (scale * 2) + 1; height++)
        {
            for (int width = start; width <= end; width++)
            {
                int posX = width + entityPos.x - scale;
                int posY = height + entityPos.y - scale;

                if (posX < 0 || posX > gridSize.x ||
                    posY < 0 || posY > gridSize.y)
                {
                    continue;
                }

                ETerritory currentType = GetTerritoryTileState(posX, posY);

                if (currentType.Equals(ETerritory.DISPUTE))
                {
                    boundaryTileCount[posX, posY] += 1;
                    territoryTile[posX, posY] = ETerritory.DISPUTE;
                    continue;
                }
                else if (currentType.Equals(ETerritory.OUR) && isPlayer == false)
                {
                    boundaryTileCount[posX, posY] += 1;
                    territoryTile[posX, posY] = ETerritory.DISPUTE;
                    continue;
                }
                else if (currentType.Equals(ETerritory.ENEMY) && isPlayer == true)
                {
                    boundaryTileCount[posX, posY] += 1;
                    territoryTile[posX, posY] = ETerritory.DISPUTE;
                    continue;
                }

                boundaryTileCount[posX, posY] += 1;
                territoryTile[posX, posY] = isPlayer ? ETerritory.OUR : ETerritory.ENEMY;
            }

            if (height < scale)
            {
                start--;
                end++;
            }
            else
            {
                start++;
                end--;
            }
        }
    }

    // 영역 초기화
    private void ResetBoundary(GridEntity entity, int scale, bool isPlayer)
    {
        if (scale <= 0)
        {
            return;
        }

        int start = scale;
        int end = scale;

        Vector2Int entityPos = GetWorldToGrid(entity.Center);

        for (int height = 0; height < (scale * 2) + 1; height++)
        {
            for (int width = start; width <= end; width++)
            {
                int posX = width + entityPos.x - scale;
                int posY = height + entityPos.y - scale;

                if (posX < 0 || posX > gridSize.x ||
                    posY < 0 || posY > gridSize.y)
                {
                    continue;
                }

                ETerritory currentType = GetTerritoryTileState(posX, posY);

                if (currentType.Equals(ETerritory.DISPUTE))
                {
                    if (GetBoundaryTileCount(posX, posY) > 2)
                    {
                        boundaryTileCount[posX, posY] -= 1;
                        continue;
                    }
                    boundaryTileCount[posX, posY] -= 1;
                    territoryTile[posX, posY] = isPlayer ? ETerritory.ENEMY : ETerritory.OUR;
                    continue;
                }
                else if (currentType.Equals(ETerritory.OUR) && isPlayer == true)
                {
                    if (GetBoundaryTileCount(posX, posY) > 1)
                    {
                        boundaryTileCount[posX, posY] -= 1;
                        continue;
                    }
                }
                else if (currentType.Equals(ETerritory.ENEMY) && isPlayer == false)
                {
                    if (GetBoundaryTileCount(posX, posY) > 1)
                    {
                        boundaryTileCount[posX, posY] -= 1;
                        continue;
                    }
                }

                boundaryTileCount[posX, posY] = 0;
                territoryTile[posX, posY] = ETerritory.NONE;
            }

            if (height < scale)
            {
                start--;
                end++;
            }
            else
            {
                start++;
                end--;
            }
        }
    }

    public void Locate(BuildingEntity entity, int x, int y, EBuilding type, int scale = 0, bool isPlayer = true)
    {
        Vector2 gridPosition = GetGridToWorld(x, y);
        FixExtrusion(entity, gridPosition);

        entity.Territory = scale;

        SetBuildingTilesByRange(entity, type);
        SetBoundary(entity, scale, isPlayer);

        if (isPlayer == true)
        {
            currentPlayerBuildings.Add(entity);
        }
        else
        {
            currentEnemyBuildings.Add(entity);
        }
    }

    public void Locate(BuildingEntity entity, Vector2 position, EBuilding type, int scale = 0, bool isPlayer = true)
    {
        entity.SetPosition(position);
        entity.Territory = scale;

        SetBuildingTilesByRange(entity, type);
        SetBoundary(entity, scale, isPlayer);

        if (isPlayer == true)
        {
            currentPlayerBuildings.Add(entity);
        }
        else
        {
            currentEnemyBuildings.Add(entity);
        }
    }

    public void LocatedOre(Ore entity, int x, int y)
    {
        Vector2 gridPosition = GetGridToWorld(x, y);
        FixExtrusion(entity, gridPosition);

        SetBuildingTilesByRange(entity, EBuilding.ORE);

        currentOres.Add(entity);
    }

    public void Unbuild(BuildingEntity entity, bool isPlayer = true)
    {
        SetBuildingTilesByRange(entity, EBuilding.NONE);
        ResetBoundary(entity, entity.Territory, isPlayer);
        entity.gameObject.SetActive(false);
    }

    public void UnbuildOre(Ore entity)
    {
        SetBuildingTilesByRange(entity, EBuilding.NONE);
        entity.gameObject.SetActive(false);
    }

    /// <summary>
    /// 지정된 좌표의 타일 상태를 가져옵니다.
    /// </summary>
    /// <param name="x">그리드 좌표 x 축</param>
    /// <param name="y">그리드 좌표 y 축</param>
    /// <returns>타일 상태를 반환합니다.</returns>
    public ETerritory GetTerritoryTileState(int x, int y)
    {
        if (IsValid(new Vector2Int(x, y)) == false)
        {
            return ETerritory.NONE;
        }

        return territoryTile[x, y];
    }

    public EBuilding GetBuildingTileState(int x, int y)
    {
        if (IsValid(new Vector2Int(x, y)) == false)
        {
            return EBuilding.NONE;
        }

        return buildingTile[x, y];
    }

    public int GetBoundaryTileCount(int x, int y)
    {
        if (IsValid(new Vector2Int(x, y)) == false)
        {
            return -1;
        }

        return boundaryTileCount[x, y];
    }

    /// <summary>
    /// 월드 좌표 위 그리드 좌표를 반환합니다. (좌측 상단 기준)
    /// </summary>
    /// <param name="position">월드 좌표</param>
    /// <returns>그리드 좌표 Vector2Int를 반환합니다.</returns>
    public Vector2Int GetWorldToGrid(Vector2 position)
    {
        return new Vector2Int(
            (int)( position.x + (gridSize.x * 0.5f) - gameObject.transform.position.x),
            (int)(-position.y + (gridSize.y * 0.5f) + gameObject.transform.position.z));
    }

    /// <summary>
    /// 그리드 좌표 위 윌드 좌표를 반환합니다. (좌측 상단 기준)
    /// </summary>
    /// <param name="x">그리드 좌표 x 축</param>
    /// <param name="y">그리드 좌표 y 축</param>
    /// <returns>월드 좌표 Vector2를 반환합니다.</returns>
    public Vector2 GetGridToWorld(int x, int y)
    {
        if (IsValid(new Vector2Int(x, y)) == false)
        {
            return Vector2.zero;
        }

        return new Vector2(
              x  - (gridSize.x * 0.5f) + gameObject.transform.position.x + 0.5f,
            (-y) + (gridSize.y * 0.5f) + gameObject.transform.position.z - 0.5f);
    }

    private void FixExtrusion(GridEntity entity, Vector2 gridPosition)
    {
        if (entity == null)
        {
            return;
        }

        Vector2 fixPosition = new Vector2(0.0f, 0.0f);

        if (gridPosition.x + entity.LocalPivot.x < GridRect.x)
        {
            fixPosition -= new Vector2(gridPosition.x + entity.LocalPivot.x - GridRect.x, 0.0f);
        }
        if (gridPosition.y + entity.LocalPivot.y > GridRect.y)
        {
            fixPosition -= new Vector2(0.0f, gridPosition.y + entity.LocalPivot.y - GridRect.y);
        }
        if (gridPosition.x + entity.LocalEdge.x > GridRect.width)
        {
            fixPosition -= new Vector2(gridPosition.x + entity.LocalEdge.x - GridRect.width, 0.0f);
        }
        if (gridPosition.y + entity.LocalEdge.y < GridRect.height)
        {
            fixPosition -= new Vector2(0.0f, gridPosition.y + entity.LocalEdge.y - GridRect.height);
        }

        entity.SetCenterPosition(gridPosition + fixPosition);
    }
}