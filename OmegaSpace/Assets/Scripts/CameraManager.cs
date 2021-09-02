using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoSingleton<CameraManager>
{
    public Camera mainCamera = null;
    public Camera InfoCamera = null;
    public Camera BuildCamera = null;
    public Camera UnitCamera = null;
    [SerializeField] private AnimationCurve curve;

    private Vector3 offset;
    private float   distance = 0.0f;
    private float   normalized = 0.0f;
    private float   cameraSpeed = 0.0f;

    private bool    isLerp = false;

    private void Awake()
    {
        InitUICamera();
    }
    private void LateUpdate()
    {
        if (distance > 0.0f)    normalized = Vector3.Distance(offset, mainCamera.transform.position) / distance;
        if (normalized > 1.0f)  normalized = 1.0f;
        if (normalized < 0.0f)  normalized = 0.0f;

        if (isLerp == true)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, offset, Time.deltaTime * cameraSpeed);
        }
        else
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, offset, curve.Evaluate(1.0f - normalized) * cameraSpeed);
        }
    }
    public void InitUICamera()
    { 
        InfoCamera.transform.position = new Vector3 (1004.25f, 4.52f, 996.62f);
        BuildCamera.transform.position = new Vector3(2004, 4.7f, 1998);
        UnitCamera.transform.position = new Vector3(1504, 4.73f, 1497);
        InfoCamera.transform.rotation = Quaternion.Euler(37, -53, 0);
        BuildCamera.transform.rotation = Quaternion.Euler(42, -64, 0);
        UnitCamera.transform.rotation = Quaternion.Euler(37, -53, 0);
        BuildCamera.fieldOfView = 25;
        InfoCamera.fieldOfView = 25;
        UnitCamera.fieldOfView = 25;
    }
    public void CameraMove(Vector3 target, float speed, float camDistance = 20.0f, bool lerp = true)
    {
        offset = new Vector3(target.x, target.y + camDistance, target.z);
        distance = Vector3.Distance(offset, mainCamera.transform.position);
        cameraSpeed = speed;
        isLerp = lerp;

    }
}
