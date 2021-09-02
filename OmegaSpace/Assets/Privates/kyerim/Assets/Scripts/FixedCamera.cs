using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FixedCamera : MonoBehaviour
{
    private Transform target;

    private float dist = 20.0f;
    private float height = 25.0f;

    private Transform cameraPos;

    private void Awake()
    {
        target = GameObject.Find("RobotSphere").transform;
        cameraPos = GetComponent<Transform>();
    }

    private void Update()
    {
        cameraPos.position = new Vector3(target.position.x, target.position.y + height, target.position.z-dist);
        cameraPos.LookAt(target);
    }

}