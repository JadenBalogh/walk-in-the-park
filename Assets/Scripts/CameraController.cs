using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private Transform target;

    private Vector2 currVelocity;

    private void FixedUpdate()
    {
        Vector3 targetPos = Vector2.SmoothDamp(transform.position, target.position, ref currVelocity, smoothTime);
        transform.position = targetPos + transform.position.z * Vector3.forward;
    }
}
