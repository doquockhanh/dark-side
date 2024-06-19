using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedStats
{
    public float heath;
    public float maxHeath;
    public float damage;
    public float exp;
    public float lv;
    public float maxExp;
    public float lvDmg;
    public float lvHeath;
    public float lvExp;
}
public class PlayerManager : Singleton<PlayerManager>
{
    public Stats playerStats;
    public string playerName;

    public void SavePlayerStats()
    {
        string json = JsonUtility.ToJson(playerStats);
        PlayerPrefs.SetString("PlayerStats", json);
        PlayerPrefs.Save();
    }

    public void SavePlayerName()
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
    }

    public void SaveScene(int scene)
    {
        PlayerPrefs.SetString("Scene", scene.ToString());
    }

    public SavedStats LoadPlayerStats()
    {
        if (PlayerPrefs.HasKey("PlayerStats"))
        {
            string json = PlayerPrefs.GetString("PlayerStats");
            SavedStats data = JsonUtility.FromJson<SavedStats>(json);
            return data;
        }

        return null;
    }

    public string GetPlayerName()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            return PlayerPrefs.GetString("PlayerName");
        }
        return null;
    }
}
