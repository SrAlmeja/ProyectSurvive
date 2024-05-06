using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region InputVariables

    private Rigidbody _rigidbody;
    [Header("Dedge Values")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _forwardForce;
    [Header("Movement Values")]
    [SerializeField] private float _speed;
    [SerializeField] private float _croachSpeed;
    private Vector2 _movement;
    private float _movementMagnitude;
    [Header("Look Values")]
    [SerializeField] private Camera _camera;

    private bool _weaponEquipped = false;
    private AnimationStateController _animStateController;

    #endregion
    
    #region Getters&Setters
    public Vector2 Movement
    {
        get { return _movement; }
        set { _movement = value; }
    }
    public Camera Camera
    {
        get { return _camera; }
        set { _camera = value; }
    }

    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animStateController = GetComponent<AnimationStateController>();
        _animStateController.Animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        _movementMagnitude = _movement.magnitude;
        Vector3 movementDirection = _movement.x * transform.right + _movement.y * transform.forward;
        _rigidbody.AddForce(movementDirection.normalized * (_speed - _croachSpeed));
    }

    public void DodgeMove()
    {
        Vector3 jumpDirection = Vector3.up * _jumpForce;
        Vector3 forwardDirection = transform.forward * _forwardForce;
        _rigidbody.AddForce(jumpDirection + forwardDirection, ForceMode.Impulse);
    }
    
    public void ChangeWeaponVerification()
    {
        if (_weaponEquipped)
        {
            _weaponEquipped = false;
        }
        else
        {
            _weaponEquipped = true;
            _animStateController.ChangeWeapon();
        }
        _animStateController.ChangeWeaponVerif(_weaponEquipped);
    }
}
