using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Active Skill Slowen Enemies", menuName = "Scriptable Object/Active Skill/Slowen Enemies", order = 1)]
public class ActiveSkillSlowenEnemies : ActiveSkillBase
{
    [SerializeField]
    private ParticleEffecter particle;
    [SerializeField]
    private float slowRatio = -1f;
    public override void ExecuteSkill(Vector3 pos)
    {
        var enemys = Physics.SphereCastAll(pos, 30, Vector3.one, 0.01f, Layers.ENEMY_UNIT_LAYER);
        foreach (var unit in enemys)
        {
            if (!unit.collider.CompareTag("Unit"))
                continue;
            var entity = unit.collider.GetComponent<MovementComponent>();

            if (entity)
                entity.AdjustSpeed(slowRatio);
        }
        particle.SetParticleAs(0);
        particle.ActivateParticle(pos);
    }
}
