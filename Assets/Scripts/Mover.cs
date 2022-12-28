using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    private Vector3 dir;
    private RaycastHit2D hit;

    protected float xSpeed = 1.0f;
    protected float ySpeed = 0.75f;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    protected virtual void UpdateMotor(Vector3 input)
    {

        //reset delta movement
        dir = new Vector3(input.x* xSpeed, input.y* ySpeed , 0);
        //direction for the player 0 (in which direction the player is looking)
        if (dir.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (dir.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //add pushforce, if any
        dir += pushDirection;

        //reduce the pushforce based off recovery speed of the fighter 
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector3(dir.x, dir.y, 0), Mathf.Abs(Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //Move the player
            transform.Translate(dir.x * Time.deltaTime, dir.y * Time.deltaTime, 0);
        }


    }
}
