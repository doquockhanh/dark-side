using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class VictoryMenu : MonoBehaviour
{
    public GameObject victoryMenuUI;
    public Text text;
    public InputField nameInputField;
    public Button okButton;
    private void Start()
    {
        victoryMenuUI.SetActive(false);
        text.gameObject.SetActive(false);
        nameInputField.gameObject.SetActive(false);
        okButton.gameObject.SetActive(false);
        okButton.onClick.AddListener(SaveRecord);
    }

    public void ShowVictoryMenu()
    {
        victoryMenuUI.SetActive(true);

        string message = $"Congratulation! You've succesfully completed the game. Thank you for playing, hope you enjoy the game.\n\nNow Save your achivement into leaderboard.\nPlease Enter you name:";
        text.gameObject.SetActive(true);
        text.text = message;

        nameInputField.gameObject.SetActive(true);
        okButton.gameObject.SetActive(true);
    }

    private void SaveRecord()
    {
        string playerName = nameInputField.text;
        PlayerManager.Instance.name = playerName;
        PlayerManager.Instance.SavePlayerName();
        Stats stats = PlayerManager.Instance.playerStats;
        float gr = stats.damage * 10 + stats.maxHeath * 5 + stats.heath * 3;

        Record record = new()
        {
            name = PlayerManager.Instance.name,
            gr = gr + "",
            level = $"{stats.lv},{stats.exp}" 
        };
        LeaderBoard.Instance.WriteRecordToFile(record);

        GameManager.Instance.Resume();
        SceneManager.LoadScene("LeaderBoard");
    }
}