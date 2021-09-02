using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class MiningText : MonoBehaviour
{
    [SerializeField] private TextMesh miningAmount = null;
    [SerializeField] private GameObject container = null;

    private Vector3 position;

    float time;
    public float _fadeTime = 1f;
    private bool isActive = false;

    void Update()
    {
        if (isActive)
        {
            container.transform.LookAt(Camera.main.transform);
            container.transform.Translate(Vector3.up * Time.deltaTime);
            if (time < _fadeTime || time == _fadeTime)
            {
                miningAmount.GetComponent<TextMesh>().color = new Color(1, 1, 1, 1f - (time / _fadeTime));
            }
            else
            {
                time = 0;
                resetAnim();
            }
            time += Time.deltaTime;
        }
    }

    public void resetAnim()
    {
        container.transform.position = position;
        miningAmount.GetComponent<TextMesh>().color = Color.white;
    }

    public void SetPosition(Vector3 pos)
    {
        container.transform.position = pos + new Vector3(0, 1, 0);
        position = pos + new Vector3(0, 1, 0);
    }

    public void SetText(string text)
    {
        miningAmount.text = text;
    }

    public void Activate()
    {
        miningAmount.gameObject.SetActive(true);
        isActive = true;
    }

    public void Deactivate()
    {
        miningAmount.gameObject.SetActive(false);
        isActive = false;
    }
}
