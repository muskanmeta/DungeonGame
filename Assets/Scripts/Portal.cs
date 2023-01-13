using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    float textCoolDown = 5.0f;
    float lastFloatingText = -5.0f;
    public int portalLevel;
  

    protected override void OnCollide(Collider2D coll){
     
         if(coll.name == "Player"){
            if (GameManager.instance.GetCurrentLevel() == portalLevel)
            {
                 GameManager.instance.SaveState();
                Debug.Log("Open Portal");
                UnityEngine.SceneManagement.SceneManager.LoadScene(GameManager.instance.scenes[portalLevel-1]);
            }
            else if (GameManager.instance.GetCurrentLevel() < portalLevel)
            {  if(Time.time - lastFloatingText > textCoolDown)
                {
                    lastFloatingText = Time.time;
                    GameManager.instance.ShowText("You need more experience, fight more enemies!", 25, Color.yellow, transform.position, Vector3.up * 25, 1.5f);
                }

            }
            else if (portalLevel < GameManager.instance.GetCurrentLevel())
            {
                Debug.Log("Open Portal");
                UnityEngine.SceneManagement.SceneManager.LoadScene(GameManager.instance.scenes[portalLevel-1]);
            }

        }
    }
}
