using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : CharacterAnimator
{
    public void EnableRun()
    {
        _animator.SetBool("Run", true);
    }
}
