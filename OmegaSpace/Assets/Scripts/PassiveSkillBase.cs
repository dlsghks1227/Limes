using System.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;


public class PassiveSkillBase : ScriptableObject
{
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private string skillName;
    [SerializeField]
    private string skillDescribe;
    public Sprite Icon
    {
        get => icon;
    }
    public bool HasPerformed
    {
        private set;
        get;
    }
    public string SkillName
    {
        get => skillName;
    }
    public string SkillDescribe
    {
        get => skillDescribe;
    }

    [SerializeField]
    protected ResourceAmount cost = new ResourceAmount(EResource.RES_ETHER, 0);
    public ResourceAmount Cost
    {
        get => cost;
    }

    public virtual void ApplyPassiveSkillEffect()
    {
        if (HasPerformed)
            return;
        HasPerformed = true;
    }
}
