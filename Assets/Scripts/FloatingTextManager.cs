using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

     List<FloatingText> floatingTexts = new List<FloatingText>();
    
    private void Update()
    {
        foreach(FloatingText txt in floatingTexts)
        txt.UpdateFloatingText();
    }
    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();
        floatingText.ourText.text = msg;
        floatingText.ourText.fontSize = fontSize;
        floatingText.ourText.color = color;
        floatingText.gameObj.transform.position = Camera.main.WorldToScreenPoint(position) ;
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }
    public FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.active);
        if (txt == null)
        {
            txt = new FloatingText();
            txt.gameObj = Instantiate(textPrefab);
            txt.gameObj.transform.SetParent(textContainer.transform);
            txt.ourText = txt.gameObj.GetComponent<Text>();

            floatingTexts.Add(txt);
        }
        return txt;
    }
}
