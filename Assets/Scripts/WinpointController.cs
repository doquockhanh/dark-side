using System.Collections;
using System.Collections.Generic;
using ChineseCharacter;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinpointController : MonoBehaviour
{
    private TaskManager taskManager;
    public Camera mainCamera;
    public float zoomDuration = 2.0f;
    public float targetFOV = 10.0f;

    public float requiredStayTime = 2.0f;

    private float stayTimer = 0.0f;
    private bool isPlayerStaying = false;
    void Start()
    {
        try
        {
            taskManager = GameObject.Find("TaskHolder").GetComponent<TaskManagerHolder>().taskManager;
        }
        catch (System.Exception ex) { Debug.Log($"Missing task manager {ex.Message}"); }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (isPlayerStaying)
        {
            stayTimer += Time.deltaTime;
            if (stayTimer >= requiredStayTime)
            {
                if (taskManager != null && taskManager.tasks.Count != 0)
                {
                    // Hiển thị thông báo chưa xong hết nhiệm vụ
                    return;
                }

                GoToNextLvOrWin();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mainCamera.GetComponent<Zoom>().isDisable = true;
            isPlayerStaying = true;
            if (taskManager != null && taskManager.tasks.Count == 0)
            {
                StartCoroutine(ZoomInSlowly());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mainCamera.GetComponent<Zoom>().isDisable = false;
            isPlayerStaying = false;
            stayTimer = 0.0f;
            StopCoroutine(ZoomInSlowly());
        }
    }

    private void GoToNextLvOrWin()
    {
        PlayerManager.Instance.playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        PlayerManager.Instance.SavePlayerData();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        if (currentSceneIndex < sceneCount - 1)
        {
            PlayerManager.Instance.SaveScene(currentSceneIndex + 1);
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.Log("Chiến thắng!");
        }
    }

    private IEnumerator ZoomInSlowly()
    {
        float startTime = Time.time;

        while (Time.time < startTime + zoomDuration)
        {
            if (mainCamera == null) {
                break;
            }
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 5, 5f * Time.deltaTime);
            yield return null;
        }
    }
}
