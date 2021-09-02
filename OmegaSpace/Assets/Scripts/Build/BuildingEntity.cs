using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBuilding
{
    NONE,
    ORE,
    BUILDING,
    COMMAND,
    BARRACK,
    STORAGE,
    BARRICADE,
    PLANETSTORE,
    MINER,
    AIRBARRACK,
}

public struct Level
{
    public Level(int value)
    {
        if (value < 0)
            value = 0;
        level = value;
    }

    private int level;
    public int Value
    {
        get => level;
        set
        {
            if (value < 0)
                value = 0;
            level = value;
        }
    }
}

public interface ILevelInterface
{
    void LevelUp(int amount);
}

public class BuildingEntity : GridEntity
{
    [SerializeField]
    private EBuilding buildingType = EBuilding.NONE;
    protected ETerritory territoryType = ETerritory.NONE;
    protected Level level;

    [SerializeField]
    protected UnitStats stats = new UnitStats(0,0);

    [SerializeField]
    protected HealthBar hpSlider;

    [SerializeField]
    private int territory = 0;
    [SerializeField]
    private ParticleEffecter destroyingParticleActor;

    [SerializeField] private List<Material> defaultMaterials = new List<Material>();
    [SerializeField] private List<Material> createMeterials = new List<Material>();
    [SerializeField] private List<Material> deleteMeterials = new List<Material>();
    private MeshRenderer        currentMeshRenderer = null;
    private SkinnedMeshRenderer currentSkinnedMeshRenderer = null;
    private bool isWorkingMaterials { get; set; }

    public int HealthPoint
    {
        get => stats.HealthPoint;
    }
    public int HealthPointMax
    {
        get => stats.HealthPointMax;
    }
    public int ArmorPoint
    {
        get => stats.ArmorPoint;
    }
    public int ArmorPointMax
    {
        get => stats.ArmorPointMax;
    }

    public int Level
    {
        get => level.Value;
    }

    public void Initialize(Vector2 size)
    {
        Size = size;
    }

    public int Territory { get => territory; set => territory = value; }

    public void AdjustHealthPoint(float ratio)
    {
        stats.AdjustHealthPoint(ratio);
    }

    public void AdjustHealthPoint(int value)
    {
        stats.AdjustHealthPoint(value);
    }

    public void AdjustArmorPoint(float ratio)
    {
        stats.AdjustArmorPoint(ratio);
    }

    public void AdjustArmorPoint(int value)
    {
        stats.AdjustArmorPoint(value);
    }

    public void TakeDamage(int damage, float armorPierce = 0, GameObject attacker = null, bool isReflection = false)
    {
        stats.TakeDamage(damage, armorPierce, attacker, isReflection);
        if (hpSlider)
        {
            //hpSlider.SetPosition(gameObject.transform.position);
            hpSlider.SetHP(HealthPoint, HealthPointMax, 3.0f);
        }
        if (stats.IsDead)
        {
            if (hpSlider) hpSlider.Destroy();
            PlayerDeleteBuilding(1.0f);
        }
    }

    public void PlayCreateBuilding(float time)
    {
        if (gameObject.GetComponent<MeshRenderer>())
        {
            currentMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        }
        else if (gameObject.GetComponent<SkinnedMeshRenderer>())
        {
            currentSkinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
        }
        else
        {
            return;
        }
        if (isWorkingMaterials == false && (createMeterials.Count > 0) && gameObject.activeSelf)
        {
            StartCoroutine(CreateBuilding(time));
        }
    }

    public void PlayerDeleteBuilding(float time)
    {
        if (gameObject.GetComponent<MeshRenderer>())
        {
            currentMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        }
        else if (gameObject.GetComponent<SkinnedMeshRenderer>())
        {
            currentSkinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
        }
        else
        {
            return;
        }

        if (isWorkingMaterials == false && (deleteMeterials.Count > 0) && gameObject.activeSelf)
        {
            StartCoroutine(DeleteBuilding(time));
        }
    }

