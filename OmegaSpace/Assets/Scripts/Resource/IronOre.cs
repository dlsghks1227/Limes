using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronOre : Ore
{
    private Iron iron = new Iron();

    [SerializeField] List<Mesh> meshes = new List<Mesh>();

    private void OnEnable()
    {
        gameObject.GetComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Count)];
    }

    public override EResource GetResourceType()
    {
        return EResource.RES_IRON;
    }
}