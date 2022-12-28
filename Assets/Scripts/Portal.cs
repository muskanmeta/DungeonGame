using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    public string[] scenes;

    protected override void OnCollide(Collider2D coll){
     
         if(coll.name == "Player"){
            GameManager.instance.SaveState();
            Debug.Log("Open Portal");
            UnityEngine.SceneManagement.SceneManager.LoadScene(scenes[0]);
         }
    }
}
