using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lv1Tasks : MonoBehaviour
{
    private Text missionText1;
    private Text missionText2;
    private Text missionText3;
    public float posX = 0;
    public float posY = 0;
    private TaskManager tm;
    private KillTask task1;
    private DestroyTask task2;
    public int enemyToKill = 5;
    public int lightBubToDestroy = 5;

    void Start()
    {
        GameObject[] missionTexts = GameObject.FindGameObjectsWithTag("missionText");
        missionText1 = missionTexts[^1].GetComponent<Text>();
        missionText2 = missionTexts[^2].GetComponent<Text>();
        missionText3 = missionTexts[^3].GetComponent<Text>();
        try
        {
            tm = GameObject.Find("TaskHolder").GetComponent<TaskManagerHolder>().taskManager;
        }
        catch (Exception ex) { Debug.Log($"Missing task manager {ex.Message}"); }

        if (tm == null)
            return;


        task1 = new("Kill 5 Enemies", enemyToKill);
        task1.OnKilled += OnKilled;
        tm.AddTask(task1);
        task2 = new("Destroy 4 light bubs", lightBubToDestroy);
        task2.OnDestroyTarget += OnDestroyTarget;
        tm.AddTask(task2);

        missionText1.text = $"{task1.TaskName} {task1.EnemiesKilled}/{task1.EnemiesToKill}";
        missionText2.text = $"{task2.TaskName} {task2.TargetDestroyed}/{task2.TargetToDestroy}";
    }

    private void OnKilled(KillTask task)
    {
        if (task1.IsCompleted)
            missionText1.text = $"{task1.TaskName} {task1.EnemiesKilled}/{task1.EnemiesToKill} DONE";
        else
            missionText1.text = $"{task1.TaskName} {task1.EnemiesKilled}/{task1.EnemiesToKill}";

        CheckAllDone();
    }

    private void OnDestroyTarget(DestroyTask task)
    {
        if (task2.IsCompleted)
            missionText2.text = $"{task2.TaskName} {task2.TargetDestroyed}/{task2.TargetToDestroy} DONE";
        else
            missionText2.text = $"{task2.TaskName} {task2.TargetDestroyed}/{task2.TargetToDestroy}";
        CheckAllDone();
    }

    private void CheckAllDone()
    {
        if (tm.tasks.Count <= 0)
        {
            missionText3.text = "All task done! Go to escape position to finish this level.";
            missionText3.color = Color.red;
        }
    }
}
