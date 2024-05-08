using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region InputVariables

    private Rigidbody _rigidbodyPlayer;
    [Header("Dodge Values")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _forwardForce;
    [Header("Movement Values")]
    [SerializeField] private float _speed;
    [SerializeField] private float _croachSpeed;
    private Vector2 _movement;
    private float _movementMagnitude;
    [Header("Look Values")]
    [SerializeField] private Camera _camera;

    [Header("Shoot Values")]
    [SerializeField] private float laserSize;
    [SerializeField] private Vector3 laserOffset;
    [SerializeField] private GameObject crosshairprefab;
    [SerializeField] private GameObject _SpawnZonePrefab;
    private GameObject _crosshair;
    private GameObject _spawnZone;

    [Header("SecundaryWeapon")]
    [SerializeField] GameObject secundaryWeapon;
    private Weapon _weapon;
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
        _rigidbodyPlayer = GetComponent<Rigidbody>();
        _animStateController = GetComponent<AnimationStateController>();
        _animStateController.Animator = GetComponentInChildren<Animator>();
        _weapon = GetComponent<Weapon>();
        
        secundaryWeapon.SetActive(false);
        _crosshair = Instantiate(crosshairprefab);
        _spawnZone = Instantiate(_SpawnZonePrefab);
    }

    private void FixedUpdate()
    {
        Move();
        Laser();
    }
    
    private void Move()
    {
        _movementMagnitude = _movement.magnitude;
        Vector3 movementDirection = _movement.x * transform.right + _movement.y * transform.forward;
        _rigidbodyPlayer.AddForce(movementDirection.normalized * (_speed - _croachSpeed));
    }

    public void DodgeMove()
    {
        Vector3 jumpDirection = Vector3.up * _jumpForce;
        Vector3 forwardDirection = transform.forward * _forwardForce;
        _rigidbodyPlayer.AddForce(jumpDirection + forwardDirection, ForceMode.Impulse);
    }
    
    public void ChangeWeaponVerification()
    {
        if (_weaponEquipped)
        {
            _weapon.ChangeWeapon();
            _weaponEquipped = false;
            secundaryWeapon.SetActive(false);
            _animStateController.ChangeWeapon();
        }
        else
        {
            _weapon.ChangeWeapon();
            _weaponEquipped = true;
            secundaryWeapon.SetActive(true);
            _animStateController.ChangeWeapon();
        }
        _animStateController.ChangeWeaponVerif(_weaponEquipped);
    }

    public void Shoot()
    {
        _weapon.Shoot();
    }
    public void StopShooting()
    {
        _weapon.StopShooting();
    }
    private void Laser()
    {
        RaycastHit hit;
        Vector3 startPoint = transform.position + transform.TransformDirection(laserOffset);
        Vector3 direction = transform.forward;
        
        _spawnZone.transform.position = startPoint;

        if (Physics.Raycast(startPoint, direction, out hit, laserSize))
        {
            _crosshair.transform.position = hit.point;
            if (hit.collider.CompareTag("Buildings"))
            {
                Debug.DrawRay(startPoint, direction * hit.distance, Color.green);
            }
            else
            {
                Debug.DrawRay(startPoint, direction * hit.distance, Color.red);
            }
        }
        else
        {
            _crosshair.transform.position = startPoint + direction * laserSize;
            Debug.DrawRay(startPoint, direction * laserSize, Color.red);
        }
    }
    public void SetMovement(Vector2 movement)
    {
        _movement = movement;
    }
}
