using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float weaponPercentDamage = 1f;
    public Transform centerPoint;
    public float lifeTime = 10f;
    private Stats playerStats;
    private GameObject explosionPrefap;
    public float explosionRange = 1f;
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        explosionPrefap = Resources.Load<GameObject>("Explosion");
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
