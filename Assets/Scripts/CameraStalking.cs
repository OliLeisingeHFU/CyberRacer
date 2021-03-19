using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStalking : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 transSpeed = Vector3.zero;

    void FixedUpdate()
    {
        handleTranslation();
        handleRotation();
    }

    private void handleTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref transSpeed, smoothTime);
    }

    private void handleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime / smoothTime);
    }
}
