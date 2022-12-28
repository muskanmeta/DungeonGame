using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int chestValue = 5;
    public int index;


    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        string chestOpen = PlayerPrefs.GetString(index.ToString());
        if (chestOpen == "true")
        {
            spriteRenderer.sprite = emptyChest;
            collected = true;
        }
    }
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            spriteRenderer.sprite = emptyChest;
            GameManager.instance.moneyAmount += chestValue;
            GameManager.instance.ShowText("+" + chestValue + " coins", 25, Color.yellow, transform.position, Vector3.up * 25, 1.5f);
        }
    }

    public void SaveChestState()
    {
        if (collected)
        {
            Debug.Log("saved chest update");
            PlayerPrefs.SetString(index.ToString(), "true");
        }
        else
        {
            Debug.Log("not saved");
            return;
        }

    }

}
