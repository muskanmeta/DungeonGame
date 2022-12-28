using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : Enemy
{
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    
    }

    protected virtual void Update()
    {
        if (chasing)
        {
            animator.SetBool("isMoving",true);
            if(collidingWithPlayer)
                animator.SetBool("isMoving", false);
        }
        else if (Vector3.Distance(transform.position, startPosition) < 0.06f)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
