using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    public float moveSpeed = 5f; // Tốc độ di chuyển
    public Transform centerPoint;
    private Vector2 direction;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetRandomDirection();
    }

    void FixedUpdate()
    {
        MoveObject();
    }

    void MoveObject()
    {
        // Di chuyển theo hướng hiện tại
        rb.velocity = direction * moveSpeed;
    }

    void SetRandomDirection()
    {
        float randomX = UnityEngine.Random.Range(-1f, 1f);
        float randomY = UnityEngine.Random.Range(-1f, 1f);
        if (randomX == 0 || randomY == 0){
            SetRandomDirection();
            return;
        }

        // cac toa do luon ban 1
        direction = new Vector2(randomX / math.abs(randomX), randomY /  math.abs(randomY)).normalized;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Phản lại khi đụng tường
        if (collision.gameObject.CompareTag("Map"))
        {
            Vector2 reflection = Vector2.Reflect(direction, collision.contacts[0].normal);
            direction = reflection.normalized; // Cập nhật hướng di chuyển mới
        }
    }
}
