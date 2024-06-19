using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        GameManager.Instance.Resume();
        isPaused = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        GameManager.Instance.Pause();
        isPaused = true;
    }

    public void ReplayLevel()
    {
        GameManager.Instance.Resume();
        GameManager.Instance.ResumeGame();
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.Resume();
        SceneManager.LoadScene("MainMenu");
    }
}
