using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpaceGenerator : MonoBehaviour
{
    public enum PlanetType
    {
        NONE = 0,
        PLANET,
        ASTEROID,
    }

    [Header("Game objects")]
    [SerializeField] private GameObject planet = null;
    [SerializeField] private GameObject asteroid = null;
    [SerializeField] private GameObject lineObject = null;

    [Header("Object pool")]
    [SerializeField] private ObjectPool objectPool = null;

    [Header("Skybox")]
    [SerializeField] private List<Material> skyboxMaterials = new List<Material>();

    public float distacne = 20.0f;

    public int spaceSize = 49;

    public PlanetType[,]        planetsType;
    public List<SpaceEntity>    planets = new List<SpaceEntity>();

    private int[,]              direction = { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };

    private void Start()
    {
        objectPool.AddObject(planet, "planet", 1000, true);
        objectPool.AddObject(asteroid, "asteroid", 100, true);
        objectPool.AddObject(lineObject, "line", 1000, true);
    }

    public Planet SpaceGenerate()
    {
        planetsType = new PlanetType[spaceSize, spaceSize];

        // int[,] direction    = { { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 1 }, { -1, 0 }, { -1, -1 }, { 0, -1 }, { 1, -1 } };
        // int[,] direction    = { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };
        // int[,] direction    = { { 1, 1 }, { 1, -1 }, { -1, -1 }, { -1, 1 } };

        // 중심 생성
        GameObject centerPlanet     = CreatePlanet(spaceSize / 2, spaceSize / 2, PlanetType.PLANET);
        Planet centerSpaceEntity    = centerPlanet.GetComponent<Planet>();

        // BFS
        Queue<Vector2Int>   queue = new Queue<Vector2Int>();
        bool[,]             visit = new bool[spaceSize, spaceSize];
        bool[,]             lineVisit = new bool[spaceSize * 2, spaceSize * 2];
        Vector3[,]          linePos = new Vector3[spaceSize, spaceSize];
        linePos.Initialize();

        queue.Enqueue(centerSpaceEntity.Position);

        visit[centerSpaceEntity.position.x, centerSpaceEntity.position.y] = true;
        linePos[centerSpaceEntity.position.x, centerSpaceEntity.position.y] = centerPlanet.transform.position;

        planetsType[centerSpaceEntity.position.x, centerSpaceEntity.position.y] = PlanetType.PLANET;

        while (queue.Count > 0)
        {
            Vector2Int  curr = queue.Dequeue();
            Vector2Int  next = new Vector2Int();

            bool        exists = AsteroidExists(curr, direction, planetsType);

            if (planetsType[curr.x, curr.y] == PlanetType.ASTEROID)
            {
                exists = true;
            }

            for (int i = 0; i < (direction.Length / 2); i++)
            {
                next.x = curr.x + direction[i, 0];
                next.y = curr.y + direction[i, 1];

                if (next.x < 0 || next.x >= spaceSize ||
                    next.y < 0 || next.y >= spaceSize)
                {
                    continue;
                }

                double distance = Mathf.Sqrt(Mathf.Pow((next.x - spaceSize / 2), 2) + Mathf.Pow((next.y - spaceSize / 2), 2));

                if (spaceSize / 2 < distance)
                {
                    continue;
                }
                
                if (visit[next.x, next.y] == true)
                {
                    CreateLine(curr, next, linePos, lineVisit);

                    continue;
                }

                queue.Enqueue(next);

                visit[next.x, next.y] = true;

                exists = AsteroidExists(next, direction, planetsType);
                GameObject entity;

                if (exists == true)
                {
                    planetsType[next.x, next.y] = PlanetType.PLANET;
                    entity = CreatePlanet(next.x, next.y, PlanetType.PLANET);
                }
                else
                {
                    if (Random.Range(0, 11) != 1)
                    {
                        planetsType[next.x, next.y] = PlanetType.PLANET;
                        entity = CreatePlanet(next.x, next.y, PlanetType.PLANET);
                    }
                    else
                    {
                        exists = true;

                        planetsType[next.x, next.y] = PlanetType.ASTEROID;
                        entity = CreatePlanet(next.x, next.y, PlanetType.ASTEROID);
                    }
                }

                linePos[next.x, next.y] = entity.transform.position;

                CreateLine(curr, next, linePos, lineVisit);
            }
        }

        // 스카이 박스 랜덤 배치
        RenderSettings.skybox = skyboxMaterials[Random.Range(0, skyboxMaterials.Count)];

        return centerSpaceEntity;
    }

    public bool IsMoveable(Planet currentPlanet, Planet nextPlanet)
    {
        Vector2Int current = currentPlanet.position;
        Vector2Int next = nextPlanet.position;

        if (Vector2Int.Distance(current, next) > 1.0f)
        {
            return false;
        }

        return true;
    }

    private GameObject CreatePlanet(int x, int y, PlanetType type)
    {
        GameObject  entity = null;
        Vector3     position = new Vector3(
            (((x - (spaceSize / 2)) * distacne) + Random.Range(-((distacne * 0.5f) - 1.0f), ((distacne * 0.5f) - 1.0f))),
            0.0f,
            (((y - (spaceSize / 2)) * distacne) + Random.Range(-((distacne * 0.5f) - 1.0f), ((distacne * 0.5f) - 1.0f))));

        switch (type)
        {
            case PlanetType.NONE:
                break;
            case PlanetType.PLANET:
                entity = objectPool.GetPooledObject("planet");
                entity.SetActive(true);
                break;
            case PlanetType.ASTEROID:
                entity = objectPool.GetPooledObject("asteroid");
                entity.SetActive(true);
                break;
        }

        if (entity == null)
        {
            return null;
        }

        entity.transform.position = position + gameObject.transform.position;

        SpaceEntity spaceEntity = entity.GetComponent<SpaceEntity>();
        if (spaceEntity == null)
        {
            return null;
        }

        spaceEntity.Position = new Vector2Int(x, y);
        planets.Add(spaceEntity);
        return entity;
    }

    private bool AsteroidExists(Vector2Int current, int[,] direction, PlanetType[,] planetsType)
    {
        Vector2Int next = new Vector2Int();
        for (int i = 0; i < (direction.Length / 2); i++)
        {
            next.x = current.x + direction[i, 0];
            next.y = current.y + direction[i, 1];

            if (next.x < 0 || next.x >= spaceSize ||
                next.y < 0 || next.y >= spaceSize)
            {
                continue;
            }

            if (planetsType[next.x, next.y] == PlanetType.ASTEROID)
            {
                return true;
            }
        }

        return false;
    }

    private void CreateLine(Vector2Int current, Vector2Int next, Vector3[,] linePos, bool[,] lineVisit)
    {
        Vector2Int lineCenter = new Vector2Int((current.x + next.x), (current.y + next.y));

        if (planetsType[next.x, next.y] != PlanetType.NONE &&
            planetsType[current.x, current.y] != PlanetType.NONE &&
            lineVisit[lineCenter.x, lineCenter.y] == false)
        {
            GameObject lineObject = objectPool.GetPooledObject("line");
            LineRenderer lineRender = lineObject.GetComponent<LineRenderer>();

            if (lineRender == null)
            {
                return;
            }
            lineObject.SetActive(true);
            lineRender.SetPosition(0, linePos[current.x, current.y]);
            lineRender.SetPosition(1, linePos[next.x, next.y]);

            lineVisit[lineCenter.x, lineCenter.y] = true;
        }
    }
}
