using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    public GameObject throwBy;
    private float damage = 1;

    void Update()
    {
        if (throwBy != null && throwBy.transform != null)
        {
            damage = throwBy.transform.GetComponent<Stats>().damage;
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
