using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputExchangeState : PlayerInputState
{
    [SerializeField] private CanvasGroup exchangeCanvas = null;

    private void Awake()
    {
        exchangeCanvas.alpha = 0;
        exchangeCanvas.blocksRaycasts = false;
    }

    public void InitializeInput(InputActionAsset inputPlayerControl)
    {
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        exchangeCanvas.alpha = 1;
        exchangeCanvas.blocksRaycasts = true;
    }

    public override void OnExitState()
    {
        base.OnExitState();

        exchangeCanvas.alpha = 0;
        exchangeCanvas.blocksRaycasts = false;
    }
}
