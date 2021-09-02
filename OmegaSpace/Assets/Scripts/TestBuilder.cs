using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestBuilder : MonoBehaviour
{
    private RectInt bound;
    [SerializeField]
    private Grid grid;

    private StructGridBuilder builder;

    private GameObject building;
    private Ore ore;
    private BuildingEntity benty;

    [SerializeField]
    private ResourceStorage storage = null;

    void Start()
    {
        //builder = new StructGridBuilder(grid);
        grid.Initialize(new Vector2Int(20, 20));
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                building = hit.collider.gameObject;
                benty = building.GetComponent<BuildingEntity>();
                //builder.SelectStructure();
            }
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            //if (builder.IsLocatedStructures())
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    {
                        grid.Locate(benty, new Vector2(hit.point.x, hit.point.z), benty.GetBuildingType());
                        building.GetComponent<Collider>().enabled = true;
                    }
                }
            }
        }
    }
}