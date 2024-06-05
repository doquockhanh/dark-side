using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    public GameObject throwBy;
    private float damage = 1;
    public bool bulletNeedRotate = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (throwBy != null && throwBy.transform != null)
        {
            damage = throwBy.transform.GetComponent<Stats>().damage;
        }

        if (!bulletNeedRotate)
        {
            return;
        }
        Vector2 velocity = rb.velocity;

        if (velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 45));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Map") || other.gameObject.CompareTag("Object"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Stats targetStats = other.gameObject.transform.GetComponent<Stats>();
            targetStats.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
