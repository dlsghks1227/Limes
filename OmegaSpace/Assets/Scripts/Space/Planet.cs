using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : SpaceEntity
{

    public string planetName;  //행성 이름
    public int numCivilization;  //문명 수
    public int landSize;
    public int objectID;
    public ResourceAmount iron = new ResourceAmount(EResource.RES_IRON, 0);  //자원1
    public ResourceAmount gold = new ResourceAmount(EResource.RES_GOLD, 0);  //자원2
    private int maxValue = 100;

    public void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        planetName = "푸리링 행성";
        numCivilization = Random.Range(1, 4);
        landSize = Random.Range(100, 200);
        objectID = gameObject.GetInstanceID();
        float rand = Random.Range(0.1f, 1.0f);
        double resourceValue = Mathf.Pow(rand, 2) * maxValue;
        iron.resAmount = (int)resourceValue;
        gold.resAmount = (int)resourceValue;
    }
}
