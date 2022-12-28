using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;

    //Immunity
    public float immuneTime = 1f;
    public float lastImmune;
    
    protected Vector3 pushDirection;

    

    protected virtual void RecieveDamage(Damage dmg)
    {
        if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
            //floating text
            GameManager.instance.ShowText("-"+dmg.damageAmount.ToString(), 10, Color.red, transform.position, Vector3.up * 25, 1.5f);

            if(hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }
    protected virtual void Death()
    {
        Destroy(gameObject);
    }

 
}
