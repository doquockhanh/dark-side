using System.Collections;
using System.Collections.Generic;
using ChineseCharacter;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public CameraShake cameraShake;
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.2f;
    public float effectedDistance = 5f;
    private Transform player;
    private float distance = 6f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            distance = Vector3.Distance(player.position, transform.position);
        }

        if (Camera.main.TryGetComponent<CameraShake>(out cameraShake))
        {
            if (distance > effectedDistance) return;
            StartCoroutine(cameraShake.Shake(shakeDuration / distance, shakeMagnitude / distance, 0.5f));
        }

        AudioManager.Instance.PlaySFX(AudioManager.Instance.explosion);
    }
}
