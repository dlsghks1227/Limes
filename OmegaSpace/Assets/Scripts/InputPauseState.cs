using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPauseState : PlayerInputState
{
    [SerializeField] private CanvasGroup pauseCanvas;

    private void Awake()
    {
        pauseCanvas.alpha = 0;
        pauseCanvas.blocksRaycasts = false;
    }

    public void InitializeInput(InputActionAsset inputPlayerControl)
    {

    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        pauseCanvas.alpha = 1;
        pauseCanvas.blocksRaycasts = true;
    }

    public override void OnExitState()
    {
        base.OnExitState();

        pauseCanvas.alpha = 0;
        pauseCanvas.blocksRaycasts = false;
    }
}