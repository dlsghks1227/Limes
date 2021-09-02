using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputUnitState : PlayerInputState
{
    [SerializeField] private CanvasGroup unitCanvas = null;

    public void Awake()
    {
        unitCanvas.alpha = 0;
        unitCanvas.blocksRaycasts = false;
    }

    public void InitializeInput(InputActionAsset inputPlayerControl)
    {
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        unitCanvas.GetComponent<UnitLayout>().SetBarrack();
        unitCanvas.alpha = 1;
        unitCanvas.blocksRaycasts = true;
    }

    public override void OnExitState()
    {
        base.OnExitState();

        unitCanvas.alpha = 0;
        unitCanvas.blocksRaycasts = false;
    }

}