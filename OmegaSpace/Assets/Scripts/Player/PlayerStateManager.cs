using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public virtual void OnEnterState()  { enabled = true; }
    public virtual void OnExitState()   { enabled = false; }
}

public class PlayerStateManager : MonoBehaviour
{
    private PlayerSpaceState    SpaceState = null;
    private PlayerPlanetState   PlanetState = null;
    private PlayerGameOverState GameOverState = null;

    private PlayerState         currentState = null;

    [SerializeField] private Button changeButton = null;
    [SerializeField] private Sprite spaceImage = null;
    [SerializeField] private Sprite planetImage = null;


    void Awake()
    {
        SpaceState = gameObject.GetComponent<PlayerSpaceState>();
        PlanetState = gameObject.GetComponent<PlayerPlanetState>();
        GameOverState = gameObject.GetComponent<PlayerGameOverState>();
    }

    private void Start()
    {
        ChangeState(PlanetState);
    }

    private void ChangeState(PlayerState newState)
    {
        if (currentState)
        {
            currentState.OnExitState();
        }
        currentState = newState;
        currentState.OnEnterState();
    }

    public void GameOverStateChange()
    {
        ChangeState(GameOverState);
    }

    public void ToggleState()
    {
        if (currentState == PlanetState)
        {
            ChangeState(SpaceState);
            changeButton.image.sprite = spaceImage;
        }
        else
        {
            ChangeState(PlanetState);
            changeButton.image.sprite = planetImage;
        }
    }
}
