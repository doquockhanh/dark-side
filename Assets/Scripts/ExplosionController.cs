using System.Collections;
using System.Collections.Generic;
using ChineseCharacter;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public CameraShake cameraShake;
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.2f;
    private Transform player;
    void Awake()
    {
        if (Camera.main.TryGetComponent<CameraShake>(out cameraShake))
        {
            float distance = 1;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player != null) {
                distance = Vector3.Distance(player.position, transform.position);
            }

            if (distance > 5f) return;
            StartCoroutine(cameraShake.Shake(shakeDuration / distance, shakeMagnitude / distance, 0.5f));
        }
    }
}
