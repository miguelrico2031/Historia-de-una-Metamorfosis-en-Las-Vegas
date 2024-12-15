using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : AMovement
{
    [SerializeField] private float _humanSpeed, _bugSpeed;
    [SerializeField] private Animator _bugAnimator, _humanAnimator = null;

    private float _defaultSpeed;
    protected override void Awake()
    {    
        base.Awake();
        _defaultSpeed = _agent.speed;
        SetHuman();
    }
    
    public void SetHuman()
    {
        _agent.speed = _defaultSpeed * _humanSpeed;
        _activeAnimator = _humanAnimator;
        Handletransforms();
    }

    public void SetBug()
    {
        _agent.speed = _defaultSpeed * _bugSpeed;
        _activeAnimator = _bugAnimator;
        Handletransforms();
    }
    
    private void Handletransforms()
    {
        float angle = _activeSpriteTransform is null ? 180f : _activeSpriteTransform.eulerAngles.z;
        _activeSpriteTransform = _activeAnimator.transform;
        _activeSpriteTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
}