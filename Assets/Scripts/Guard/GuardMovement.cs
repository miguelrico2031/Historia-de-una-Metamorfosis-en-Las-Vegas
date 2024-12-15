
using UnityEngine;

public class GuardMovement : AMovement
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _spriteTransform;

    protected override void Awake()
    {
        base.Awake();
        _agent.speed = _speed;
        _activeSpriteTransform = _spriteTransform;
        _activeAnimator = _activeSpriteTransform.GetComponent<Animator>();
    }
}
