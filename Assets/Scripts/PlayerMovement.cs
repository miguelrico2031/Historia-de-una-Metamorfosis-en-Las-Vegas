
using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _humanSpeed, _bugSpeed;
    [SerializeField] private bool _startBug;
    
    private NavMeshAgent _agent;

    private float _defaultSpeed;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
        _defaultSpeed = _agent.speed;
        if(_startBug) SetBug();
        else SetHuman();
    }


    public void SetTarget(Transform target)
    {
        _agent.isStopped = false;
        _agent.SetDestination(target.position);
    }
    
    public bool HasReachedTarget()
    {
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }

        return false;
    }

    public void CancelMovement()
    {
        _agent.ResetPath();
        _agent.isStopped = true;
    }

    public void SetHuman()
    {
        _agent.speed = _defaultSpeed * _humanSpeed;
    }

    public void SetBug()
    {
        _agent.speed = _defaultSpeed * _bugSpeed;
    }
}
