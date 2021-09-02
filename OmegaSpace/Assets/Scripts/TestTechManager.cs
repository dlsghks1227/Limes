using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTechManager : MonoBehaviour
{
    private TechManager techManager;
    public List<BuildingData> testBuildings;

    public void TestAddPresentBuilding()
    {
        foreach (var a in testBuildings)
        {
            GameObject obj = null;
            for (int i = 0; i < 50; i++)
            {
                obj = Instantiate(a.BuildingPrefab, Vector3.zero, Quaternion.identity);
                techManager.AddPresentBuilding(obj);
            }
        }
    }
    void Start()
    {
        techManager = TechManager.Instance;
        TestAddPresentBuilding();
    }
}
