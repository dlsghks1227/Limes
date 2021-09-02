using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactShopLayout : LayoutBase
{
    [SerializeField]
    private Button buttonClose = null;
    [SerializeField]
    private Button buttonBuy = null;
    [SerializeField]
    private Text textSkillDesribe = null;

    private ActiveSkillBase currentSelectedActiveSkill = null;
    private PassiveSkillBase currentSelectedPassiveSkill = null;
    private bool isChooseeActiveSkill = false;

    private PlayerSkillSlot playerSkillSlot;
    private ActiveSkillLayout skillLayout;

    public void SelectSkill(ActiveSkillBase skill)
    {
        currentSelectedActiveSkill = skill;
        if(textSkillDesribe)
           textSkillDesribe.text = currentSelectedActiveSkill.SkillDescribe;
        
        isChooseeActiveSkill = true;
    }
    public void SelectSkill(PassiveSkillBase skill)
    {
        currentSelectedPassiveSkill = skill;
        textSkillDesribe.text = currentSelectedPassiveSkill.SkillDescribe;
        isChooseeActiveSkill = false;
    }

    public void Close()
    {
        SetVisible(false);
    }
    
    public void Cancel()
    {
        currentSelectedActiveSkill = null;
        currentSelectedPassiveSkill = null;
    }

    public void Purchase()
    {
        if (isChooseeActiveSkill)
        {
            bool playerHasSkill = playerSkillSlot.IsContain(currentSelectedActiveSkill);
            bool isPurchable = PlayerDataManager.Instance.IsAffordableToPay(currentSelectedActiveSkill.Cost);
            if (!playerHasSkill && isPurchable)
            {
                playerSkillSlot.AddSkill(currentSelectedActiveSkill);
                skillLayout.ForceRefreshSkillAllign();
            }
        }
        else
        {
            bool isPurchable = PlayerDataManager.Instance.IsAffordableToPay(currentSelectedPassiveSkill.Cost);
            if (isPurchable)
                playerSkillSlot.AddSkill(currentSelectedPassiveSkill);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (buttonBuy != null)
            buttonBuy.onClick.AddListener(Purchase);
        if (buttonClose != null)
            buttonClose.onClick.AddListener(Close);

        playerSkillSlot = PlayerSkillSlot.Instance;
        skillLayout = FindObjectOfType<ActiveSkillLayout>();
    }
}
