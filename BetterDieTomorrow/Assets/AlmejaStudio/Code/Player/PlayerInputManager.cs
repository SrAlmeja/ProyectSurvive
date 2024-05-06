using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;
    private PlayerController _playerController;
    private PlayerInputActions _playerInputActions;

    private Vector2 _movement;
    private float _croachSpeed;

    private Vector2 _mousePos;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _playerController = GetComponent<PlayerController>();

        #region Inputs
        if (_playerInputActions == null)
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();

            _playerInputActions.Player.Movement.performed += OnMovement;
            _playerInputActions.Player.Movement.canceled += OnMovement;

            _playerInputActions.Player.Loock.performed += OnMousePos;
            _playerInputActions.Player.Loock.canceled += OnMousePos;
            
            _playerInputActions.Player.Dodge.performed += OnDodging;
        }
        #endregion
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Disable();
    }
    

    private void FixedUpdate()
    {
        _rigidbody.AddForce((_movement.x * (_playerController.Speed - _croachSpeed)), 0, (_movement.y * (_playerController.Speed - _croachSpeed)));
        
        Vector3 mousePosWorld = GetMouseWorldPosition();
        Vector3 playerForward = mousePosWorld - _rigidbody.position;
        playerForward.y = 0;

        if (playerForward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(playerForward, Vector3.up);
            _rigidbody.MoveRotation(targetRotation);
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();
        _mousePos += lookInput * _playerController.CursorSpeed * Time.deltaTime;
    }

    public void OnMousePos(InputAction.CallbackContext context)
    {
        _mousePos = _playerController.Camera.WorldToScreenPoint(context.ReadValue<Vector2>());
    }
    
    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = _playerController.Camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
    
    public void OnDodging(InputAction.CallbackContext context)
    {
        Vector3 jumpDirection = Vector3.up * _playerController.JumpForce;
        Vector3 forwardDirection = transform.forward * _playerController.ForwardForce;

        _rigidbody.AddForce(jumpDirection + forwardDirection, ForceMode.Impulse);
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        Debug.Log("Croaching");
    }

    public void OnChangeWeapon(InputAction.CallbackContext context)
    {
        Debug.Log("ChangeWeapon");
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        Debug.Log("Shooting");
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        Debug.Log("Realoading!!!");
    }
}
