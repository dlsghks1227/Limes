using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;
    private bool downMode = false;

    private void Awake()
    {
        if(this.GetComponent<Camera>() == null)
        {
            Debug.LogError("카메라 컴포넌트가 존재하지 않습니다");
            return;
        }
    }

    void Update()
    {
        if(downMode == false)
        {
            if(transform.rotation.eulerAngles.x <= 20.0f || 360f - transform.rotation.eulerAngles.x <= 20.0f)
            {
                transform.Rotate(direction * Time.deltaTime);
            }
            else
            {
                downMode = true;
            }
        }
        else
        {
            if(transform.rotation.eulerAngles.x >= 360f - transform.rotation.eulerAngles.x)
            {
                transform.Rotate(-direction * Time.deltaTime);
            }
            else
            {
                downMode = false;
            }
        }
    }
}
