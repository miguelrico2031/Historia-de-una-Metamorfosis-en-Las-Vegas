
using System;
using UnityEngine;

public class CameraBound : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;
    private CameraMovement _cameraMovement;

    private void Awake()
    {
        _cameraMovement = GetComponentInParent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _cameraMovement.MoveToDirection(_direction);
        }
    }
}
