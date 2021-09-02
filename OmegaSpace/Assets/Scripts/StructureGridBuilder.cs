using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class StructGridBuilder
{
    private Grid gridObject = null;

    private BuildingEntity focusEntity = null;
    private bool colliderEnabled = false;
    private EBuilding type = EBuilding.NONE;

    public StructGridBuilder(Grid grid)
    {
        if (grid == null)
        {
            Debug.LogError("grid has not found");
            return;
        }

        gridObject = grid;
    }

    public void CreateStructure(BuildingEntity building)
    {
        if (building == null)
        {
            return;
        }

        ChangeFocus(building);
    }

    public bool SelectStructure()
    {
        type = EBuilding.NONE;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            BuildingEntity buildingEntity = hit.collider.gameObject.GetComponent<BuildingEntity>();
            if (buildingEntity == null)
            {
                return false;
            }
            else
            {
                type = buildingEntity.GetBuildingType();
            }

            ChangeFocus(buildingEntity);
            //planetGrid.SelectBuildTiles(focusEntity.Pivot, focusEntity.Edge, EBuilding.BUILDING);
            gridObject.Unbuild(focusEntity, false);
            return true;
        }

        return false;
    }

    private void ChangeFocus(BuildingEntity newFocus)
    {
        // 임시로 focusEntity
        if (focusEntity)
        {
            Collider oldCollider = focusEntity.GetComponent<Collider>();
            if (oldCollider)
            {
                oldCollider.enabled = colliderEnabled;
            }
        }

        focusEntity = newFocus;
        Collider newCollider = newFocus.GetComponent<Collider>();
        if (newCollider)
        {
            colliderEnabled = newCollider.enabled;
            newCollider.enabled = false;
        }
    }

    public void PickStructure()
    {
        if (focusEntity == null)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) == false
            || hit.collider.gameObject != gridObject.gameObject)
        {
            return;
        }

        Vector2 gridPosition = GetGridPosition(hit.point);
        FixExtrusion(gridPosition);
    }

    public bool IsLocatedStructures()
    {
        if (focusEntity == null)
        {
            return false;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) == false
            || (hit.collider.gameObject != gridObject.gameObject && hit.collider.gameObject.GetComponent<Ore>() == null))
        {
            return false;
        }

        //if (type == EBuilding.MINER && hit.collider.gameObject.GetComponent<Ore>() == null)
        //{
        //    Debug.LogError("can't build miner here");
        //    return false;
        //}
        //else if (type == EBuilding.MINER && hit.collider.gameObject.GetComponent<Ore>() != null)
        //{
        //    return false;
        //}

        //if (type == EBuilding.COMMAND)
        //{
        //    return false;
        //}

        //if (gridObject.IsEmpty(focusEntity.Pivot, focusEntity.Edge) == false)
        //{
        //    return false;
        //}

        gridObject.Locate(focusEntity, GetGridPosition(hit.point), EBuilding.BUILDING, 5, true);

        return true;
        ////if (planetGrid.CheckPlanetTileState(GetGridPosition(hit.point)) != ETerritory.OUR)
        //{
        //    Debug.LogError("not our territory" + ": " + GetGridPosition(hit.point));
        //    return false;
        //}

        //Vector2 gridPosition = GetGridPosition(hit.point);
        //FixExtrusion(gridPosition);

        ////if (planetGrid.IsLocatedBuildTiles(focusEntity.Pivot, focusEntity.Edge, EBuilding.BUILDING) == false)
        //if (gridObject.IsEmpty(focusEntity.Pivot, focusEntity.Edge) == false)
        //{
        //    return false;
        //}


        //return true;
    }

    public void RotateStructure()
    {
        if (focusEntity == null)
        {
            return;
        }

        focusEntity.Rotate(90.0f);
    }

    private Vector2 GetGridPosition(Vector3 hitPosition)
    {
        float TrimX = Mathf.Floor(hitPosition.x);
        float TrimZ = Mathf.Ceil(hitPosition.z);

        return new Vector2(TrimX + 0.5f, TrimZ - 0.5f);
    }

    private void FixExtrusion(Vector2 gridPosition)
    {
        if (focusEntity == null)
        {
            return;
        }

        Vector2 fixPosition = new Vector2(0.0f, 0.0f);

        if (gridPosition.x + focusEntity.LocalPivot.x < gridObject.GridRect.x)
        {
            fixPosition -= new Vector2(gridPosition.x + focusEntity.LocalPivot.x - gridObject.GridRect.x, 0.0f);
        }
        if (gridPosition.y + focusEntity.LocalPivot.y > gridObject.GridRect.y)
        {
            fixPosition -= new Vector2(0.0f, gridPosition.y + focusEntity.LocalPivot.y - gridObject.GridRect.y);
        }
        if (gridPosition.x + focusEntity.LocalEdge.x > gridObject.GridRect.width)
        {
            fixPosition -= new Vector2(gridPosition.x + focusEntity.LocalEdge.x - gridObject.GridRect.width, 0.0f);
        }
        if (gridPosition.y + focusEntity.LocalEdge.y < gridObject.GridRect.height)
        {
            fixPosition -= new Vector2(0.0f, gridPosition.y + focusEntity.LocalEdge.y - gridObject.GridRect.height);
        }

        focusEntity.SetCenterPosition(gridPosition + fixPosition);
    }
}