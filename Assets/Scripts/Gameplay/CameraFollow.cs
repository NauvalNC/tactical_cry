using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float lerpTime = 0.1f;
    public float clampPos = 0f;

    private void Update()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 clamped = new Vector3(Mathf.Clamp(targetPos.x, -clampPos, clampPos), targetPos.y, targetPos.z);
        transform.position = Vector3.Lerp(transform.position, clamped, lerpTime);
    }
}
