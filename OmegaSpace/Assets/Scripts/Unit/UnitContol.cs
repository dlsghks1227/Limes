using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitContol : Singleton<UnitContol> 
{
    #region variables
    private List<GameObject> selectedUnits = new List<GameObject>();
    [SerializeField]
    private string unitTag = "Unit";
    #endregion
    #region user functions 
    public void SelectUnits(Vector3 firstHitPos, Vector3 secondHitPos)
    {
        Vector3 dragBoxCenter = GetDragBoxCenter(firstHitPos, secondHitPos);
        Vector3 dragBoxSize = GetDragBoxSize(firstHitPos, secondHitPos);
       
        selectedUnits.Clear();
        foreach (var a in Physics.BoxCastAll(dragBoxCenter, dragBoxSize, Vector3.one, Quaternion.identity, 0.1f, Layers.PLAYER_UNIT_LAYER))
            if (a.collider.CompareTag(unitTag) && !selectedUnits.Contains(a.collider.gameObject))
                selectedUnits.Add(a.collider.gameObject);
    } 

    public void MoveAllUnits(Vector3 pos)
    {
        foreach (var a in selectedUnits)
            a.GetComponent<MovementComponent>().Move(pos, true);
    }

    private Vector3 GetDragBoxCenter(Vector3 firstHitPos,Vector3 secondHitPos)
    {
        Vector3 dragBoxCenter = Vector3.zero;

        dragBoxCenter.x = (firstHitPos.x + secondHitPos.x) / 2;
        dragBoxCenter.y = firstHitPos.y + 10f;
        dragBoxCenter.z = (firstHitPos.z + secondHitPos.z) / 2;

        return dragBoxCenter;
    }
    private Vector3 GetDragBoxSize(Vector3 firstHitPos, Vector3 secondHitPos)
    {
        Vector3 dragBoxSize = Vector3.zero;

        dragBoxSize.x = Mathf.Abs(firstHitPos.x - secondHitPos.x);
        dragBoxSize.y = 20f;
        dragBoxSize.z = Mathf.Abs(firstHitPos.z - secondHitPos.z);

        return dragBoxSize;
    }
    #endregion. 
}
    