using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;



class PlayerSkillSlot : MonoSingleton<PlayerSkillSlot>
{
    [SerializeField]
    private ActiveSkillBase[] activeSkillArray = new ActiveSkillBase[10];
    private List<PassiveSkillBase> passiveSkillList = new List<PassiveSkillBase>(10);
    private IEnumerator coolTimeCounter;
    private bool isReadyToMouseOrder;
    public int currentSkillIdx
    {
        private set;
        get;
    }

    public ReadOnlyArray<ActiveSkillBase> ActiveSkillList
    {
        get => activeSkillArray;
    }
    public ReadOnlyCollection<PassiveSkillBase> PassiveSkillList
    {
        get => passiveSkillList.AsReadOnly();
    }

    public int RetainingActiveSkillCnt
    {
        get
        {
            int i = 0;
            for (; i < activeSkillArray.Length && activeSkillArray[i] != null; i++)
                continue;
            return i;
        }
    }
    public int RetainingPassiveSkillCnt
    {
        get => passiveSkillList.Count;
    }
    public bool IsOnCoolDown
    {
        private set;
        get;
    }
    public int SkillExcutedCnt
    {
        private set;
        get;
    }
    public ActiveSkillBase CurrentActiveSkill
    {
        get
        {
            if (currentSkillIdx < 0)
                return null;
            return activeSkillArray[currentSkillIdx];
        }
    }

    public void ExecuteSkill()
    {
        ExecuteSkill(Vector3.zero);
    }

    public void ExecuteSkill(Vector3 pos)
    {
        if (RetainingActiveSkillCnt <= 0 || IsOnCoolDown)
            return;

        var skill = activeSkillArray[currentSkillIdx];
        if (skill.IsMouseActiveSkill)
        {
            if (isReadyToMouseOrder)
            {
                skill.ExecuteSkill(pos);
                isReadyToMouseOrder = false;
            }
            else
            {
                isReadyToMouseOrder = true;
                return;
            }
        }
        else
            skill.ExecuteSkill();

        SkillExcutedCnt++;

        coolTimeCounter = MoveNextSkill();
        StartCoroutine(coolTimeCounter);
    }

    private IEnumerator MoveNextSkill()
    {
        IsOnCoolDown = true;

        if (++currentSkillIdx >= RetainingActiveSkillCnt)
        {
            ShuffleActiveSkillQueue();
            currentSkillIdx = 0;
        }

        float coolTime = activeSkillArray[currentSkillIdx].CoolTime;

        yield return WaitTime.GetWaitForSecondOf(coolTime);
        IsOnCoolDown = false;
    }

    private void ShuffleActiveSkillQueue()
    {
        currentSkillIdx = 0;
        int i = 0;
        EnableAllActiveSkill();

        int currentSkillCnt = RetainingActiveSkillCnt;
        for (i = 0; i < currentSkillCnt && activeSkillArray[i] != null; i++)
        {
            var temp = activeSkillArray[i];
            int randomIdx = Random.Range(0, currentSkillCnt);
            activeSkillArray[i] = activeSkillArray[randomIdx];
            activeSkillArray[randomIdx] = temp;
        }
    }

    private void EnableAllActiveSkill()
    {
        int i = 0;
        for (; i < activeSkillArray.Length && activeSkillArray[i] != null; i++)
            activeSkillArray[i].EnableAvailiablity();
    }

    public void AddSkill(ActiveSkillBase skill)
    {
        if (RetainingActiveSkillCnt < 1) 
        {
            activeSkillArray[0] = skill;
            coolTimeCounter = MoveNextSkill();
            StartCoroutine(coolTimeCounter);
            return;
        }

        int currentSkillCnt = RetainingActiveSkillCnt;
        if (currentSkillCnt < activeSkillArray.Length - 1 && !IsContain(skill))
            activeSkillArray[currentSkillCnt] = skill;
    }

    public void AddSkill(PassiveSkillBase skill)
    {
        if (!passiveSkillList.Contains(skill))
        {
            passiveSkillList.Add(skill);
            skill.ApplyPassiveSkillEffect();
        }
    }

    public bool IsContain(ActiveSkillBase skill)
    {
        int i = 0;
        for (; IsValidIdx(i); i++)
            if (activeSkillArray[i] == skill)
                return true;
        return false;
    }

    public bool IsContain(PassiveSkillBase skill)
    {
        return passiveSkillList.Contains(skill);
    }

    private bool IsValidIdx(int i)
    {
        return i < activeSkillArray.Length && activeSkillArray[i] != null;
    }

    private void OnEnable()
    {
        currentSkillIdx = -1;
        SkillExcutedCnt = 0;
        if (activeSkillArray[0] != null)
        {
            EnableAllActiveSkill();
            StartCoroutine(MoveNextSkill());
        }
        if (passiveSkillList.Count != 0)
            foreach (var a in passiveSkillList)
                a.ApplyPassiveSkillEffect();
    }
}
