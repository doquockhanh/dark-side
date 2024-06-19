using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private Stats playerStats;
    private List<MonoBehaviour> scriptsToDisable = new();

    public void SaveGame()
    {
        PlayerManager.Instance.SavePlayerStats();
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
            PlayerManager.Instance.playerName = "Unknow PLayer";
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

    public void Pause()
    {
        Time.timeScale = 0f;
        if (scriptsToDisable.Count == 0) AddDisableScript();
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            if (script == null) continue;
            script.enabled = false;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        if (scriptsToDisable.Count == 0) AddDisableScript();
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            if (script == null) continue;
            script.enabled = true;
        }
    }

    void AddDisableScript()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        scriptsToDisable.Add(player.GetComponent<Move_jump>());
        scriptsToDisable.Add(player.GetComponent<UseBoomerang>());
        scriptsToDisable.Add(player.GetComponent<UseGun>());
    }
}
