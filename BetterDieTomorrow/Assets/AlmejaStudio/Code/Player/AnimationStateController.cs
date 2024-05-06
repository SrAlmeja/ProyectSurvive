using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public Animator Animator
    {
        get { return _animator; }
        set { _animator = value; }
    }

    public void MovementAnim(float value)
    {
        _animator.SetFloat("Move", value);
    }

    public void DodgeAnim(bool activator)
    {
        _animator.SetBool("Roll", activator);
    }
    
    public void CrouchAni()
    {
        _animator.SetTrigger("Crouch");
    }

    public void ChangeWeapon()
    {
        _animator.SetTrigger("ChangeWeapon");
    }
    
    public void ChangeWeaponVerif(bool verification)
    {
        _animator.SetBool("IsWeaponEquipped", verification);
    }

    public void ShootAnim(bool activator)
    {
        _animator.SetBool("Attack", activator);
    }
}
