using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System;

class ActiveSkillButton 
{
    private PlayerSkillSlot playerSkillSlot;
    private InputSkillState skillState;
    private ActiveSkillLayout skillLayout;
    private PlayerInputManager playerInputManager;

    public void OnCiick()
    {
        if (playerSkillSlot.CurrentActiveSkill && !playerSkillSlot.IsOnCoolDown)
        {
            playerSkillSlot.ExecuteSkill();
            if (playerSkillSlot.CurrentActiveSkill.IsMouseActiveSkill)
                playerInputManager.ChangeState(skillState);
            else
                skillLayout.OrderRefreshingSkillAllign();
        }
    }

    public ActiveSkillButton()
    {
    }

    public ActiveSkillButton(ActiveSkillLayout skillLayout)
    {
        playerSkillSlot = PlayerSkillSlot.Instance;
        playerInputManager = GameObject.FindObjectOfType<PlayerInputManager>();
        skillState = GameObject.FindObjectOfType<InputSkillState>();
        this.skillLayout = skillLayout;
    }

}

