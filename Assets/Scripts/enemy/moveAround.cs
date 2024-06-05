using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float range = 20f;
    public bool movingRight = true;
    private float defaultPosition;
    public float minFlipTime = 1f;
    public float maxFlipTime = 5f;
    private float flipTimer;
    public float stopDelay = 1f;
    private float stopTimer;

    void Start()
    {
        movingRight = false;
        defaultPosition = transform.position.x;
        flipTimer = Random.Range(minFlipTime, maxFlipTime);
    }

    void Update()
    {
        if (stopTimer > 0)
        {
            stopTimer -= Time.deltaTime;
            if (stopTimer <= 0)
            {
                Flip();
            }
            return;
        }

        flipTimer -= Time.deltaTime;

        if (flipTimer <= 0)
        {
            flipTimer = Random.Range(minFlipTime, maxFlipTime);
            stopTimer = stopDelay;
        }

        if (movingRight)
        {
            transform.Translate(moveSpeed * Time.deltaTime * Vector2.right);
            if (transform.position.x > defaultPosition + range)
            {
                stopTimer = stopDelay;
            }
        }
        else
        {
            transform.Translate(moveSpeed * Time.deltaTime * Vector2.left);
            if (transform.position.x < defaultPosition - range)
            {
                stopTimer = stopDelay;
            }
        }
    }

    public void Flip()
    {
        movingRight = !movingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}

