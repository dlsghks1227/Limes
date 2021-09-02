using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


class ActiveSkillLayout : MonoBehaviour
{
    private PlayerSkillSlot playerSkillSlot;

    private Image previousSkillIcon;
    private Button currentSkillButton;
    private Image nextSkillIcon;
    private Image coolTimeMask;
    private Sprite skillNull;

    private IEnumerator coolTimeCounter;
    private bool isOnCoolTime;

    public int SkillExcutedCnt
    {
        private set;
        get;
    }

    public void SetLayoutComponents(Image prevSkill, Button currentSkillBtn, Image nextSkill, Image coolTimeMask, Sprite skillNull)
    {
        previousSkillIcon = prevSkill;
        currentSkillButton = currentSkillBtn;
        nextSkillIcon = nextSkill;
        this.coolTimeMask = coolTimeMask;
        this.skillNull = skillNull;

        ForceRefreshSkillAllign(); 
    }

    public void OrderRefreshingSkillAllign()
    {
        if (playerSkillSlot.SkillExcutedCnt > SkillExcutedCnt)
        {
            SkillExcutedCnt++;
            ForceRefreshSkillAllign();
        }
    }

    public void ForceRefreshSkillAllign()
    {
        LoadSkillIcons();

        if (!isOnCoolTime)
        {
            coolTimeCounter = StartCoolTimeProcess();
            StartCoroutine(coolTimeCounter);
        }
    }

    private void ChangeSkillIcons()
    {
        if (playerSkillSlot.RetainingActiveSkillCnt <= 0)
        {
            InitSkillIcons();
            return;
        }

        int idx = GetPreviousSkillIdx();
        previousSkillIcon.sprite = playerSkillSlot.ActiveSkillList[idx].Icon;

        idx = playerSkillSlot.currentSkillIdx;
        currentSkillButton.image.sprite = playerSkillSlot.CurrentActiveSkill.Icon;

        idx = GetNextSkillIdx();
        nextSkillIcon.sprite = playerSkillSlot.ActiveSkillList[idx].Icon;
    }

    private void InitSkillIcons()
    {
        if (!skillNull)
        {
            previousSkillIcon.sprite = null;
            currentSkillButton.image.sprite = null;
            nextSkillIcon.sprite = null;
        }
        else
        {
            previousSkillIcon.sprite = skillNull;
            currentSkillButton.image.sprite = skillNull;
            nextSkillIcon.sprite = skillNull;
        }
    }

    private int GetPreviousSkillIdx()
    {
        int previousIdx = playerSkillSlot.currentSkillIdx - 1;
        if (previousIdx < 0)
            return playerSkillSlot.RetainingActiveSkillCnt - 1;
        return previousIdx;
    }

    private int GetNextSkillIdx()
    {
        int nextIdx = playerSkillSlot.currentSkillIdx + 1;
        if (nextIdx >= playerSkillSlot.RetainingActiveSkillCnt && playerSkillSlot.RetainingActiveSkillCnt > 0)
            return 0;
        else if(playerSkillSlot.RetainingActiveSkillCnt < 1)
            return -1;
        return nextIdx;
    }

    private void DecideIconShadeLevel()
    {
        int idx = GetPreviousSkillIdx();
        int skillCnt = playerSkillSlot.RetainingActiveSkillCnt;
        if (idx >= 0 && idx <= skillCnt && playerSkillSlot.ActiveSkillList[idx].IsUsed)
            previousSkillIcon.color = Color.gray;
        else
            previousSkillIcon.color = Color.white;

        idx = GetNextSkillIdx();
        if (idx >= 0 && idx <= skillCnt && playerSkillSlot.ActiveSkillList[idx].IsUsed)
            nextSkillIcon.color = Color.grey;
        else
            nextSkillIcon.color = Color.white;
    }

    private IEnumerator StartCoolTimeProcess()
    {
        if (playerSkillSlot.CurrentActiveSkill == null)
            yield break;
        coolTimeMask.fillAmount = 1;
        float second = 0;
        float coolTime = playerSkillSlot.CurrentActiveSkill.CoolTime;
        isOnCoolTime = true;
        while (second <= coolTime)
        {
            coolTimeMask.fillAmount =  1-(second/coolTime);
            yield return WaitTime.GetWaitForSecondOf(0.25f);
            second += 0.25f;
        }
        isOnCoolTime = false;
    }

    private void StopCoolTimeProcess()
    {
        if (coolTimeCounter == null)
            return;
        isOnCoolTime = false;
        StopCoroutine(coolTimeCounter);
    }

    public void LoadSkillIcons()
    {
        ChangeSkillIcons();
        DecideIconShadeLevel();
    }

    private void Awake()
    {
        playerSkillSlot = PlayerSkillSlot.Instance;
        coolTimeCounter = StartCoolTimeProcess();
        SkillExcutedCnt = 0;
    }
}

