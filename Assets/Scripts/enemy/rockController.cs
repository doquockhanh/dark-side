using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    public GameObject throwBy;
    private float damage = 1;
    public bool bulletNeedRotate = false;
    private Rigidbody2D rb;
    private GameObject explosionPrefap;
    public float explosionRange = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        explosionPrefap = Resources.Load<GameObject>("Explosion");
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
            ExplosionAndDestroy();
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Stats targetStats = other.gameObject.transform.GetComponent<Stats>();
            targetStats.TakeDamage(damage);

            ExplosionAndDestroy();
        }
    }

    private void ExplosionAndDestroy()
    {
        GameObject expolosion = Instantiate(explosionPrefap, transform.position, Quaternion.identity);
        Vector3 scale = expolosion.transform.localScale;
        expolosion.transform.localScale = new Vector3(scale.x * explosionRange, scale.y * explosionRange, 1);
        Destroy(gameObject);
    }
}
