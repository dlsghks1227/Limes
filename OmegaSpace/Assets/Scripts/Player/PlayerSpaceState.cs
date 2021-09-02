using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerSpaceState : PlayerState
{
    [SerializeField] private Vector3    spacePosition   = new Vector3(8.5f, 10000.0f, -8.5f);
    [SerializeField] private Vector3    angleVector     = new Vector3(90.0f, -45.0f, 0.0f);
    [SerializeField] private float      cameraDistance = 50.0f;

    [SerializeField] private SpaceGenerator spaceGenerator = null;

    void Start()
    {
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<Planet>() != null)
            {
                Planet planet = hit.collider.gameObject.GetComponent<Planet>();

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    if (spaceGenerator.IsMoveable(PlayerDataManager.Instance.currentPlanet, planet) == true)
                    {
                        GameplayManager.Instance.ChangePlanet(planet);
                        CameraManager.Instance.CameraMove(planet.gameObject.transform.position, 2.0f, cameraDistance, true);
                    }
                }
            }
        }
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        spacePosition = PlayerDataManager.Instance.currentPlanet.transform.position + new Vector3(0.0f, 0.0f, 0.0f);

        CameraManager.Instance.mainCamera.transform.rotation = Quaternion.Euler(angleVector);
        CameraManager.Instance.CameraMove(spacePosition, 3000.0f, cameraDistance, false);
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }
}
