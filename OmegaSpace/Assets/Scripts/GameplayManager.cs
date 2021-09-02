using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameplayManager : MonoSingleton<GameplayManager>
{
    [SerializeField] private MapGenerator   mapGenerator    = null;
    [SerializeField] private SpaceGenerator spaceGenerator  = null;
    [SerializeField] private Grid           grid            = null;

    [SerializeField] private PlayerStateManager playerStateManager = null;

    public BuildingData buildingData = null;

    [SerializeField] private Image       image = null;
    [SerializeField] private GameObject   text = null;

    private bool isDead = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangePlanet(spaceGenerator.SpaceGenerate());
    }

    private void Update()
    {
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            CheckCommandCount();
        }
    }

    public void ChangePlanet(Planet planet)
    {
        mapGenerator.OnSave();
        mapGenerator.OnExit();

        mapGenerator.OnLoad(planet);

        PlayerDataManager.Instance.currentPlanet = planet;
    }

    public void CheckCommandCount()
    {
        if (isDead == true)
            return;
        int commandCount = 0;
        for (int i = 0; i < grid.currentPlayerBuildings.Count; i++)
        {
            if (grid.currentPlayerBuildings[i].GetBuildingType().Equals(EBuilding.COMMAND))
            {
                if (grid.currentPlayerBuildings[i].gameObject.activeSelf)
                {
                    commandCount++;
                }
            }
        }

        if (commandCount <= 0)
        {
            isDead = true;
            StartCoroutine(PlayEndGame());
            StartCoroutine(PlayerEndLayout());
            playerStateManager.GameOverStateChange();
        }
    }

    IEnumerator PlayEndGame()
    {
        for (int i = 0; i < grid.currentPlayerBuildings.Count; i++)
        {
            if (grid.currentPlayerBuildings[i].gameObject.activeSelf)
            {
                grid.currentPlayerBuildings[i].TakeDamage(99999);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    IEnumerator PlayerEndLayout()
    {
        image.gameObject.SetActive(true);
        image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        text.SetActive(false);
        for (float i = 0.0f; i <= 1.0f; i += 0.005f)
        {
            image.color = new Color(1.0f, 1.0f, 1.0f, i);
            yield return null;
        }
        text.SetActive(true);
    }
}
