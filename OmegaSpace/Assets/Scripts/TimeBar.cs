using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    [SerializeField]
    private Slider timeBar;
    private CanvasGroup slider;

    private bool isActive = false;

    private Vector3 position;
    private float time;
    private float maxTime;
    private float offset;

    void Start()
    {
        slider = timeBar.GetComponent<CanvasGroup>();
        offset = transform.parent.gameObject.transform.localScale.y * 0.5f + 1.4f;
    }

    void Update()
    {
        if (isActive)
        {
            gameObject.transform.rotation = Camera.main.transform.rotation;

            //SetPosition(transform.parent.gameObject.transform.position);
            SetTime(time);
            time += Time.deltaTime;
        }
    }

    public void SetPosition(Vector3 target)
    {
        position = Camera.main.WorldToScreenPoint(target + new Vector3(0, offset, 0));
        timeBar.transform.position = position;
    }

    public void SetTime(float currentTime)
    {
        timeBar.value = currentTime / maxTime;
        if (currentTime > maxTime) 
        {
            isActive = false;
            time = 0;
            maxTime = 0;
            slider.alpha = 0;
        }
    }

    public void SetMaxTime(float time)
    {
        maxTime = time;
        isActive = true;
        slider.alpha = 1;
    }

    public bool IsActive { get => isActive; }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
