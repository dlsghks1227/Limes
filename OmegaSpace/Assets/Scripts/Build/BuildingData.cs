using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "build Data", menuName = "Scriptable Object/Build Data")]
public class BuildingData : ScriptableObject
{
    [SerializeField]
    private GameObject buildingPrefab;

    [SerializeField]
    private string buildingName = null;

    [SerializeField]
    private string explain = null;

    public string Name
    {
        get => buildingName;
    }

    public GameObject BuildingPrefab
    {
        get => buildingPrefab;
    }

    public string Explain
    {
        get => explain;
    }

    [SerializeField]
    private List<Mesh> meshesWhileConstructing = new List<Mesh>();
    public ReadOnlyCollection<Mesh> MeshesWhileConstructing
    {
        get => meshesWhileConstructing.AsReadOnly();
    }

    public bool IsGeneratbale
    {
        get;
        set;
    }
    [SerializeField]
    private ResourceAmount ironCost = new ResourceAmount(EResource.RES_IRON, 0);
    [SerializeField]
    private ResourceAmount goldCost = new ResourceAmount(EResource.RES_GOLD, 0);
    [SerializeField]
    private float buildTime;
    public float BuildTime
    {
        get => buildTime;
    }

    public ResourceAmount IronCost
    {
        get => ironCost;
    }

    public ResourceAmount GoldCost
    {
        get => goldCost;
    }

    [SerializeField]
    Vector2Int gridScale;
    [SerializeField]
    Vector2Int buildableAreaExtent;

    public Vector2 GridScale
    {
        get => gridScale;
    }

    public void AdjustIronCost(float ratio)
    {
        ironCost.resAmount = (int)(ironCost.resAmount * ratio);
    }

    public void AdjustIronCost(int value)
    {
        ironCost.resAmount += value;
    }

    public void AdjustGoldCost(float ratio)
    {
        goldCost.resAmount = (int)(goldCost.resAmount * ratio);
    }

    public void AdjustGoldCost(int value)
    {
        goldCost.resAmount += value;
    }

    public void AdjustGenerateTime(float ratio)
    {
        buildTime *= ratio;
    }

    public void AdjustGenerateTime(int value)
    {
        buildTime += value;
    }
}
