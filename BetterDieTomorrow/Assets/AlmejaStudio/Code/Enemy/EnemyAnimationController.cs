using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
 
    public void Dead()
    {
        _animator.Play("Dead");
    }
}
