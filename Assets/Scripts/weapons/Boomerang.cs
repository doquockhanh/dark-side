using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform centerPoint;
    private bool inHighest = false;
    private bool flyBack = false;
    public float lifeTime = 10f;
    private float fallingTime = 0.3f;
    private float flyBackAngle = 0f;
    public float angle = -10f;
    private Stats playerStats;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        Rotate();
        if (inHighest)
        {
            if (fallingTime > 0)
            {
                fallingTime -= Time.deltaTime;
            }
            else
            {
                FlyBack();
            }

            return;
        }

        Vector2 direction = rb.velocity.normalized;
        if (direction.y < 0)
            inHighest = true;
    }

    void Rotate()
    {
        transform.RotateAround(centerPoint.position, Vector3.forward, 1000f * Time.deltaTime);
    }

    void FlyBack()
    {

        if (!flyBack)
        {
            flyBack = true;
            rb.gravityScale = 0;
            if (rb.velocity.normalized.x < 0)
                flyBackAngle = angle;
            else
                flyBackAngle = -180f - angle;
        }
        Vector2 force = Quaternion.Euler(0, 0, flyBackAngle) * Vector2.right;
        rb.AddForce(force, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Map") || other.gameObject.CompareTag("Object"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("enemy"))
        {
            Stats targetStats = other.gameObject.transform.GetComponent<Stats>();
            targetStats.TakeDamage(playerStats.damage);

            Destroy(gameObject);
        }
    }

}
