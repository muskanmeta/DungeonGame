using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : Collidable
{
    public float saveTime;
    protected override void OnCollide(Collider2D coll)
    {
      
            if (coll.name == "Player")
            {
                saveTime = Time.time;
                GameManager.instance.SaveState();
                
                GameManager.instance.ShowText("Progress Saved", 13, Color.green, transform.position, Vector3.up * 25, 1.2f);
            }
        
    }
}

