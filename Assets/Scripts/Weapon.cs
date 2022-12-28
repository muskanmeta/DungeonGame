using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6 };
    public float[] pushForce = { 1.3f, 2.2f, 2.5f, 3.0f, 3.3f, 3.8f };
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    public string objectName;

    //Swing the weapon
    private Animator animator;
    private float cooldown = 0.01f;
    private float lastSwing;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
    protected override void Update()
    {
        base.Update();

        if (Time.time - lastSwing > cooldown)
        {
            lastSwing = Time.time;
            if (Input.GetKeyDown("space"))
            {
                Swing();
            }
        }
    }
    private void Swing()
    {
        if(!GameManager.instance.player.dead)
            animator.SetTrigger("Swing");
    }
    protected override void OnCollide(Collider2D col)
    {
        if (col.tag == "Fighter")
        {
            if (col.name == objectName)
                return;

            Damage dmg = new Damage()
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            col.SendMessage("RecieveDamage", dmg);

        }


    }
    public void UpgradeWeapon()
    {
        Debug.Log("upgrade weapon in scene");
        weaponLevel++;
        GameManager.instance.weaponLevel = weaponLevel;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
    public void LoadWeapon(int w)
    {
        weaponLevel = w;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

    }

}
