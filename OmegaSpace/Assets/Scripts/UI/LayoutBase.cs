using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayoutBase : MonoBehaviour
{
    private GameObject rootNode = null;
    public GameObject RootNode { get => rootNode; }

    public virtual void SetVisible(bool visible)
    {
        if (rootNode == null)
        {
            return;
        }

        rootNode.SetActive(visible);
    }

    public virtual void Toggle()
    {
        if(rootNode == null)
        {
            return;
        }

        SetVisible(!rootNode.activeInHierarchy);
    }



    protected virtual void Awake()
    {
        if (GetComponent<RectTransform>() == null)
        {
            Debug.LogError("Layout을 소유하는 GameObject는 반드시 RectTransform 컴포넌트여야 합니다");
            return;
        }

        rootNode = gameObject;
    }
}