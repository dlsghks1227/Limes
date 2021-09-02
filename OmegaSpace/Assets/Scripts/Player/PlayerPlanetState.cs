using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlanetState : PlayerState
{
    [SerializeField] private Vector3    planetPosition  = new Vector3(8.5f, 0.0f, -8.5f);
    [SerializeField] private Vector3    angleVector     = new Vector3(60.0f, -45.0f, 0.0f);
    [SerializeField] private float      cameraDistance = 20.0f;

    [Header("Input")]
    [SerializeField] private InputActionAsset inputPlayerControl = null;
    private InputAction movementLeft;
    private InputAction movementRight;
    private InputAction movementUp;
    private InputAction movementDown;

    private Vector2 direction = new Vector2();

    void Awake()
    {
        CameraManager.Instance.CameraMove(planetPosition, 3000.0f, cameraDistance, false);

        movementLeft    = inputPlayerControl.FindActionMap("CameraMove").FindAction("Left");
        movementRight   = inputPlayerControl.FindActionMap("CameraMove").FindAction("Right");
        movementUp      = inputPlayerControl.FindActionMap("CameraMove").FindAction("Up");
        movementDown    = inputPlayerControl.FindActionMap("CameraMove").FindAction("Down");

        movementLeft.performed  += ctx => { direction += new Vector2(-1.0f, -1.0f); };
        movementLeft.canceled   += ctx => { direction -= new Vector2(-1.0f, -1.0f); };

        movementRight.performed += ctx => { direction += new Vector2( 1.0f, 1.0f); };
        movementRight.canceled  += ctx => { direction -= new Vector2( 1.0f, 1.0f); };

        movementUp.performed    += ctx => { direction += new Vector2(-1.0f, 1.0f); };
        movementUp.canceled     += ctx => { direction -= new Vector2(-1.0f, 1.0f); };

        movementDown.performed  += ctx => { direction += new Vector2( 1.0f,-1.0f); };
        movementDown.canceled   += ctx => { direction -= new Vector2( 1.0f,-1.0f); };
    }

    void Update()
    {
        if (CameraManager.Instance.mainCamera.transform.position.y <= (cameraDistance + 1.0f))
        {
            CameraManager.Instance.CameraMove(CameraManager.Instance.mainCamera.transform.position + new Vector3(direction.x, 0.0f, direction.y), 5.0f, 0.0f);
        }
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        if (movementLeft    != null) movementLeft.Enable();
        if (movementRight   != null) movementRight.Enable();
        if (movementUp      != null) movementUp.Enable();
        if (movementDown    != null) movementDown.Enable();

        CameraManager.Instance.mainCamera.transform.rotation = Quaternion.Euler(angleVector);
        CameraManager.Instance.CameraMove(planetPosition, 3000.0f, cameraDistance, false);
    }

    public override void OnExitState()
    {
        base.OnExitState();

        if (movementLeft    != null) movementLeft.Disable();
        if (movementRight   != null) movementRight.Disable();
        if (movementUp      != null) movementUp.Disable();
        if (movementDown    != null) movementDown.Disable();
    }
}
