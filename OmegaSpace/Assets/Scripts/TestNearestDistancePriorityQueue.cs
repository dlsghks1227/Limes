using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTestNearestLocationPriorityQueue : MonoBehaviour
{
    public List<GameObject> samepleObjs = new List<GameObject>();
    DistancePriorityQueue distancePriorityQueue = new DistancePriorityQueue(Vector3.zero);
    void TestEnqueue()
    {
        foreach (var a in samepleObjs)
            distancePriorityQueue.Enqueue(a.transform);

        for (int i = 0; i < distancePriorityQueue.Count; i++)
            Debug.Log(i + "th element's location is " + distancePriorityQueue[i].position);

    }

    void TestDequeue()
    {
        var temp = distancePriorityQueue.Dequeue();
        for (int i = 0; temp != null; i++)
        {
            Debug.Log(temp.name + "'s location : " + temp.position);
            temp = distancePriorityQueue.Dequeue();
        }
    }

    void TestPeek()
    {
        Debug.Log("Top of the tree's element is " + distancePriorityQueue.Peek);
    }
    void Start()
    {
        //TestEnqueue();
        //TestDequeue();
        //TestPeek();
    }
}
