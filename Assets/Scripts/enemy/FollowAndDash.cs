using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowAndDash : MonoBehaviour
{
    public float followSpeed = 1f;
    public float approachDistance = 15f;
    public float dashSpeed = 10f;
    public float dashDistance = 5f;
    public float knockbackForce = 20f;
    public float dashCooldown = 3f;
    public int hitPerDash = 0;

    private Transform player;
    private bool isApproaching = false; //tiep can
    private bool isDashing = false; // lao vao
    private bool isOnCooldown = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isDashing || isOnCooldown) return;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= approachDistance)
        {
            isApproaching = true;
        }

        if (isApproaching)
        {
            // Di chuyển về phía người chơi
            transform.position = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);


            // Nếu kẻ địch đang tiếp cận, di chuyển về phía người chơi
            if (isApproaching && !isDashing)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);

                // Nếu khoảng cách đủ gần, bắt đầu lao
                if (distanceToPlayer <= dashDistance)
                {
                    StartCoroutine(DashTowardsPlayer());
                }
            }
        }
    }

    private IEnumerator DashTowardsPlayer()
    {
        isDashing = true;

        Vector2 dashDirection = (player.position - transform.position).normalized;
        float dashTime = dashDistance * 2 / dashSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < dashTime)
        {
            transform.position += (Vector3)dashDirection * dashSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        hitPerDash = 0;
        isApproaching = false;
        isOnCooldown = true;

        yield return new WaitForSeconds(dashCooldown);

        isOnCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isDashing && collider.transform.CompareTag("Player"))
        {
            if (++hitPerDash > 1) return;
            Stats targetStats = collider.gameObject.transform.GetComponent<Stats>();
            Stats thisStats = transform.GetComponent<Stats>();
            targetStats.TakeDamage(thisStats.damage);
            
            if (player.TryGetComponent<Rigidbody2D>(out var playerRb))
            {
                Vector2 knockbackDirection = (player.position - transform.position).normalized;
                StartCoroutine(KnockbackPlayer(playerRb, knockbackDirection));
            }
        }
    }

    private IEnumerator KnockbackPlayer(Rigidbody2D playerRb, Vector2 knockbackDirection)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 0.2f)
        {
            playerRb.MovePosition(playerRb.position + knockbackForce * Time.deltaTime * knockbackDirection);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
