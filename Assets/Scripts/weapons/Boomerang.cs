using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform centerPoint;
    public float weaponPercentDamage = 1.5f;
    private bool inHighest = false;
    private bool flyBack = false;
    public float lifeTime = 10f;
    private float fallingTime = 0.3f;
    private float flyBackAngle = 0f;
    public float angle = -10f;
    private Stats playerStats;
    private GameObject explosionPrefap;
    public float explosionRange = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        explosionPrefap = Resources.Load<GameObject>("Explosion");
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        Rotate();
    }

    void FixedUpdate()
    {
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
            DestroyThis();
        }

        if (other.gameObject.CompareTag("enemy"))
        {
            Stats targetStats = other.gameObject.transform.GetComponent<Stats>();
            targetStats.TakeDamage(playerStats.damage * weaponPercentDamage);

            DestroyThis();
        }
    }

    private void DestroyThis()
    {
        GameObject expolosion = Instantiate(explosionPrefap, transform.position, Quaternion.identity);
        Vector3 scale = expolosion.transform.localScale;
        expolosion.transform.localScale = new Vector3(scale.x * explosionRange, scale.y * explosionRange, 1);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.explosion);
        Destroy(gameObject);
    }

}
