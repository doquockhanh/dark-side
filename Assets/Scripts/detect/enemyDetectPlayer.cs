using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDetectPlayer : MonoBehaviour
{
    private GameObject player;
    private Transform bullet;
    private Transform detectPoint;
    public float enemyDetectbulletRange = 5f;
    public LayerMask obstacleMask;

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
        bool detected = player.GetComponent<PlayerController>().detected;
        if (!detected)
        {
            return;
        }

        Vector2 em_playerDirection = player.transform.position - detectPoint.position;
        float em_playerDistance = Vector2.Distance(detectPoint.position, player.transform.position);
        RaycastHit2D em_playerHit = Physics2D.Raycast(detectPoint.position, em_playerDirection, em_playerDistance, obstacleMask);
        if (em_playerHit.collider == null)
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
