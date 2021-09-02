using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Active Skill Damage Enemies", menuName = "Scriptable Object/Active Skill/Damage Enemies", order = 1)]
public class ActiveSkillDamageUnits : ActiveSkillBase
{
    [SerializeField]
    private ParticleEffecter particle;
    public override void ExecuteSkill(Vector3 pos)
    {
        var enemys = Physics.SphereCastAll(pos, 30, Vector3.one , 0.01f, Layers.ENEMY_UNIT_LAYER);
        foreach(var unit in enemys)
        {
            if (!unit.collider.CompareTag("Unit"))
                continue;
            var entity = unit.collider.GetComponent<UnitEntity>();

            if(entity)
               entity.TakeDamage(10000,1);
        }
        particle.SetParticleAs(0);
        particle.ActivateParticle(pos);
    }
}
