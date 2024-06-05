using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_jump : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float gravityScale = 3f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool onLadder;
    private int jumpCount;
    public int maxJumps = 2;
    private float inputHorizontal;
    private float inputVertical;

    private bool facingRight = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        Move();
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpCount < maxJumps))
        {
            Jump();
        }

        if (onLadder)
        {
            ClimbLadder();
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(inputHorizontal * moveSpeed, rb.velocity.y);

        if (inputHorizontal > 0 && !facingRight || inputHorizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce - jumpCount * 2);
        jumpCount++;
        if (isGrounded)
        {
            isGrounded = false;
        }
    }

    void ClimbLadder()
    {
        rb.velocity = new Vector2(rb.velocity.x, inputVertical * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Object") || collision.gameObject.CompareTag("Ladder"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Object") || collision.gameObject.CompareTag("Ladder"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            onLadder = true;
            rb.gravityScale = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            onLadder = false;
            rb.gravityScale = gravityScale;
        }
    }
}
