using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform player;


    private Vector3 _startPos, _targetPos;
    private bool _isMoving;
    private float _moveProgress;
    private void Update()
    {
        if (!_isMoving) return;

        _moveProgress = Mathf.Clamp01(_moveProgress + _moveSpeed * Time.deltaTime);

        transform.position = Vector3.Slerp(
            _startPos, _targetPos, _moveProgress
        );

        if (_moveProgress >= 1f)
        {
            _isMoving = false;
            foreach (var col in GetComponentsInChildren<Collider2D>())
                col.enabled = true;
        }
        
    }


    public void MoveToDirection(Vector3 direction)
    {
        foreach (var col in GetComponentsInChildren<Collider2D>())
            col.enabled = false;

        _startPos = transform.position;
        if (Mathf.Abs(direction.x) > 0f)
            _targetPos = new Vector3(player.position.x, transform.position.y, transform.position.z);
        else
            _targetPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
        _moveProgress = 0f;
        _isMoving = true;
    }
}