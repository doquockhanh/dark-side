using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DestroyTask : Task
{
    public int TargetToDestroy{ get; set; }
    public int TargetDestroyed { get; set; }
    public event System.Action<DestroyTask> OnDestroyTarget;

    public DestroyTask(string name, int targetToDestroy)
    {
        TaskName = name;
        TargetToDestroy = targetToDestroy;
    }

    public void AddDestroyedTarget()
    {
        TargetDestroyed++;
        CheckCompletion();
        OnDestroyTarget?.Invoke(this);
    }

    public override void CheckCompletion()
    {
        if (TargetDestroyed >= TargetToDestroy)
        {
            CompleteTask();
        }
    }
}