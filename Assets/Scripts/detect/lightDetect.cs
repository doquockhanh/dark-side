using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightDetect : MonoBehaviour
{
    private GameObject player;
    public LayerMask obstacleMask;
    public List<Light2D> spotlights = new();

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        foreach (Light2D light in FindObjectsOfType<Light2D>())
        {
            // chi chap nhan cac loai point light
            if (light.lightType == Light2D.LightType.Point)
            {
                spotlights.Add(light);
            }
        }
    }

    private void FixedUpdate()
    {
        CheckPlayerInSpotlight();
    }

    private void CheckPlayerInSpotlight()
    {
        foreach (Light2D light in spotlights)
        {
            if(light == null || light.transform == null) {
                continue;
            }

            float spotlightRadius = light.pointLightOuterRadius;

            // Calculate direction from spotlight to player
            Vector2 direction = player.transform.position - light.transform.position;
            float distance = Vector2.Distance(light.transform.position, player.transform.position);

            if (distance > spotlightRadius)
            {
                player.GetComponent<PlayerController>().detected = false;
                continue;
            }

            RaycastHit2D hit = Physics2D.Raycast(light.transform.position, direction, distance, obstacleMask);

            if (hit.collider == null)
            {
                player.GetComponent<PlayerController>().detected = true;
                return;
            }
        }
    }
}
