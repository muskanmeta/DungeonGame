using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    public int[] damage = { 1, 2, 3, 4, 5, 6 };
    public float[] pushForce = { 1.0f, 2.2f, 2.5f, 3.0f, 3.3f, 3.8f };
    public int enemyLevel;

    protected override void OnCollide(Collider2D col)
    {
        if (col.name == "Player")
        {
            Damage dmg = new Damage()
            {
                damageAmount = damage[enemyLevel],
                origin = transform.position,
                pushForce = pushForce[enemyLevel]
            };

            col.SendMessage("RecieveDamage", dmg);
        }
    }
}
