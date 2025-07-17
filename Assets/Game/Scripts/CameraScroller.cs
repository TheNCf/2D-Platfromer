using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;

    private Vector3 _velocity;

    private void LateUpdate()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        Vector3 targetPosition = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
    }
}
