using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class KillTask : Task
{
    public int EnemiesToKill { get; set; }
    public int EnemiesKilled { get; set; }
    public event System.Action<KillTask> OnKilled;

    public KillTask(string name, int enemiesToKill)
    {
        TaskName = name;
        EnemiesToKill = enemiesToKill;
    }

    public void EnemyKilled()
    {
        EnemiesKilled++;
        CheckCompletion();
        OnKilled?.Invoke(this);
    }

    public override void CheckCompletion()
    {
        if (EnemiesKilled >= EnemiesToKill)
        {
            CompleteTask();
        }
    }
}