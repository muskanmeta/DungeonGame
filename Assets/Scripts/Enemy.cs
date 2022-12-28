using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int xpValue = 1;

    //Chasing logic
    public float triggerLength = 0.4f;
    public float chaseLength = 0.7f;
    protected bool chasing;
    protected bool collidingWithPlayer;
    protected Transform playerTransform;
    protected Vector3 startPosition;

    public string isDead = "false";
    public string enemyName;

    //hitbox: enemy will need a damage-attack mechanism to damage the player
    public BoxCollider2D hitbox;
    protected Collider2D[] hits = new Collider2D[10];
    protected ContactFilter2D filter;
    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();

        //int eHealth = PlayerPrefs.GetInt(enemyName);
        //hitpoint = eHealth;
        string enemyDead = PlayerPrefs.GetString(enemyName);
        if (enemyDead == "true")
        {
            Debug.Log(enemyName + "is dead");
            Destroy(gameObject);
        }
    }

    protected virtual void FixedUpdate()
    {
        if (Vector3.Distance(playerTransform.position, startPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startPosition) < triggerLength)
                chasing = true;

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startPosition - transform.position);
            }

        }
        else
        {
            UpdateMotor(startPosition - transform.position);
            chasing = false;
        }

        //check if the player is colliding with the enemy, if yess enemy's gonna stop chasing the player
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }
            //Clean the array manually
            hits[i] = null;
        }


    }

    //public void SaveState()
    //{

    //}

    protected override void Death()
    {
        if (isDead == "false")
        {
            isDead = "true";
            PlayerPrefs.SetString(enemyName, isDead);
            Destroy(gameObject);
            //GrandReward();
            GameManager.instance.GrantXp(xpValue);
            GameManager.instance.ShowText("+" + xpValue + " xp", 25, Color.magenta, transform.position, Vector3.up * 25, 1.5f);
        }
    }

}