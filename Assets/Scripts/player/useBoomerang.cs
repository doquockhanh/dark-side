using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBoomerang : MonoBehaviour
{
    public RectTransform powerUi;
    private readonly float powerUiMaxWidth = 400f;
    public RectTransform lastFireUi;
    private readonly float lastFireUi_pxMin = 100f;
    private readonly float lastFireUi_pxMax = 500f;

    public GameObject boomerangPrefab;
    public Transform boomerangSpawnPoint;
    public float minForce = 5f; // Lực bắn tối thiểu
    public float maxForce = 50; // Lực bắn tối đa
    public float chargeTime = 3f; // Thời gian tích lực đầy đủ
    public float currentForce;
    private bool isCharging;

    public LineRenderer lineRenderer;
    public int numberOfPoints = 20;
    public float timeBetweenPoints = 0.1f;

    private Vector2 velocity;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numberOfPoints;
    }

    void Update()
    {
        // Bắt đầu tích lực khi nhấn giữ chuột trái
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("player's bullet");
        if (bullets.Length == 0)
            ShotListening();

    }

    void FixedUpdate()
    {
        UpdateTrajectory();
    }

    void ShotListening()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            currentForce = minForce;
        }

        // Tăng lực tích khi giữ chuột trái
        if (Input.GetMouseButton(0) && isCharging)
        {
            currentForce += (maxForce - minForce) / chargeTime * Time.deltaTime;
            currentForce = Mathf.Clamp(currentForce, minForce, maxForce);

            // ve thanh luc
            powerUi.sizeDelta = new Vector2(currentForce * (powerUiMaxWidth / maxForce), powerUi.sizeDelta.y);
        }

        // Bắn đạn khi thả chuột trái
        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            isCharging = false;
            Shoot();

            // ve vi tri ban lan cuoi
            Vector2 newPos = lastFireUi.anchoredPosition;
            newPos.x = lastFireUi_pxMin + currentForce * ((lastFireUi_pxMax - lastFireUi_pxMin) / maxForce);
            lastFireUi.anchoredPosition = newPos;
        }
    }

    void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.y < boomerangSpawnPoint.position.y)
        {
            mousePosition.y = boomerangSpawnPoint.position.y;
        }
        Vector2 direction = mousePosition - boomerangSpawnPoint.position;

        GameObject boomerang = Instantiate(boomerangPrefab, boomerangSpawnPoint.position, Quaternion.identity);

        Rigidbody2D rb = boomerang.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction.x, direction.y).normalized * currentForce;
    }

    void UpdateTrajectory()
    {
        if (!isCharging)
        {
            lineRenderer.enabled = false;
            return;
        }
        else
            lineRenderer.enabled = true;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.y < boomerangSpawnPoint.position.y)
            mousePosition.y = boomerangSpawnPoint.position.y;

        Vector2 direction = mousePosition - boomerangSpawnPoint.position;
        velocity = new Vector2(direction.x, direction.y).normalized * currentForce;
        Vector2 startPos = boomerangSpawnPoint.position;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float time = i * timeBetweenPoints;
            Vector2 position = startPos + velocity * time + 0.5f * Physics2D.gravity * time * time;
            lineRenderer.SetPosition(i, position);
        }

    }
}
