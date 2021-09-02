using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;

[CreateAssetMenu(fileName = "Active Skill Heal Buildings", menuName = "Scriptable Object/Active Skill/Heal Buildings", order = 1)]
public class ActiveSkillHealBuildings : ActiveSkillBase
{
    [SerializeField]
    private int repairAmount = 150;
    [SerializeField]
    private ParticleEffecter particle;

    public override void ExecuteSkill(Vector3 pos)
    {
        var playerObjs = Physics.SphereCastAll(pos, 30, Vector3.one, 0.01f, Layers.PLAYER_UNIT_LAYER);
        foreach (var a in playerObjs)
        {
            if (a.transform.CompareTag("Building"))
                a.collider.gameObject.GetComponent<BuildingEntity>().RepairBuilding(repairAmount);
        }
        particle.SetParticleAs(0);
        particle.ActivateParticle(pos);
    }
}
