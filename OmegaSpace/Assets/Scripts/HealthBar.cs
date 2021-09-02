using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider hpSlider;
    private CanvasGroup slider;

    private Vector3 position;

    private float time;
    private float delay;
    float offset;

    public float size = 0.02f;

    private bool isActive = false;
    private void Start()
    {
        slider = hpSlider.GetComponent<CanvasGroup>();
        offset = transform.parent.gameObject.transform.localScale.y * 0.5f + 1.0f;
        gameObject.transform.localScale = new Vector3(size / transform.parent.localScale.x, size / transform.parent.localScale.x, size / transform.parent.localScale.x);
    }

    private void Update()
    {
        gameObject.transform.rotation = Camera.main.transform.rotation;
        //SetPosition(transform.parent.gameObject.transform.position);
        if (isActive)
        {
            slider.alpha = 1;
            time += Time.deltaTime;
        }
        if (time > delay)
        {
            time = 0.0f;
            isActive = false;
            slider.alpha = 0.0f;
        }
    }

    public void SetPosition(Vector3 target)
    {
        position = Camera.main.WorldToScreenPoint(target + new Vector3(0, offset, 0));
        hpSlider.transform.position = position;
    }

    public void SetHP(float hp, float maxHp, float delayTime)
    {
        hpSlider.value = hp / maxHp;
        delay = delayTime;
        isActive = true;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}