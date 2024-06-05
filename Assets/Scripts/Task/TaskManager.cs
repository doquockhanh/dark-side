using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager
{
    public List<Task> tasks = new List<Task>();

    public void AddTask(Task task)
    {
        tasks.Add(task);
        task.OnTaskCompleted += OnTaskCompleted;
    }

    private void OnTaskCompleted(Task task)
    {
        tasks.Remove(task);
    }

    public void CheckTasks()
    {
        foreach (var task in tasks)
        {
            task.CheckCompletion();
        }
    }
}