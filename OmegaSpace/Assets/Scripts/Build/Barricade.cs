using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// 투사체 통과 O
public class Barricade : BuildingEntity, ILevelInterface
{
    [SerializeField]
    private UnitStats barricadeStat;
    private bool isUnderGround = false;
    private ResourceStorage storage;
    private Animator elevateManager;

    [SerializeField] List<Mesh> meshes = new List<Mesh>();

    public bool IsElevatable
    {
        get;set;
    }

    private void OnEnable()
    {
        elevateManager = GetComponent<Animator>();

        gameObject.GetComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Count)];
    }

    public void LevelUp(int value)
    {

    }

    public override void Destroyed()
    {
        ResourceAmount res = new ResourceAmount(EResource.RES_IRON, 100);
        storage.Store(res);
    }

    public void ElevateBarricade()
    {
        if (IsElevatable) 
            elevateManager.SetBool("hasElevateOrder", true);
    }
}