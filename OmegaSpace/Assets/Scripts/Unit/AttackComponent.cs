using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    #region variables
    [SerializeField]
    protected float attackRange;
    protected float modifiedAttackRange;
    protected float attackRangeCoefficient = 1f;
    [SerializeField]
    protected float attackSpeed;
    protected float attackSpeedCoefficient = 1f;
    protected float modifiedAttackSpeed;
    [SerializeField]
    protected int attackDamage;
    protected float attackDamageCoefficient = 1f;
    protected int modifiedAttackDamage;
    [SerializeField]
    protected float armorPierce = 0;

    public float AttackRange
    {
        get => attackRange;
    }
    public float AttackSpeed
    {
        get => attackSpeed;
    }
    public float ModifiedAttackSpeed
    {
        get => modifiedAttackSpeed;
    }
    public int AttackDamage
    {
        get => attackDamage;
    }
    public int ModifiedAttackDamage
    {
        get => modifiedAttackDamage;
    }
    public float ArmorPierce
    {
        get
        {
            if (armorPierce > 1)
                return 1;
            return armorPierce;
        }
    }

    public GameObject attackTarget;
    protected ObjectSoundManager attackSoundManager = null;
    [SerializeField]
    protected AudioClip attackSound = null;
    #endregion
    #region user functions
    public virtual void Attack(GameObject target)
    {
        if(attackSound && IsInRange(target.transform.position))
            attackSoundManager.ForcePlay(attackSound);
    }
   
    public bool IsInRange(Vector3 target)
    {
        if ((target - transform.position).magnitude <= attackRange)
            return true;
        else
            return false;
    }

    public void InitAttackTarget()
    {
        attackTarget = null;
    }

    public void InitAttackDamage()
    {
        modifiedAttackDamage = (int)(attackDamage*attackDamageCoefficient);
    }

    public void InitAttackSpeed()
    {
        modifiedAttackSpeed = attackSpeed * attackSpeedCoefficient;
    }

    public void InitAttackRange()
    {
        modifiedAttackRange = attackRange * attackRangeCoefficient;
    }

    public void AdjustAttackDamage(float ratio)
    {
        attackDamageCoefficient += ratio;
        modifiedAttackDamage =(int)(attackDamage* attackDamageCoefficient);
    }

    public void AdjustAttackDamage(int value)
    {
        attackDamage += value;
        modifiedAttackDamage += (int)(attackDamage * attackDamageCoefficient);
    }

    public void AdjustAttackRange(float ratio)
    {
        attackRangeCoefficient += ratio;
        modifiedAttackRange = attackRange * attackRangeCoefficient;
    }

    public void AdjustAttackRange(int value)
    {
        attackRange += value;
        modifiedAttackRange = attackRange * attackRangeCoefficient;
    }

    public void AdjustAttackSpeed(float ratio)
    {
        attackSpeedCoefficient += ratio;
        modifiedAttackSpeed = attackSpeed * attackSpeedCoefficient;
    }

    public void AdjustAttackSpeed(int value)
    {
        attackSpeed += value;
        modifiedAttackSpeed = attackSpeed * attackSpeedCoefficient;
    }

    public void AdjustArmorPierce(float value)
    {
        armorPierce += value;
    }
    #endregion

    protected virtual void OnEnable()
    {
        InitAttackDamage();
        InitAttackSpeed();
        InitAttackRange();
        attackSoundManager = GetComponent<ObjectSoundManager>();
        //if (!attackSoundManager.audioSource)
        //    attackSoundManager.audioSource = GetComponent<AudioSource>();
    }
}
