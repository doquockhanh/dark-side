using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject rockPrefab;
    private Transform player;
    private Transform throwPoint;
    public float throwInterval = 3f;
    public float throwAngle = 35f;
    public float throwAngleDiff = 5f;
    public float throwPowerDiff = 3f;
    private int count = 0;
    private Transform hpBarTransform;
    private GameObject atkStatusPrefap;
    private GameObject atkStatusInstance;
    public bool attacking = true;

    private void Start()
    {
        throwPoint = transform.Find("throwpoint");
        player = GameObject.FindGameObjectWithTag("Player").transform;

        hpBarTransform = transform.Find("HealthBar(Clone)");
        atkStatusPrefap = Resources.Load<GameObject>("EmAttackStatus");
        atkStatusInstance = Instantiate(atkStatusPrefap, hpBarTransform.position - new Vector3(0, -1, 0), Quaternion.identity);
        atkStatusInstance.transform.SetParent(hpBarTransform);
        atkStatusInstance.transform.localScale = new Vector3(0.3f, 0.3f, 0);

        StartCoroutine(ThrowRockAtPlayer());
    }

    void FixedUpdate()
    {
        // toggle attack status
        if (count <= 0)
        {
            if (attacking)
            {
                attacking = false;
                atkStatusInstance.SetActive(false);
            }
        }
        else
        {
            if (!attacking)
            {
                attacking = true;
                atkStatusInstance.SetActive(true);
            }
        }
    }

    private IEnumerator ThrowRockAtPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(throwInterval);

            // can attack if count has value
            if (count <= 0)
                continue;
            count--;

            float _throw_angle = CalculateThrowAngle(throwAngle);
            GameObject rock = Instantiate(rockPrefab, throwPoint.position, Quaternion.identity);
            Vector3 initialVelocity = CalculateInitialVelocity(throwPoint.position, player.position, _throw_angle);

            Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
            rock.GetComponent<RockController>().throwBy = gameObject;
            if (rb != null)
            {
                rb.velocity = initialVelocity;
            }
        }
    }

    private Vector3 CalculateInitialVelocity(Vector3 start, Vector3 target, float angle)
    {
        Vector3 direction = target - start;
        float heightDifference = direction.y;
        float distance = direction.magnitude;
        float angleRad = angle * Mathf.Deg2Rad;

        float denominator = 2 * (heightDifference - distance * Mathf.Tan(angleRad)) * Mathf.Pow(Mathf.Cos(angleRad), 2);

        float velocityMagnitude = Mathf.Sqrt(Mathf.Abs(Physics.gravity.y * distance * distance / denominator));

        Vector3 velocity = direction.normalized * CalculateThrowPower(velocityMagnitude) * Mathf.Cos(angleRad);
        velocity.y = velocityMagnitude * Mathf.Sin(angleRad);

        return velocity;
    }

    private float CalculateThrowPower(float power)
    {
        float min = power > throwPowerDiff ? power - throwPowerDiff : 0;
        float max = power + throwPowerDiff;
        float calculatedPower = UnityEngine.Random.Range(min, max);
        return calculatedPower;
    }

    private float CalculateThrowAngle(float angle)
    {
        float min = angle - throwAngleDiff > 0 ? angle - throwAngleDiff : 360 - (angle - throwAngleDiff);
        float max = (angle + throwAngleDiff) % 360;
        float caculatedAngle = UnityEngine.Random.Range(min, max);
        return caculatedAngle;
    }

    public void SetCount(int count)
    {
        this.count = count;
    }
}
