using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    public float moveSpeed = 1f;
    private float speedUp = 1f;
    public float range = 20f;
    public bool movingRight = true;
    private float defaultPosition;
    public float minFlipTime = 1f;
    public float maxFlipTime = 5f;
    private float flipTimer;
    public float stopDelay = 1f;
    private float stopTimer;
    private Attack attack;
    private Animator animator;
    void Start()
    {
        movingRight = false;
        defaultPosition = transform.position.x;
        flipTimer = Random.Range(minFlipTime, maxFlipTime);

        attack = gameObject.GetComponent<Attack>();

        TryGetComponent<Animator>(out animator);
    }

    void FixedUpdate()
    {
        if (attack != null && attack.attacking)
        {
            stopDelay = 0.25f;
            speedUp = 3f;
        }
        else
        {
            stopDelay = 1f;
            speedUp = 1f;
        }


        if (stopTimer > 0)
        {
            animator?.SetBool("walking", false);
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

        animator?.SetBool("walking", true);
        if (movingRight)
        {
            transform.Translate(moveSpeed * speedUp * Time.deltaTime * Vector2.right);
            if (transform.position.x > defaultPosition + range)
            {
                stopTimer = stopDelay;
            }
        }
        else
        {
            transform.Translate(moveSpeed * speedUp * Time.deltaTime * Vector2.left);
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

