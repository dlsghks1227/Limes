using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputMainState : PlayerInputState
{
    private Vector3 pressPos;
    public void InitialzeInput(InputActionAsset inputPlayerControl)
    {
        
    }

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hitResult;
        Physics.Raycast(ray, out hitResult);
        if (hitResult.collider == null)
            return;
        bool isHitGround = hitResult.collider.name.Equals("Plane") || hitResult.collider.name.Equals("Grid");
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            
            bool isHitPlayerUnit = hitResult.collider.CompareTag("Unit")
                                    && hitResult.transform.gameObject.layer == LayerMask.NameToLayer("Player");

            if (isHitGround || isHitPlayerUnit)
                pressPos = hitResult.point;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            
            var releasePos = hitResult.point;
            UnitContol.Instance.SelectUnits(pressPos, releasePos);
        }

        if (Mouse.current.rightButton.wasPressedThisFrame && isHitGround)
            UnitContol.Instance.MoveAllUnits(hitResult.point);
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }
}
