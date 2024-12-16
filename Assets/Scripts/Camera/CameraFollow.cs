
using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _target;

    private Vector3 _offset;
    private void Awake()
    {
        _offset = Vector3.forward * transform.position.z;
    }

    private void LateUpdate()
    {
        Vector3 targetpos = _target.position + _offset;
        transform.position = Vector3.MoveTowards(transform.position, targetpos, _speed * Time.deltaTime);
    }

    public void ForceLockTarget()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
    }
}
