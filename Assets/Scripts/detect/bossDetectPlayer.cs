using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDetectPlayer : MonoBehaviour
{
    private GameObject player;
    private Transform bullet;
    private Transform detectPoint;
    public float enemyDetectbulletRange = 15f;
    public float detectRange = 45f;

    void Start()
    {
        detectPoint = transform.Find("detectPoint");;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        DetectPlayer();
        DetectPlayerByBullet();
    }

    void DetectPlayer()
    {
        float em_playerDistance = Vector2.Distance(detectPoint.position, player.transform.position);
        Debug.Log(em_playerDistance);
        if (em_playerDistance <= detectRange)
        {
            // enemy who can see player
            // can attack *count time
            GetComponent<Attack>().SetCount(2);
        }

    }

    void DetectPlayerByBullet()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("player's bullet");
        if (bullets.Length == 0)
            return;
        else
            bullet = bullets[0].transform;

        float distance = Vector2.Distance(detectPoint.position, bullet.position);
        if (distance < enemyDetectbulletRange)
        {
            GetComponent<Attack>().SetCount(1);
        }
    }
}
