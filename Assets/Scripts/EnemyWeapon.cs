using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Collidable
{
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6 };
    public float[] pushForce = { 1.3f, 2.2f, 2.5f, 3.0f, 3.3f, 3.8f };
    public int weaponLevel;

    protected override void OnCollide(Collider2D col)
    {
        if (col.name == "Player")
        {
            Damage dmg = new Damage()
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            col.SendMessage("RecieveDamage", dmg);

        }
    }
}
