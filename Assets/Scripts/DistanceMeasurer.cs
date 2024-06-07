using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceMeasurer : MonoBehaviour
{
    public Transform pointA; // Điểm đầu tiên
    public Transform pointB; // Điểm thứ hai

    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pointA.position, pointB.position);

            float distance = Vector3.Distance(pointA.position, pointB.position);

            Vector3 midPoint = (pointA.position + pointB.position) / 2;
            UnityEditor.Handles.Label(midPoint, distance.ToString("F2"));
        }
    }
}
