using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private TaskManager taskManager;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        try
        {
            taskManager = GameObject.Find("TaskHolder").GetComponent<TaskManagerHolder>().taskManager;
        }
        catch (System.Exception ex) { Debug.Log($"Missing task manager {ex.Message}"); }

    }
    public void Die()
    {
        player.GetComponent<Stats>().SetExp(1);
        if (taskManager != null)
        {
            List<KillTask> kts = taskManager.tasks.Where(t => t is KillTask).Select(t => t as KillTask).ToList();
            foreach (var kt in kts)
            {
                kt.EnemyKilled();
            }

        }

        Destroy(gameObject);
    }
}
