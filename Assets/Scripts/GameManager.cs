using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private Stats playerStats;

    public void SaveGame()
    {
        PlayerManager.Instance.SavePlayerData();
    }

    public void LoadPlayerStats()
    {
        SavedStats stats = PlayerManager.Instance.LoadPlayerStats();
        if (stats != null)
        {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
            playerStats.lv = stats.lv;
            playerStats.exp = stats.exp;
            playerStats.heath = stats.heath;
            playerStats.maxHeath = stats.maxHeath;
            playerStats.maxExp = stats.maxExp;
            playerStats.damage = stats.damage;
        }
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();

        string name = PlayerManager.Instance.GetPlayerName();
        if (name != null)
        {
            PlayerManager.Instance.playerName = name;
        }
        else
        {
            PlayerManager.Instance.playerName = "Khanh";
            Debug.Log("show box to enter username");
        }
        SceneManager.LoadScene(0);
    }

    public void ResumeGame()
    {
        if (PlayerPrefs.HasKey("Scene"))
        {
            int scene = int.Parse(PlayerPrefs.GetString("Scene"));
            SceneManager.LoadScene(scene);
        }
        else
        {
            NewGame();
        }
    }

}
