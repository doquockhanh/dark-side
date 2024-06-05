using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManagerHolder : MonoBehaviour
{
    public TaskManager taskManager;
    void Awake()
    {
        taskManager = new();
    }
}
