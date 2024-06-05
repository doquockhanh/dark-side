using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBubController : MonoBehaviour
{
    private GameObject player;
    private TaskManager taskManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        try
        {
            taskManager = GameObject.Find("TaskHolder").GetComponent<TaskManagerHolder>().taskManager;
        }
        catch (System.Exception ex) { Debug.Log($"Missing task manager {ex.Message}"); }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player's bullet"))
        {
            player.GetComponent<Stats>().SetExp(1);
            if (taskManager != null)
            {
                List<DestroyTask> dts = taskManager.tasks.Where(t => t is DestroyTask).Select(t => t as DestroyTask).ToList();
                foreach (var dt in dts)
                {
                    dt.AddDestroyedTarget();
                }

            }
            Destroy(gameObject);
        }
    }

}
