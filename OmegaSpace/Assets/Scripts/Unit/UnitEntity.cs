using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EUnitID
{
    MARINE,
    FIREMAN,
    SHIELDMAN,
    ARTILLERY,
    CALVARY,
    PIERCER,
    MEDIC,
    ENGINEER,
    SNIPER,
    PUSHER,
    ASSAULT,
    ASSASSIN,
    DEBUFFER,
    ANTIARMOR
}

[Serializable]
public class UnitStats
{
    [SerializeField]
    protected int baseHealthPointMax;
    protected int healthPointMax;
    [SerializeField]
    protected int healthPoint;
    protected float healthPointCoefficient = 1f;
    [SerializeField]
    protected int baseArmorPointMax;
    protected int armorPointMax;
    [SerializeField]
    protected int armorPoint;
    protected float armorPointCoefficient = 1f;
    public int ArmorPointMax
    {
        get => armorPointMax;
    }
    public int HealthPoint
    {
        get => healthPoint;
    }
    public int HealthPointMax
    {
        get => healthPointMax;
    }
    public int ArmorPoint
    {
        get => armorPoint;
    }
    public bool IsDead
    {
        get
        {
            if (healthPoint <= 0)
                return true;
            else
                return false;
        }
    }

    public UnitStats(int maxHP, int maxArmor)
    {
        baseHealthPointMax = maxHP;
        baseArmorPointMax = maxArmor;
        InitStats();    
    }

    #region user function
    public void InitStats()
    {
        healthPointMax = (int)(baseHealthPointMax * healthPointCoefficient);
        armorPointMax = (int)(baseArmorPointMax * armorPointCoefficient);
        healthPoint = healthPointMax;
        armorPoint = armorPointMax;
    }

    public void InitStatsAsZero()
    {
        healthPointMax = (int)(baseHealthPointMax * healthPointCoefficient);
        armorPointMax = (int)(baseArmorPointMax * armorPointCoefficient);
        healthPoint = 0;
        armorPoint = 0;
    }

    public virtual void TakeDamage(int damage, float armorPierce = 0,GameObject attacker = null, bool IsRefeltion = false)
    {
        if (armorPoint > 0)
        {
            GetReductuionDamage(damage, armorPierce);
            return;
        }
        else
            healthPoint -= damage;
    }

    protected void GetReductuionDamage(int damage, float armorPierce = 0)
    {
        if (damage < armorPoint)
        {
            armorPoint -= damage;
            healthPoint -= (int)(damage * armorPierce);
        }
        else
        {
            int leftDamage = damage - armorPoint;
            armorPoint = 0;
            healthPoint -= leftDamage;
        }
    }

    public virtual void TakeHeal(int heal)
    {
        int leftHP = healthPointMax - healthPoint;
        if (leftHP != 0)
        {
            if (leftHP < heal)
            {
                healthPoint = healthPointMax;
                heal -= leftHP;
            }
            else
            {
                healthPoint += heal;
                return;
            }
        }
        float leftArmor = armorPointMax - armorPoint;
        if (leftArmor != 0)
        {
            if (leftArmor < heal)
                armorPoint = armorPointMax;
            else
                armorPoint += heal;
        }
    }

    public void AdjustHealthPoint(float ratio)
    {
        float presntHPratioByMaxHP = healthPoint / healthPointMax;
      
        healthPointCoefficient += ratio;
        healthPointMax = (int)(baseHealthPointMax * healthPointCoefficient);
        healthPoint = (int)(healthPointMax * presntHPratioByMaxHP);
    }

    public void AdjustHealthPoint(int value)
    {
        float presntHPratioByMaxHP = healthPoint / healthPointMax;

        baseHealthPointMax += value;
        healthPointMax = (int)(baseHealthPointMax * healthPointCoefficient);
        healthPoint = (int)(healthPointMax * presntHPratioByMaxHP);
    }

    public void AdjustArmorPoint(float ratio)
    {
        float presntArmorRatioByMaxArmor = armorPoint / armorPointMax;

        armorPointCoefficient += ratio;
        armorPointMax = (int)(baseArmorPointMax * armorPointCoefficient);
        armorPoint = (int)(armorPointMax * presntArmorRatioByMaxArmor);
    }

    public void AdjustArmorPoint(int value)
    {
        float presntArmorRatioByMaxArmor = armorPoint / armorPointMax;

        baseArmorPointMax += value;
        armorPointMax = (int)(baseArmorPointMax * armorPointCoefficient);
        armorPoint = (int)(armorPointMax * presntArmorRatioByMaxArmor);
    }
    #endregion
}

