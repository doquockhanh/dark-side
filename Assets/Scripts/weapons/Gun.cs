using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform centerPoint;
    public float lifeTime = 10f;
    private Stats playerStats;
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.RotateAround(centerPoint.position, Vector3.forward, 1000f * Time.deltaTime);
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
