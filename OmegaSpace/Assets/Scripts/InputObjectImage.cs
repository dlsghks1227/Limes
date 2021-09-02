using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class InputObjectImage : MonoBehaviour
{
    [Header("Building Object")]
    [SerializeField]
    private GameObject buildingGroup = null;
    [SerializeField]
    private GameObject initObject = null;
    [SerializeField]
    private GameObject barrack = null;
    [SerializeField]
    private GameObject barricade = null;
    [SerializeField]
    private GameObject command = null;
    [SerializeField]
    private GameObject exchange = null;
    [SerializeField]
    private GameObject storage = null;
    [SerializeField]
    private GameObject minerA = null;
    [SerializeField]
    private GameObject minerB = null;
    [SerializeField]
    private GameObject meleeSplash = null;
    [SerializeField]
    private GameObject rangeSplashTurret = null;
    [SerializeField]
    private GameObject rangeTurret = null;
    [SerializeField]
    private GameObject protector = null;

    [Header("Unit Object")]
    [SerializeField]
    private GameObject splashUnit = null;
    [SerializeField]
    private GameObject rangeUnit = null;
    [SerializeField]
    private GameObject rangeSplashUnit = null;
    [SerializeField]
    private GameObject meleeUnit = null;

    private GameObject imgObject = null;
    public GameObject curUnitFocus = null;
    public GameObject curbuildFocus = null;
    public GameObject curObjectFocus = null;

    public void Awake()
    {
        if (buildingGroup == null)
        {
            return;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        curUnitFocus = initObject;
        curbuildFocus = initObject;
        curObjectFocus = initObject;
    }

    public void SetSelectedBuild(GameObject prefab)
    {
        Destroy(curbuildFocus);
        UnityEngine.Debug.Log(transform.Find(prefab.name).gameObject);
        imgObject = Instantiate(transform.Find(prefab.name).gameObject,new Vector3(2000, 0, 2000), Quaternion.identity);
        imgObject.layer = 12;
        curbuildFocus = imgObject;
    }

    public void SetSelectedUnit(String name)
    {
        Destroy(curUnitFocus);
        imgObject = Instantiate(transform.Find(name).gameObject, new Vector3(1500, 0, 1500), Quaternion.identity);
        imgObject.layer = 12;
        curUnitFocus = imgObject;
    }

    public void ShowSelectedObject(String name)
    {
        if (name == "DomeBarrier"|| transform.Find(name).gameObject.name != name)
        {
            return;
        }
        Destroy(curObjectFocus);
        imgObject = Instantiate(transform.Find(name).gameObject, new Vector3(1000, 0, 1000), Quaternion.identity);
        imgObject.layer = 12;
        curObjectFocus = imgObject;
    }


}
