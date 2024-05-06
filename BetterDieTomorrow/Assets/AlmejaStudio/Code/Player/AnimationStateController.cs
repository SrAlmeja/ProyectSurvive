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
}
