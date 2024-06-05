using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public Transform player;
    public Transform bullet;
    private Camera mainCamera;
    private float originalSize;
    public float maxSize = 27f;
    public float zoomSpeed = 5f;
    public bool isDisable = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = Camera.main;
        originalSize = mainCamera.orthographicSize;
    }

    void FixedUpdate()
    {
        if (isDisable)
            return;

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("player's bullet");
        if (bullets.Length == 0)
            bullet = null;
        else
            bullet = bullets[0].transform;

        if (bullet == null)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, originalSize, zoomSpeed * Time.deltaTime);
            return;
        }

        float dh = Mathf.Abs(player.position.y - bullet.position.y);
        float dw = Mathf.Abs(player.position.x - bullet.position.x);

        float size = Mathf.Max(dh, dw) * 1.1f;

        if (size > originalSize && size > mainCamera.orthographicSize)
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, Mathf.Min(size, maxSize), zoomSpeed * Time.deltaTime);
    }
}
