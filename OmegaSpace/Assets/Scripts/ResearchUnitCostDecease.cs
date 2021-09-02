using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Research Unit Cost Decrease", menuName = "Scriptable Object/Research/Unit Cost Decrease", order = 1)]
public class ResearchUnitCostDecease : ResearchBase
{
    [SerializeField]
    private UnitData[] specificUnits = new UnitData[10];

    [SerializeField]
    private float ironDeceaseRatio = 0;
    [SerializeField]
    private float goldDeceaseRatio = 0;

    [SerializeField]
    private int ironDeceasesValue = 0;
    [SerializeField]
    private int goldDeceasesValue = 0;

    public override void DoResearch()
    {
        bool isTargetSpecificUnits = specificUnits[0] != null;
        if (isTargetSpecificUnits)
        {
            for (int i = 0; specificUnits[i]; i++)
            {
                TechManager.Instance.AdjustUnitIronCost(-ironDeceasesValue, specificUnits[i]);
                TechManager.Instance.AdjustUnitGoldCost(-goldDeceasesValue, specificUnits[i]);

                TechManager.Instance.AdjustUnitIronCost((1 - ironDeceaseRatio), specificUnits[i]);
                TechManager.Instance.AdjustUnitGoldCost((1 - goldDeceaseRatio), specificUnits[i]);
            }
        }
        else
        {
            TechManager.Instance.AdjustUnitIronCost(-ironDeceasesValue);
            TechManager.Instance.AdjustUnitGoldCost(-goldDeceasesValue);

            TechManager.Instance.AdjustUnitIronCost((1 - ironDeceaseRatio));
            TechManager.Instance.AdjustUnitGoldCost((1 - goldDeceaseRatio));
        }
    }
}
