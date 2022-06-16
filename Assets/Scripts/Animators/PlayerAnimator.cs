using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    private void EnableJump()
    {
        _animator.SetBool("Jump", true);
    }

    private void OnEnable()
    {
        PlayerMovement.OnFinish += EnableJump;
    }

    private void OnDisable()
    {
        PlayerMovement.OnFinish -= EnableJump;
    }
}
