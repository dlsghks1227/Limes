using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    [SerializeField]
    private Text txtCoal = null;
    [SerializeField]
    private Text txtIron = null;
    [SerializeField]
    private Text txtAluminum = null;
    [SerializeField]
    private Text txtGold = null;
    [SerializeField]
    private Text txtVibranium = null;
    [SerializeField]
    private Text txtfood = null;
    [SerializeField]
    private Text txtpopulation = null;

    public Image itemSlider;

    private bool canSlider;
    private float itemCooldownTime;
    private float updateTime;

    PlayerInformation playerInfo = new PlayerInformation();
    void Start()
    {
        canSlider = true;
        updateTime = 0.0f;
        itemCooldownTime = 5.0f;

        playerInfo.Initialize();

    }
    void Update()
    {
        txtfood.text = playerInfo.food.currentValue.ToString();
        txtpopulation.text = playerInfo.population.currentValue.ToString();

        if (canSlider)
        {
            updateTime = updateTime + Time.deltaTime;
            itemSlider.fillAmount = 1.0f - (Mathf.SmoothStep(0, 100, updateTime / itemCooldownTime) / 100);

            if (updateTime > itemCooldownTime)
            {
                itemSlider.fillAmount = 0.0f;
                updateTime = 0.0f;
                canSlider = false;
            }
        }
    }

}
