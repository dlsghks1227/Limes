using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestCreate : MonoBehaviour
{
    private List<Material> defaultMaterials = new List<Material>();
    [SerializeField] private List<Material> createMeterials   = new List<Material>();
    [SerializeField] private List<Material> deleteMeterials   = new List<Material>();

    private MeshRenderer currentMeshRenderer = null;

    public float delayTime = 1.0f;

    void Awake()
    {
        currentMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        defaultMaterials.AddRange(currentMeshRenderer.materials);
        //StartCoroutine(CreateBuilding());
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            StartCoroutine(CreateBuilding(delayTime));
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            StartCoroutine(DeleteBuilding(delayTime));
        }
    }

    IEnumerator CreateBuilding(float time)
    {
        List<Material> materials = new List<Material>();
        for (int i = 0; i < createMeterials.Count; i++)
        {
            if (createMeterials[i] != default)
            {
                materials.Add(createMeterials[i]);
                if (createMeterials[i].HasProperty("Time"))
                {
                    createMeterials[i].SetFloat("Time", 0.0f);
                }
            }
            else
            {
                materials.Add(defaultMaterials[i]);
            }
        }

        currentMeshRenderer.materials = materials.ToArray();

        for (float i = 0.0f; i < 0.6f; i += (0.01f / time))
        {
            for (int j = 0; j < createMeterials.Count; j++)
            {
                if (currentMeshRenderer.materials[j].HasProperty("Time") == true)
                {
                    currentMeshRenderer.materials[j].SetFloat("Time", i);
                }
            }
            yield return null;
        }

        currentMeshRenderer.materials = defaultMaterials.ToArray();
    }

    IEnumerator DeleteBuilding(float time)
    {
        List<Material> materials = new List<Material>();
        for (int i = 0; i < deleteMeterials.Count; i++)
        {
            if (deleteMeterials[i] != default)
            {
                materials.Add(deleteMeterials[i]);
                if (deleteMeterials[i].HasProperty("Time"))
                {
                    deleteMeterials[i].SetFloat("Time", 0.0f);
                }
            }
            else
            {
                materials.Add(defaultMaterials[i]);
            }
        }

        currentMeshRenderer.materials = materials.ToArray();

        for (float i = 0.4f; i < 1.0f; i += (0.01f / time))
        {
            for (int j = 0; j < deleteMeterials.Count; j++)
            {
                if (currentMeshRenderer.materials[j].HasProperty("Time") == true)
                {
                    currentMeshRenderer.materials[j].SetFloat("Time", i);
                }
            }
            yield return null;
        }

        // currentMeshRenderer.materials = defaultMaterials.ToArray();
    }
}
