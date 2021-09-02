using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 지뢰 / 찌리리 두개로 가능
public class Landmine : BuildingEntity, ILevelInterface
{
    private Collider[] attacked;
    private UnitEntity[] unitStats;

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        attacked = Physics.OverlapSphere(transform.position, 2);
        unitStats = new UnitEntity[attacked.Length];
        for (int i =0; i < attacked.Length; i++)
        {
            Debug.Log(attacked[i].name);
            unitStats[i] = attacked[i].GetComponent<UnitEntity>();
            unitStats[i].TakeDamage(50);
        }
    }

    public void LevelUp(int amount)
    {
        level.Value += amount;
    }

    public override void Destroyed()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        attacked = Physics.OverlapSphere(transform.position, 2);
        unitStats = new UnitEntity[attacked.Length];
        for (int i = 0; i < attacked.Length; i++)
        {
            Debug.Log(attacked[i].name);
            unitStats[i] = attacked[i].GetComponent<UnitEntity>();
            unitStats[i].TakeDamage(50);
        }
    }
}