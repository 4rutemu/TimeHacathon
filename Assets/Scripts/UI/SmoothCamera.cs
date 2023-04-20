using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    private Vector3 velocity = Vector3.zero;

    [Range(0, 1)] public float smoothTime;
    
    private void FixedUpdate()
    {
        Vector3 targetPos = target.position;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
