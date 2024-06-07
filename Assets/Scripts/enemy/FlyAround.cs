using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAround : MonoBehaviour
{
    public float speed = 2f;
    public float minChangeTime = 2f;
    public float maxChangeTime = 4f;

    private Vector2 currentDirection;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
    }

    void FixedUpdate()
    {
        rb.velocity = currentDirection * speed;
    }

    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            float waitTime = Random.Range(minChangeTime, maxChangeTime);
            currentDirection = Random.insideUnitCircle.normalized;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
