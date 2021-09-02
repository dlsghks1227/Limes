using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public string name;
    public int amountToPool;            // 생성 할 오브젝트 수
    public bool shouldExpand = true;    // 확장

    public ObjectPoolItem(GameObject obj, string name, int amt, bool exp = true)
    {
        objectToPool = obj;
        this.name = name;
        amountToPool = Mathf.Max(amt, 2);
        shouldExpand = exp;
    }
}

[System.Serializable]
public class ObjectPool : MonoBehaviour
{
    public GameObject parent;

    public List<ObjectPoolItem> itemsToPool;
    private Dictionary<string, int> itemsToPoolPosition;

    public Dictionary<string, List<GameObject>> pooledObjectsList;
    private Dictionary<string, int> position;


    public void Awake()
    {
        itemsToPoolPosition = new Dictionary<string, int>();
        pooledObjectsList = new Dictionary<string, List<GameObject>>();
        position = new Dictionary<string, int>();

        for (int i = 0; i < itemsToPool.Count; i++)
            ObjectPoolItemToPooledObject(i);
    }

    public bool IsContainObject(string name)
    {
        return pooledObjectsList.ContainsKey(name);
    }

    /// <summary>
    /// 오브젝트 풀의 오브젝트를 반환합니다.
    /// </summary>
    /// <param name="name">오브젝트 풀 이름</param>
    /// <returns></returns>
    public GameObject GetPooledObject(string name)
    {
        if (itemsToPoolPosition.ContainsKey(name) == false
            || pooledObjectsList.ContainsKey(name) == false
            || position.ContainsKey(name) == false)
            return null;

        int curSize = pooledObjectsList[name].Count;

        for (int i = position[name]; i < position[name] + pooledObjectsList[name].Count; i++)
        {
            if (!pooledObjectsList[name][i % curSize].activeInHierarchy)
            {
                position[name] = i % curSize;
                return pooledObjectsList[name][i % curSize];
            }
        }

        int num = itemsToPoolPosition[name];

        if (itemsToPool[num].shouldExpand)
        {
            GameObject obj = Instantiate(itemsToPool[num].objectToPool) as GameObject;
            obj.SetActive(false);
            obj.transform.SetParent(parent.transform);
            pooledObjectsList[name].Add(obj);
            return obj;
        }
        return null;
    }

    /// <summary>
    /// 오브젝트 풀의 모든 오브젝트를 List 형식으로 반환합니다.
    /// </summary>
    /// <param name="name">오브젝트 풀 이름</param>
    /// <returns></returns>
    public List<GameObject> GetAllPooledObjects(string name)
    {
        if (pooledObjectsList.ContainsKey(name) == false)
            return null;

        return pooledObjectsList[name];
    }

    /// <summary>
    /// 오브젝트 풀을 추가합니다.
    /// </summary>
    /// <param name="obj">추가 할 오브젝트</param>
    /// <param name="name">오브젝트 풀 이름</param>
    /// <param name="amt">오브젝트 풀 개수</param>
    /// <param name="exp">오브젝트 풀 확장</param>
    /// <returns></returns>
    public int AddObject(GameObject obj, string name, int amt = 3, bool exp = true)
    {
        ObjectPoolItem item = new ObjectPoolItem(obj, name, amt, exp);
        int currLen = itemsToPool.Count;

        itemsToPool.Add(item);
        ObjectPoolItemToPooledObject(currLen);

        return currLen;
    }

    private void ObjectPoolItemToPooledObject(int index)
    {
        ObjectPoolItem item = itemsToPool[index];
        List<GameObject> pooledObjects = new List<GameObject>();

        for (int i = 0; i < item.amountToPool; i++)
        {
            GameObject obj = Instantiate(item.objectToPool) as GameObject;
            obj.SetActive(false);
            obj.transform.SetParent(parent.transform);
            pooledObjects.Add(obj);
        }

        itemsToPoolPosition.Add(item.name, index);
        pooledObjectsList.Add(item.name, pooledObjects);
        position.Add(item.name, 0);
    }
}