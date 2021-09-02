using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if( col.tag =="A" || col.tag == "B")
        {
            Destroy(gameObject);
        }
    }
}