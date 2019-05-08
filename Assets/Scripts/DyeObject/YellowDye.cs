using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowDye : DyeObject
{
    [SerializeField] Animator animator;
    private void LateUpdate()
    {
        animator.SetFloat("ChopValue", processValue);
    }
}
