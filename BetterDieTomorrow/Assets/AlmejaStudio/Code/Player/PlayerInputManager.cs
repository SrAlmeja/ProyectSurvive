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
    private AnimationStateController _animStateController;

    private Vector2 _movement;
    private float _croachSpeed;

    private Vector2 _mousePos;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _playerController = GetComponent<PlayerController>();
        _animStateController = GetComponent<AnimationStateController>();
        _animStateController.Animator = GetComponentInChildren<Animator>();

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
            _playerInputActions.Player.Dodge.canceled += OnDodging;

            _playerInputActions.Player.Crouch.performed += OnCrouch;
            _playerInputActions.Player.Crouch.canceled += OnCrouch;
            
            _playerInputActions.Player.ChangeWeapon.performed += OnChangeWeapon;

            _playerInputActions.Player.Shoot.performed += OnShoot;
            _playerInputActions.Player.Shoot.canceled += OnShoot;
            
            _playerInputActions.Player.ReloadWeapon.performed += OnReload;
            _playerInputActions.Player.ReloadWeapon.canceled += OnReload;
            
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
        Vector3 movementDirection = _movement.x * transform.right + _movement.y * transform.forward;
        _rigidbody.AddForce(movementDirection.normalized * (_playerController.Speed - _croachSpeed));
        
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
        float movemagnitude = _movement.magnitude;
        _animStateController.Animator.SetFloat("Move", movemagnitude);
        _movement = context.ReadValue<Vector2>();
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
        bool isDodging = context.ReadValueAsButton();
        _animStateController.Animator.SetBool("Roll", isDodging);

        Vector3 jumpDirection = Vector3.up * _playerController.JumpForce;
        Vector3 forwardDirection = transform.forward * _playerController.ForwardForce;
        _rigidbody.AddForce(jumpDirection + forwardDirection, ForceMode.Impulse);    
        
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        _animStateController.Animator.SetTrigger("Crouch");
    }

    public void OnChangeWeapon(InputAction.CallbackContext context)
    {
        if (_playerController.IsWeaponEquipped)
        {
            _playerController.IsWeaponEquipped = false;
        }
        else
        {
            _playerController.IsWeaponEquipped = true;
            _animStateController.Animator.SetTrigger("ChangeWeapon");
        }
        _animStateController.Animator.SetBool("IsWeaponEquipped", _playerController.IsWeaponEquipped);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        bool isShoot = context.ReadValueAsButton();
        _animStateController.Animator.SetBool("Attack", isShoot);
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        Debug.Log("Realoading!!!");
    }
}
