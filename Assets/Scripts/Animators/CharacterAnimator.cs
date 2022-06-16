using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class CharacterAnimator : MonoBehaviour
{
    protected Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
}
