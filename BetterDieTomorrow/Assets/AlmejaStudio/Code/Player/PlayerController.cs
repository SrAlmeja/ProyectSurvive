using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region InputVariables

    [Header("Dedge Values")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _forwardForce;
    [Header("Movement Values")]
    [SerializeField] private float _speed;
    [SerializeField] private float _croachSpeed;
    [Header("Look Values")]
    [SerializeField] private Camera _camera;
    

    private bool _weaponEquipped = false;

    #endregion


    #region Getters&Setters
    
    public float JumpForce
    {
        get { return _jumpForce; }
        set { _jumpForce = value; }
    }
    public float ForwardForce
    {
        get { return _forwardForce; }
        set { _forwardForce = value; }
    }
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    public float CroachSpeed
    {
        get { return _croachSpeed; }
        set { _croachSpeed = value; }
    }
    public Camera Camera
    {
        get { return _camera; }
        set { _camera = value; }
    }
    
    public bool IsWeaponEquipped
    {
        get { return _weaponEquipped; }
        set { _weaponEquipped = value; }
    }

    #endregion
}