public class UnitStatReflectDamage : UnitStats
{
    [SerializeField]
    private float damageRefelctRatio = 0.1f;
    public UnitStatReflectDamage(int maxHP, int maxArmor) : base(maxHP, maxArmor) { }

    public override void TakeDamage(int damage, float armorPierce = 0, GameObject attacker = null, bool IsRefeltion = false)
    {
        base.TakeDamage(damage, armorPierce, attacker);
        if (!IsRefeltion && attacker)
            ReflectDamage(damage, attacker);
    }

    private void ReflectDamage(int damage, GameObject attacker)
    {
        if (attacker.CompareTag("Unit"))
            attacker.GetComponent<UnitStats>().TakeDamage((int)(damage * damageRefelctRatio), 0, null, true);
        else
            attacker.GetComponent<BuildingEntity>().TakeDamage((int)(damage * damageRefelctRatio), 0, null, true);
    }
}

public class UnitEntity : MonoBehaviour
{
    #region variables
    [SerializeField]
    private UnitStats unitStat= new UnitStats(0,0);
    [SerializeField]
    private EUnitID unitID;
    [SerializeField]
    private ParticleEffecter dyingParticleActor;
    [SerializeField]
    private String explain = null;

    [SerializeField] private Material playerMaterial;
    [SerializeField] private Material enemyMaterial;

    public int HealthPoint
    {
        get=> unitStat.HealthPoint;
    }
    public int ArmorPoint
    {
        get => unitStat.ArmorPoint;
    }
    public int HealthPointMax
    {
        get => unitStat.HealthPointMax;
    }
    public int ArmorPointMax
    {
        get => unitStat.ArmorPointMax;
    }
    public EUnitID UnitID
    {
        get => unitID;
    }
    public string Explain { get => explain; }
    #endregion
    #region user function
    public void TakeDamage(int damage, float armorPierce = 0,GameObject attacker=null, bool isReflection = false)
    {
        unitStat.TakeDamage(damage, armorPierce,attacker,isReflection);
        if (unitStat.IsDead)
            StartCoroutine(StartDyingAnimation());
    }

    private IEnumerator StartDyingAnimation()
    {
        Vector3 scaleDownPerDeciSec = transform.localScale / 5f;
        float second = 0;
        var originalLayer = gameObject.layer;
        gameObject.layer = 0;
        while (second < 0.5)
        {
            transform.localScale -= scaleDownPerDeciSec;
            yield return WaitTime.GetWaitForSecondOf(0.1f);
            second += 0.1f;
        }
       
        dyingParticleActor.SetParticleAs(0);
        dyingParticleActor.ActivateParticle(transform.position);
        
        yield return WaitTime.GetWaitForSecondOf(0.5f);
       

        gameObject.layer = originalLayer;
        transform.localScale = scaleDownPerDeciSec * 5f;
        dyingParticleActor.DeactiveParticleObject();
        gameObject.SetActive(false);
    }

    public void SetMaterial(bool isPlayer)
    {
        if (isPlayer == true)
        {
            if (gameObject.GetComponent<MeshRenderer>())
            {
                gameObject.GetComponent<MeshRenderer>().material = playerMaterial;
            }
            else if(gameObject.GetComponent<SkinnedMeshRenderer>())
            {
                gameObject.GetComponent<SkinnedMeshRenderer>().material = playerMaterial;
            }
        }
        else
        {
            if (gameObject.GetComponent<MeshRenderer>())
            {
                gameObject.GetComponent<MeshRenderer>().material = enemyMaterial;
            }
            else if (gameObject.GetComponent<SkinnedMeshRenderer>())
            {
                gameObject.GetComponent<SkinnedMeshRenderer>().material = enemyMaterial;
            }
        }
    }

    public void TakeHeal(int heal)
    {
        unitStat.TakeHeal(heal);
    }

    public void AdjustHealthPoint(int value)
    {
        unitStat.AdjustHealthPoint(value);
    }

    public void AdjustHealthPoint(float ratio)
    {
        unitStat.AdjustHealthPoint(ratio);
    }

    public void AdjustArmorPoint(int value)
    {
        unitStat.AdjustArmorPoint(value);
    }

    public void AdjustArmorPoint(float ratio)
    {
        unitStat.AdjustArmorPoint(ratio);
    }
    #endregion
    private void OnEnable()
    {
        unitStat.InitStats();
    }
}

