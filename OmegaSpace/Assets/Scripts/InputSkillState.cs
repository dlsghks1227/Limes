using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputSkillState : PlayerInputState
{
    private PlayerSkillSlot playerSkillSlot;
    private ActiveSkillLayout skillLayout;
    private PlayerInputManager inputManager;
    private InputAction skillPos;

    public void InitializeInput(InputActionAsset inputPlayerControl)
    {
        skillPos = inputPlayerControl.FindActionMap("SkillState").FindAction("GetSkillPos");
        skillPos.performed += ExcuteSkillOnPos;
    }

    public void ExcuteSkillOnPos(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            playerSkillSlot.ExecuteSkill(hit.point);
            inputManager.ChangeState(GetComponent<InputMainState>());
            skillLayout.OrderRefreshingSkillAllign();

            OnExitState();
        }
    }

    public void Awake()
    {
        playerSkillSlot = PlayerSkillSlot.Instance;
        inputManager = FindObjectOfType<PlayerInputManager>();
        skillLayout = FindObjectOfType<ActiveSkillLayout>();
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        skillPos.Enable();
    }

    public override void OnExitState()
    {
        base.OnExitState();

        skillPos.Disable();
    }
}

