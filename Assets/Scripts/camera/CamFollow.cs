using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.005f;
    public Vector3 offset = new Vector3(0f, 2f, -10f);

    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
    }
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
