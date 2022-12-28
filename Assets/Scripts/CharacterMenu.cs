using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{

    private Animator animator;

    //Text fields
    public Text levelText, purchaseCostText, hitpointText, xpText, moneyAmountText;

    //Logic
    public int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    public int weaponl;
    public int currLevel;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    //character selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            onPlayerSelection();
        }
        else
        {
            currentCharacterSelection--;

            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            onPlayerSelection();
        }
    }
    private void onPlayerSelection()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    }

    public void OnSelectionConfirm(bool confirm)
    {
        if (confirm)
        {
            GameManager.instance.playerSkin = currentCharacterSelection;
            GameManager.instance.player.UpdatePlayerSkin(currentCharacterSelection);
            Debug.Log("confirm player");
        }
    }

    //ugrade weapon through coins
    public void weaponPurchase()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
            Debug.Log("purchase succesfull");
        }
    }

    public void SetAnimatorBool(bool show)
    {
        animator.SetBool("show", show);
    }

    public void OnReset()
    {
        PlayerPrefs.DeleteAll();
    }

    //Update character information in the menu 
    public void UpdateMenu()
    {
        currLevel = GameManager.instance.GetCurrentLevel();
        //Weapon
        weaponl = GameManager.instance.weaponLevel;
        weaponSprite.sprite = GameManager.instance.weaponSprites[weaponl + 1];
        purchaseCostText.text = GameManager.instance.weaponPrices[weaponl + 1].ToString();

        //Meta
        levelText.text = currLevel.ToString();
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        moneyAmountText.text = GameManager.instance.moneyAmount.ToString();

        //Experience Bar
        if (currLevel == GameManager.instance.xpTable.Count)  //maximum level reached
        {
            xpText.text = GameManager.instance.experience.ToString() + "xp";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int previousXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currentXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currentXp - previousXp;

            int currentLevelExperience = GameManager.instance.experience - previousXp;

            float completionRatio = (float)currentLevelExperience / (float)diff;
            xpText.text = currentLevelExperience.ToString() + " / " + diff.ToString() + " xp";
            xpBar.localScale = new Vector3(completionRatio, 1, 0);
        }
    }

}
