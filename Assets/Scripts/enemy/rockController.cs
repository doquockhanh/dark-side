using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    public GameObject throwBy;
    private float weaponDame = 1;
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
            damage = throwBy.transform.GetComponent<Stats>().damage * weaponDame;
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
            // DealExplosiveDamage();
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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.explosion);
        Destroy(gameObject);
    }

    private void DealExplosiveDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRange);
        foreach (Collider2D nearbyObject in colliders)
        {
            float distance = Vector2.Distance(transform.position, nearbyObject.transform.position);
            float damagePercentage = Mathf.Clamp01(1 - (distance / explosionRange));
            if (nearbyObject.transform.TryGetComponent<Stats>(out var stats))
            {
                stats.TakeDamage(damagePercentage * damage);
            }
        }
    }
}
