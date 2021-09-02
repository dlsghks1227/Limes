using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputBuildState : PlayerInputState
{
    [SerializeField]
    private BuildMenuLayout buildLayout = null;

    [SerializeField] private CanvasGroup buildCanvas = null;

    private PlayerInputManager manager;
    private InputMainState mainState;

    [SerializeField] private Grid grid = null;

    private void Awake()
    {
        manager = GetComponent<PlayerInputManager>();
        mainState = GetComponent<InputMainState>();

        buildLayout.Initialize(mainState, manager, grid);

        buildCanvas.alpha = 0;
        buildCanvas.blocksRaycasts = false;
    }

    public void InitializeInput(InputActionAsset inputPlayerControl)
    {
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        buildCanvas.alpha = 1;
        buildCanvas.blocksRaycasts = true;
    }

    public override void OnExitState()
    {
        base.OnExitState();

        buildCanvas.alpha = 0;
        buildCanvas.blocksRaycasts = false;
    }
}
