using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool saved = false;

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
    public List<string> newscene = new List<string>();

    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud.gameObject);
            Destroy(menuCanvas.gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoad;

    }

    //References     
    
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public GameObject hud;
    public GameObject menuCanvas;
    public GameObject Des_obj;
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public int playerSkin;
    public int moneyAmount;
    public int experience;
    public int weaponLevel;
    public string[] scenes;
    public Image health;

    //Upgrade weapon
    public bool TryUpgradeWeapon()
    { //if we reach the max weapon-level return false
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (moneyAmount >= weaponPrices[weapon.weaponLevel + 1])
        {
            moneyAmount -= weaponPrices[weapon.weaponLevel + 1];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }
    //Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;
        }

        if (r == xpTable.Count)
        { return r; }

        return r;
    }
    //How much exp we need to reach the level passed in the argument
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int exp = 0;

        while (r < level)
        {   
            exp += xpTable[r];
            r++;
        }

        return exp;
    }
    //Grant experience and leveling up
    public void GrantXp(int xp)
    {
        int currentLevel = GetCurrentLevel();
        experience += xp;
        if (currentLevel < GetCurrentLevel())
        {
            player.LevelUp();
        }
    }
    public void SetPlayerLevel(int level)
    {
        for (int i = 1; i < level; i++)
        {
            player.LevelUp();
        }
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(scenes[level-1]);
    }

    public void Update()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        if (player.hitpoint <= player.maxHitpoint)
        {
            health.fillAmount = ratio;
        }
    }

    //Save and reload collected chests
    public void SaveChests()
    {
        GameObject Chests = GameObject.Find("Chests").gameObject;
        for (int i = 0; i < Chests.transform.childCount; i++)
        {
            Chest c = Chests.transform.GetChild(i).gameObject.GetComponent<Chest>();
            c.SaveChestState();
        }
    }

    public void SaveState()
    {
        string s = playerSkin.ToString() + "|";
        s += moneyAmount.ToString() + "|";
        s += experience.ToString() + "|";
        s += weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
        SaveChests();
        Debug.Log("SaveState");
        saved = true;

    }
    private void OnSceneLoad(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
     
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;
        if (PlayerPrefs.HasKey("SaveState"))
        {

            string[] data = PlayerPrefs.GetString("SaveState").Split('|');
            playerSkin = int.Parse(data[0]);
            player.LoadPlayer(int.Parse(data[0]));
            moneyAmount = int.Parse(data[1]);
            experience = int.Parse(data[2]);
            weaponLevel = int.Parse(data[3]);
            weapon.LoadWeapon(int.Parse(data[3]));

            SetPlayerLevel(GetCurrentLevel());
            LoadLevel(GetCurrentLevel());
            Debug.Log("LoadState");

        }
        else
        {
            return;
        }

    }
}
