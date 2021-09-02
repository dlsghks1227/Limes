using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTechState : PlayerInputState
{
    [SerializeField] private CanvasGroup techCanvas = null;

    private void Awake()
    {
        techCanvas.alpha = 0;
        techCanvas.blocksRaycasts = false;
    }

    public void InitializeInput(InputActionAsset inputPlayerControl)
    {
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        techCanvas.alpha = 1;
        techCanvas.blocksRaycasts = true;
    }

    public override void OnExitState()
    {
        base.OnExitState();

        techCanvas.alpha = 0;
        techCanvas.blocksRaycasts = false;
    }
}
