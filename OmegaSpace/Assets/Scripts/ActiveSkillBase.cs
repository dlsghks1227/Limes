using System.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;


public class ActiveSkillBase : ScriptableObject
{
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private string skillName;
    [SerializeField]
    private string skillDescribe;
    [SerializeField]
    protected float coolTime = 1f;
    [SerializeField]
    protected bool isMouseActiveSkill;
    [SerializeField]
    protected bool isUsed = false;
    public float CoolTime
    {
        get => coolTime;
    }
    public Sprite Icon
    {
        get => icon;
    }
    public string SkillName
    {
        get => skillName;
    }
    public string SkillDescribe
    {
        get => skillDescribe;
    }
    public bool IsUsed
    {
        get=> isUsed;
    }
    public bool IsMouseActiveSkill
    {
        get => isMouseActiveSkill;
    }

    [SerializeField]
    protected ResourceAmount cost = new ResourceAmount(EResource.RES_ETHER, 0);
    public ResourceAmount Cost
    {
        get => cost;
    }

    public void EnableAvailiablity()
    {
        isUsed = false;
    }

    public virtual void ExecuteSkill(Vector3 pos)
    {

    }

    public virtual void  ExecuteSkill()
    {

    }
}

