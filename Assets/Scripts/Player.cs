using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    public int playerSkin = 0;
    Vector3 pos;
    public bool dead;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (dead)
        {
            transform.position = pos;
        }
    }
    private void FixedUpdate()
    { 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }
    public void UpdatePlayerSkin(int level)
    {
        playerSkin = level;
        spriteRenderer.sprite = GameManager.instance.playerSprites[playerSkin];
    }
    public void LevelUp()
    {
        maxHitpoint++;
        hitpoint = maxHitpoint;
    }
    public void LoadPlayer(int level)
    {
        UpdatePlayerSkin(level);
    }
    public void Heal(int HealAmount)
    {
        if (hitpoint == maxHitpoint)
            return;

        hitpoint += HealAmount;
        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;

        GameManager.instance.floatingTextManager.Show("+" + HealAmount, 25, Color.green, transform.position, Vector3.up * 25, 1.5f);

    }

    protected override void Death()
    {
        StartCoroutine("Respawn");
    }

     IEnumerator Respawn()
    {
       pos = transform.position;
        int count = 5;
        while (count > 0)
        {
            dead = true;
            GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().text = "You are dead. Respawing in " + count;
            GameManager.instance.player.GetComponent<BoxCollider2D>().enabled = false;
            count--;
            yield return new WaitForSeconds(1.0f);
        }

        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().text = "";
        dead = false;
        GameManager.instance.player.hitpoint = GameManager.instance.player.maxHitpoint;
        GameManager.instance.player.GetComponent<BoxCollider2D>().enabled = true;
        GameManager.instance.player.gameObject.transform.position = GameObject.Find("RespawnPoint").transform.position;


    }
}
