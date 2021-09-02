using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManager 
{
    public GameObject ghostObject;
    public bool isConstructing = false;
    public bool isMiner = false;
    
    public void FixObject(Vector3 hitPosition)
    {
        if (isConstructing == true && ghostObject != null)
        {
            ghostObject.transform.position = hitPosition;
        }
    }
    public void CreateObject(GameObject selectObject, Vector3 hitPosition)
    {
        ghostObject = GameObject.Instantiate(selectObject, hitPosition, Quaternion.identity);
        ghostObject.SetActive(true);
    }
    public void CheckMiner(GameObject hitObject)
    {
        if (isConstructing == false)
        {
            return;
        }

        if ((hitObject.name.Equals("RedCube") && ghostObject.name.StartsWith("RedMiner")) ||
        (hitObject.name.Equals("YellowCube") && ghostObject.name.StartsWith("YellowMiner")) ||
        (hitObject.name.Equals("GreenCube") && ghostObject.name.StartsWith("GreenMiner")))
        {
            BuildMiner(hitObject);
        }
        else
        {
            GameObject.Destroy(ghostObject);
            isConstructing = false;
            isMiner = false;
        }
    }
    public void CheckArmy(GameObject hitObject,Vector3 hitPosition)
    {
        if (hitObject.name.Equals("Plane") && ghostObject.name.StartsWith("Barrack"))
        {
            BuildArmy(hitPosition);
        }
        else
        {
            GameObject.Destroy(ghostObject);
            isConstructing = false;
        }
    }
    private void BuildMiner(GameObject hitObject)
    {
        ghostObject.transform.position = hitObject.transform.position;
        ghostObject = null;
        GameObject.Destroy(hitObject);
        isConstructing = false;
        isMiner = false;
    }
    private void BuildArmy(Vector3 hitPosition)
    {
        ghostObject.transform.position = hitPosition;
        ghostObject = null;
        isConstructing = false;
    }
    public void CheckBuild(GameObject hitObject, Vector3 hitPosition)
    {
        if (isMiner)
            CheckMiner(hitObject);
        else
            CheckArmy(hitObject, hitPosition);
    }
}
