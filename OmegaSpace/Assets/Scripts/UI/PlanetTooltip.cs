using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlanetDescItem
{
    private string filePath = "Icon/";
    [SerializeField]
    private RectTransform panelItem = null;
    [SerializeField]
    private Image imageDesc = null;
    [SerializeField]
    private Text textDesc = null;

    public PlanetDescItem(RectTransform root)
    {
        if(root == null)
        {
            Debug.LogError(root.name + "가 유효하지 않습니다");
            return;
        }

        panelItem = root.GetChild(root.transform.childCount - 1).GetComponent<RectTransform>();

        if(panelItem == null)
        {
            Debug.LogError(panelItem.name + "가 유효하지 않습니다");
        }

        imageDesc = panelItem.GetComponentInChildren<Image>();
        if(imageDesc == null)
        {
            Debug.LogError(imageDesc.GetType().Name + "가 유효하지 않습니다");
        }

        textDesc = panelItem.GetComponentInChildren<Text>();
        if(textDesc == null)
        {
            Debug.LogError(textDesc.name + "가 유효하지 않습니다");
        }
    }

    public void SetVisible(bool visible)
    {
        if(panelItem == null)
        {
            return;
        }

        panelItem.gameObject.SetActive(visible);
    }

    public float Height
    {
        get
        {
            if (panelItem == null)
            {
                return 0;
            }

            return panelItem.rect.height;
        }
    }

    public float Line
    {
        get
        {
            if(panelItem == null)
            {
                return 0;
            }

            return panelItem.rect.y;
        }
        set
        {
            if(panelItem == null)
            {
                return;
            }

            panelItem.localPosition = 
                new Vector3(panelItem.localPosition.x, value, panelItem.localPosition.z);
        }
    }

    public void SetDescIcon(string iconName)
    {
        if(imageDesc == null)
        {
            return;
        }

        string iconPath = filePath + iconName; 
        Sprite icon = Resources.Load<Sprite>(iconPath) as Sprite;
        if(icon == null)
        {
            Debug.LogError("스프라이트 로드에 실패했습니다");
            return;
        }

        imageDesc.sprite = icon;
    }

    public void SetDesc(string desc)
    {
        if(textDesc == null)
        {
            return;
        } 

        textDesc.text = desc;
    }

    
}

public class PlanetTooltip : TooltipBase
{
    [Serializable]
    public struct PlanetDesc
    {
        public string icon;
        public string desc;
    }

    [SerializeField]
    private RawImage imageObject = null;
    [SerializeField]
    private Text textPlanet = null;
    [SerializeField]
    private RectTransform panelItem = null;
    [SerializeField]
    private GameObject prefabDescItem = null;

    private List<PlanetDescItem> items = new List<PlanetDescItem>();
    private float currYPos = 0;
 
    protected override void Awake()
    {
        base.Awake();
        if(imageObject == null)
        {
            Debug.LogError("imageObject가 유효하지 않습니다");
            return;
        }
        if(textPlanet == null)
        {
            Debug.LogError("textPlanet이 유효하지 않습니다");
            return;
        }
        if(panelItem == null)
        {
            Debug.LogError("panelItem이 유효하지 않습니다");
            return;
        }
        
        AddItem("Icon_Planet"," ");
        AddItem("Icon_Civil"," ");
        AddItem("Icon_Diamond"," ");
        AddItem("Icon_Bottle"," ");
       
    }
    public void SetTextPlanet(string planetName)
    {
        if (textPlanet == null)
        {
            return;
        }
        textPlanet.text = planetName;
    }
   
    public void AddItem(string iconName, string desc)
    {
        if (prefabDescItem == null)
        {
            Debug.LogError("prefabDescItem이 등록되지 않았습니다.");
            return;
        }

        GameObject newDescItem = GameObject.Instantiate(prefabDescItem, panelItem);
        PlanetDescItem item = new PlanetDescItem(panelItem);
        if(item == null)
        {
            Debug.LogError("PlanetDescItem을 생성하는 데 실패했습니다");
            return;
        }

        item.SetDescIcon(iconName);
        item.SetDesc(desc);

        if(item.Height > 0)
        {
            item.Line = currYPos - item.Height;
            currYPos -= item.Height;
            items.Add(item);
        }
    }

    public PlanetDescItem GetDescItem(int index)
    {
        if (index < 0 || index >= items.Count)
        {
            return null;
        }

        return items[index];
    }
}