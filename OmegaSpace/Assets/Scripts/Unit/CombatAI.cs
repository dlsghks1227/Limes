using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.Rendering;


public static class WaitTime
{
    private static Dictionary<float, WaitForSeconds> waitTimes = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds GetWaitForSecondOf(float second)
    {
        if (!waitTimes.ContainsKey(second))
            waitTimes.Add(second, new WaitForSeconds(second));
        return waitTimes[second];
    }
}


public class CombatAI : MonoBehaviour
{
    #region variabless
    protected SearchComponent searchComponent;
    protected AttackComponent attackComponent;

    public bool IsAttacking
    {
        protected set;
        get;
    }
    #endregion
    #region user functions
    protected IEnumerator AutoSetAttackTarget()
    {
        while (true)
        {
            var searchedObj = searchComponent.GetSearchedUnit();
            if (!searchedObj)
                searchedObj = searchComponent.GetSearchedBuilding();
            if (searchedObj)
                attackComponent.attackTarget = searchedObj;

            CheckAttackTargetValid();

            yield return WaitTime.GetWaitForSecondOf(0.2f);
        }
    }

    private void CheckAttackTargetValid()
    {
        if (!attackComponent.attackTarget)
            return;
        if ((attackComponent.attackTarget.layer == gameObject.layer) ||
            !attackComponent.attackTarget.activeSelf)
            attackComponent.attackTarget = null;
    }

    protected virtual IEnumerator ActiveCombatMode() { return null; }
    #endregion
}