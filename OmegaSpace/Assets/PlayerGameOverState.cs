using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGameOverState : PlayerState
{
    [SerializeField] private Vector3    endPosition = new Vector3(25.0f, 70.0f, -25.0f);
    [SerializeField] private Vector3    angleVector = new Vector3(90.0f, -45.0f, 0.0f);
    [SerializeField] private float      cameraDistance = 50.0f;

    public override void OnEnterState()
    {
        base.OnEnterState();

        //CameraManager.Instance.mainCamera.transform.position = new Vector3(15.0f, 30.0f, -15.0f);
        CameraManager.Instance.mainCamera.transform.rotation = Quaternion.Euler(angleVector);
        CameraManager.Instance.CameraMove(endPosition, 0.05f, cameraDistance, true);
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }
}
