using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoSingleton<PlayerDataManager>
{

    // 자원
    [SerializeField]
    private List<ResourceAmount> playerResourceList = new List<ResourceAmount>(3);

    [Header("Ether increase per second")]
    [SerializeField]
    private int increment;

    private TechManager playerTechManager;

    public Planet           currentPlanet   { get; set; }
    public int              currentPlanetID { get; private set; }
    public float            CurrentResearchTime { get; private set; }

    private void Start()
    {
        StartCoroutine("IncreaseResource");
    }
    public void Initialize()
    {
        playerTechManager = TechManager.instance;
    }

    public void AddPlayerResource(ResourceAmount resource)
    {
        int i = 0;
        for (; i < playerResourceList.Count; i++)
            if (playerResourceList[i].resType == resource.resType)
            {
                var res = playerResourceList[i];
                res.AddAmount(resource);
                playerResourceList[i] = res;
            }
    }

    public void SubstractPlayerResource(ResourceAmount resource) 
    {
        int i = 0;
        for (; i < playerResourceList.Count; i++)
            if (playerResourceList[i] >= resource)
            {
                var res = playerResourceList[i];
                res.SubstractAmount(resource);
                playerResourceList[i] = res;
            }
    }

    public bool IsAffordableToPay(ResourceAmount resource)
    {
        int i = 0;
        for (; i < playerResourceList.Count; i++)
            if (playerResourceList[i] >= resource)
                return true;
        return false;
    }
    
    public ResourceAmount GetPlayerResourceOf(EResource type)
    {
        foreach (var res in playerResourceList)
            if (res.resType.Equals(type))
            {
                return res;
            }
        return new ResourceAmount(EResource.RES_NONE,0);
    }

    IEnumerator IncreaseResource()
    {
        WaitForSeconds sec = new WaitForSeconds(1.0f);
        ResourceAmount res = new ResourceAmount(EResource.RES_ETHER, 0);
        res.resAmount = res.resAmount + increment;

        while (true)
        {
            AddPlayerResource(res);
            yield return sec;    
        }
    }
}
