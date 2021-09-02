using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{
    Vector3 startVec;
    Vector3 edgeVec;
    Vector3 currentVec;

    private void Update()
    {
        ChooseObject();
        if (Mouse.current.leftButton.wasPressedThisFrame) ReadCurrentPosition();
    }

    private void ChooseObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit)) {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                startVec = hit.point;
                Debug.Log("startVec : " + startVec);
            }
            if (startVec != null && Mouse.current.rightButton.wasReleasedThisFrame)
            {
                edgeVec = hit.point;
                Debug.Log("edgeVec : " + edgeVec);
            }
        }
    }

    private void ReadCurrentPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            currentVec = hit.point;
            Debug.Log("currentVect : " + currentVec);
        }
    }
}
