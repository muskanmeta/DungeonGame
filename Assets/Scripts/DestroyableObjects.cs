using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObjects : Fighter
{
    public string[] dialogue;
   

    protected override void RecieveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;

            int i = Random.Range(0,dialogue.Length);
            if (dialogue.Length !=0)
            {
                GameManager.instance.ShowText(dialogue[i], 8, Color.yellow, transform.position, Vector3.up * 25, 1.5f);
            }
            else
            {
                GameManager.instance.ShowText("-", 8, Color.yellow, transform.position, Vector3.up * 25, 1.5f);
            }
            
            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }

    protected override void Death()
    {
        GameObject obj = Instantiate(GameManager.instance.Des_obj.gameObject, transform.position, Quaternion.identity);
        base.Death();
        Destroy(obj,0.3f);

    }
}
