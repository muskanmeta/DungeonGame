using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : Enemy
{

    private Animator animator;
    private Animator alert_anim;

    private bool attack;
    public float attackRate;

    GameObject wallAA;
    GameObject wallBB;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        alert_anim = transform.Find("Alert_state").GetComponent<Animator>();
        wallAA = GameObject.Find("wallA");
        wallBB = GameObject.Find("wallB");
    }

    void Update()
    {
        if (attack)
        {
            if (Time.time - attackRate > 1f)
            {
                attackRate = Time.time;
                animator.SetTrigger("EnemySwing");
            }
        }

        if (chasing)
        {
            alert_anim.SetBool("on", true);
            if (collidingWithPlayer)
                alert_anim.SetBool("on", true);
        }
        else if (Vector3.Distance(transform.position, startPosition) < 0.04f)
        {
            alert_anim.SetBool("on", false);
        }
        else
        {
            alert_anim.SetBool("on", false);
        }

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Vector2.Distance(playerTransform.position, transform.position) < 0.3f)
        {
            attack = true;
            Debug.Log(attack);
        }
        else
        {
            attack = false;
        }

        float routeX = startPosition.x - transform.position.x;

        if (!chasing)
        {
            bool right = false;
            boxCollider.OverlapCollider(filter, base.hits);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i] == null)
                    continue;

                if (hits[i].name == "wallB")
                {
                    right = false;
                    Debug.Log(wallAA.transform.position);
                    if(!right)
                    UpdateMotor((wallAA.transform.position - transform.position));
                }
                else if (hits[i].name == "wallA")
                {
                    right = true;
                    Debug.Log(wallBB.transform.position);
                    if(right)
                    UpdateMotor((wallBB.transform.position - transform.position));
                }
                
              
                //Clean the array manually
                hits[i] = null;
            }

        }
    }
}
