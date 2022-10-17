using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ֻ��� Character class
/// </summary>
public abstract class Character : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody rigid;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }
}