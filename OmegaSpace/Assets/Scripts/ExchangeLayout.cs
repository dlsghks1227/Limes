using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeLayout : LayoutBase
{
    [SerializeField]
    private ResourceStorage storage = null;
    
    [SerializeField]
    private CanvasGroup exchangeTab = null;

    [SerializeField] private Image resource;
    [SerializeField] private Text resName;
    private bool isResChoose = false;
    [SerializeField] private Image target;
    [SerializeField] private Text targetName;

    private ResourceAmount res;
    private ResourceAmount tar;

    [SerializeField]
    float IronGoldrate;
    [SerializeField]
    float IronEtherrate;
    [SerializeField]
    float GoldEtherrate;

    private float rate;

    private int resAmount;
    private float tarAmount;

    [SerializeField]
    private InputField amount;
    private InputField targetAmount;

    [SerializeField] private Button btn_gold = null;
    [SerializeField] private Button btn_iron = null;
    [SerializeField] private Button btn_ether = null;

    [SerializeField] private Image gold = null;
    [SerializeField] private Image iron = null;
    [SerializeField] private Image ether = null;

    [SerializeField] private Image result = null;

    [SerializeField] private Button btn_exchange = null;
    [SerializeField] private Button btn_cancel = null;

    [SerializeField] private Button btn_exit = null;

    [SerializeField] private Text returnAmount = null;

    private InputExchangeState exchangeState = null;

    private void Start()
    {
        Initialize();
    }

    private void SetInfo(string arg0)
    {
        resAmount = int.Parse(arg0);
        tarAmount = (float)resAmount * rate;
        res.resAmount = -resAmount;
        tar.resAmount = (int)tarAmount/1;
        result.enabled = true;
        result.sprite = target.sprite;
        ShowInfo();
    }

    private void Initialize()
    {
        exchangeState = FindObjectOfType<InputExchangeState>();

        btn_exchange.onClick.AddListener(ExchangeRes);
        btn_cancel.onClick.AddListener(Clean);
        btn_exit.onClick.AddListener(Exit);

        btn_gold.onClick.AddListener(PickGold);
        btn_iron.onClick.AddListener(PickIron);
        btn_ether.onClick.AddListener(PickEther);

        amount.onEndEdit.AddListener(SetInfo);

        resource.enabled = false;
        target.enabled = false;
        result.enabled = false;
    }

    private void PickIron()
    {
        if (isResChoose)
        {
            targetName.text = "Iron";
            target.enabled = true;
            target.GetComponent<Image>().sprite = iron.sprite;
            tar = new ResourceAmount(EResource.RES_IRON, 0);
            if (res.resType == EResource.RES_GOLD) rate = 1/IronGoldrate;
            else if (res.resType == EResource.RES_ETHER) rate = 1/IronEtherrate;
        }
        else
        {
            resName.text = "Iron";
            resource.enabled = true;
            resource.GetComponent<Image>().sprite = iron.sprite;
            isResChoose = true;
            res = new ResourceAmount(EResource.RES_IRON, 0);
        }
    }

    private void PickGold()
    {
        if (isResChoose)
        {
            targetName.text = "Gold";
            target.enabled = true;
            target.GetComponent<Image>().sprite = gold.sprite;
            tar = new ResourceAmount(EResource.RES_GOLD, 0);
            if (res.resType == EResource.RES_IRON) rate = IronGoldrate;
            else if (res.resType == EResource.RES_ETHER) rate = 1 / GoldEtherrate;
        }
        else
        {
            resName.text = "Gold";
            resource.enabled = true;
            resource.GetComponent<Image>().sprite = gold.sprite;
            res = new ResourceAmount(EResource.RES_GOLD, 0);
            isResChoose = true;
        }
    }

    private void PickEther()
    {
        if (isResChoose)
        {
            targetName.text = "Ether";
            target.enabled = true;
            target.GetComponent<Image>().sprite = ether.sprite;
            tar = new ResourceAmount(EResource.RES_ETHER, 0);
            if (res.resType == EResource.RES_GOLD) rate = GoldEtherrate;
            else if (res.resType == EResource.RES_IRON) rate = IronEtherrate;
        }
        else
        {
            resName.text = "Ether";
            resource.enabled = true;
            resource.GetComponent<Image>().sprite = ether.sprite;
            res = new ResourceAmount(EResource.RES_ETHER, 0);
            isResChoose = true;
        }
    }

    private void ShowInfo()
    {
        returnAmount.text = tar.resAmount.ToString();
    }

    private void ExchangeRes()
    {
        if(storage.GetAmount(res.resType) < -(res.resAmount))
        {
            Debug.Log("Not Enough Resource");
            exchangeTab.alpha = 0;
            exchangeTab.blocksRaycasts = false;
            exchangeState.OnExitState();
            Clean();
        }
        else
        {
            storage.Store(res);
            storage.Store(tar);
            exchangeTab.alpha = 0;
            exchangeTab.blocksRaycasts = false;
            exchangeState.OnExitState();
            Clean();
        }
    }

    private void Exit()
    {
        Clean();
        exchangeTab.alpha = 0;
        exchangeTab.blocksRaycasts = false;
        exchangeState.OnExitState();
    }

    private void Clean()
    {
        targetName.text = "목표 자원";
        target.GetComponent<Image>().sprite = null;
        target.enabled = false;
        resName.text = "대상 자원";
        resource.GetComponent<Image>().sprite = null;
        resource.enabled = false;
        result.sprite = null;
        result.enabled = false;
        res = new ResourceAmount();
        tar = new ResourceAmount();
        //info.text = "";
        amount.text = "자원 소비량";
        returnAmount.text = "0ㅅ0";
    }
}