    IEnumerator CreateBuilding(float time)
    {
        List<Material> materials = new List<Material>();
        for (int i = 0; i < createMeterials.Count; i++)
        {
            if (createMeterials[i] != default)
            {
                materials.Add(createMeterials[i]);
                if (createMeterials[i].HasProperty("Time"))
                {
                    createMeterials[i].SetFloat("Time", 0.0f);
                }
            }
            else
            {
                materials.Add(defaultMaterials[i]);
            }
        }

        if (currentMeshRenderer)
        {
            currentMeshRenderer.materials = materials.ToArray();
        }
        else if (currentSkinnedMeshRenderer)
        {
            currentSkinnedMeshRenderer.materials = materials.ToArray();
        }
        else
        {
            yield return null;
        }

        isWorkingMaterials = true;

        for (float i = 0.01f; i < 1.0f; i += (0.02f / time))
        {
            for (int j = 0; j < createMeterials.Count; j++)
            {
                if (currentMeshRenderer)
                {
                    if (currentMeshRenderer.materials[j].HasProperty("Time") == true)
                    {
                        currentMeshRenderer.materials[j].SetFloat("Time", 0.6f * i);
                    }
                }
                else if (currentSkinnedMeshRenderer)
                {
                    if (currentSkinnedMeshRenderer.materials[j].HasProperty("Time") == true)
                    {
                        currentSkinnedMeshRenderer.materials[j].SetFloat("Time", 0.6f * i);
                    }
                }
            }
            if (hpSlider != null)
            {
                hpSlider.SetHP(i / 0.6f, 1.0f, time);
            }
            yield return null;
        }
        isWorkingMaterials = false;

        if (currentMeshRenderer)
        {
            currentMeshRenderer.materials = defaultMaterials.ToArray();
        }
        else if (currentSkinnedMeshRenderer)
        {
            currentSkinnedMeshRenderer.materials = defaultMaterials.ToArray();
        }
        else
        {
            yield return null;
        }
    }

    IEnumerator DeleteBuilding(float time)
    {
        destroyingParticleActor.SetParticleAs(0);
        destroyingParticleActor.ActivateParticle(transform.position);

        List<Material> materials = new List<Material>();
        for (int i = 0; i < deleteMeterials.Count; i++)
        {
            if (deleteMeterials[i] != default)
            {
                materials.Add(deleteMeterials[i]);
                if (deleteMeterials[i].HasProperty("Time"))
                {
                    deleteMeterials[i].SetFloat("Time", 0.0f);
                }
            }
            else
            {
                materials.Add(defaultMaterials[i]);
            }
        }

        if (currentMeshRenderer)
        {
            currentMeshRenderer.materials = materials.ToArray();
        }
        else if (currentSkinnedMeshRenderer)
        {
            currentSkinnedMeshRenderer.materials = materials.ToArray();
        }
        else
        {
            yield return null;
        }

        isWorkingMaterials = true;

        for (float i = 0.01f; i < 1.0f; i += (0.01f / time))
        {
            for (int j = 0; j < deleteMeterials.Count; j++)
            {
                if (currentMeshRenderer)
                {
                    if (currentMeshRenderer.materials[j].HasProperty("Time") == true)
                    {
                        currentMeshRenderer.materials[j].SetFloat("Time", i);
                    }
                }
                else if (currentSkinnedMeshRenderer)
                {
                    if (currentSkinnedMeshRenderer.materials[j].HasProperty("Time") == true)
                    {
                        currentSkinnedMeshRenderer.materials[j].SetFloat("Time", i);
                    }
                }
            }
            yield return null;
        }

        isWorkingMaterials = false;

        var originalLayer = gameObject.layer;
        gameObject.layer = 0;
        gameObject.layer = originalLayer;
        destroyingParticleActor.DeactiveParticleObject();


        gameObject.SetActive(false);

        GameplayManager.Instance.CheckCommandCount();
        //if (currentMeshRenderer)
        //{
        //    currentMeshRenderer.materials = defaultMaterials.ToArray();
        //}
        //else if (currentSkinnedMeshRenderer)
        //{
        //    currentSkinnedMeshRenderer.materials = defaultMaterials.ToArray();
        //}
        //else
        //{
        //    yield return null;
        //}
    }

    private IEnumerator StartDestroying()
    {
        destroyingParticleActor.SetParticleAs(0);
        destroyingParticleActor.ActivateParticle(transform.position);

        yield return new WaitForSeconds(1.0f);

        PlayerDeleteBuilding(1.0f);
    }

    public void RepairBuilding(int heal)
    {
        stats.TakeHeal(heal);
    }

    public bool isDestroy()
    {
        return stats.IsDead;
    }

    public virtual void Destroyed() { }

    public EBuilding GetBuildingType()
    {
        return buildingType;
    }

    public ETerritory GetTerritoryType()
    {
        return territoryType;
    }

    public void EnableDamageReflection()
    {
        stats = new UnitStatReflectDamage(stats.HealthPointMax, stats.HealthPointMax);
    }

    public void InitStatsAsZero()
    {
        stats.InitStatsAsZero();
    }

    public void InitStats()
    {
        stats.InitStats();
    }
}