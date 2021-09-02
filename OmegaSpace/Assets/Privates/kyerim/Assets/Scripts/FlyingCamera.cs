using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class FlyingCamera : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset cameraMove = null;
    private InputAction horizontalMove;
    private InputAction verticalMove;

    private float moveSpeed = 5.0f;
    //private float rotSpeed = 2.0f;

    private float horizontalValue;
    private float verticalValue;

    private Vector3 cameraInitPosition;
    private Vector3 cameraInitRotation;

    private void Awake()
    {
        horizontalMove = cameraMove.FindActionMap("CameraMove").FindAction("HorizontalMove");
        verticalMove = cameraMove.FindActionMap("CameraMove").FindAction("VerticalMove");
        cameraInitRotation = new Vector3(90, 0, 0);
        cameraInitPosition = new Vector3(10, 20, 10);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * verticalValue);
        transform.Translate(Vector3.right * horizontalValue);
    }

    private void OnEnable()
    {
        SetCameraTransform();
        horizontalMove.performed += OnhorizontalMovement;
        horizontalMove.canceled += horizontalMovementCanceled;
        verticalMove.performed += OnverticalMovement;
        verticalMove.canceled += verticalMovementCanceled;
        horizontalMove.Enable();
        verticalMove.Enable();
    }

    private void OnDisable()
    {
        horizontalMove.performed -= OnhorizontalMovement;
        horizontalMove.canceled -= horizontalMovementCanceled;
        verticalMove.performed -= OnverticalMovement;
        verticalMove.canceled -= verticalMovementCanceled;
        horizontalMove.Disable();
        verticalMove.Disable();
    }

    private void OnverticalMovement(InputAction.CallbackContext context)
    {
        verticalValue = context.ReadValue<float>() * moveSpeed * Time.deltaTime;
    }

    private void OnhorizontalMovement(InputAction.CallbackContext context)
    {
        horizontalValue = context.ReadValue<float>() * moveSpeed * Time.deltaTime;
    }

    private void verticalMovementCanceled(InputAction.CallbackContext context)
    {
        verticalValue = 0.0f;
    }

    private void horizontalMovementCanceled(InputAction.CallbackContext context)
    {
        horizontalValue = 0.0f;
    }

    private void SetCameraTransform()
    {
        Quaternion rot;
        rot = Quaternion.Euler(cameraInitRotation);
        transform.rotation = rot;
        transform.position = cameraInitPosition;
    }
}