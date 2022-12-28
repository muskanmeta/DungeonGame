using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FloatingText
{
    public bool active;
    public GameObject gameObj;
    public Text ourText;
    public Vector3 motion;
    public float duration;
    public float lastShownTime;

public void Show (){
    active = true;
    lastShownTime = Time.time;
    gameObj.SetActive(active);
}

public void Hide (){
    active = false;
    gameObj.SetActive(active);
}

 public void UpdateFloatingText()
    {
        if (!active)
            return;
        
        if(Time.time - lastShownTime > duration)
            Hide();
        
        gameObj.transform.position += motion * Time.deltaTime;
    }
}
