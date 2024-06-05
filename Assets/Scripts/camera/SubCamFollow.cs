using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCamFollow : MonoBehaviour
{
    private Transform target;
    public float smoothSpeed = 0.005f;
    public Vector3 offset = new(0f, 2f, -10f);

    void LateUpdate()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("player's bullet");
        if (bullets.Length == 0)
            return;
        else
            target = bullets[0].transform;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
