using UnityEngine;
using UnityEngine.AI;

public abstract class AMovement : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    protected Animator _activeAnimator;
    protected Transform _activeSpriteTransform;
    protected NavMeshAgent _agent;
    private static readonly int _speedHash = Animator.StringToHash("Speed");


    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    protected virtual void Update()
    {
        if (_activeAnimator is not null)
        {
            _activeAnimator.SetFloat(_speedHash, _agent.velocity.magnitude / _agent.speed);

            if (!_agent.isStopped)
            {
                // var angle = Vector3.SignedAngle(_activeSpriteTransform.up, _agent.velocity, Vector3.forward);
                // _activeSpriteTransform.Rotate(0f, 0f, angle);
                RotateTowards(_agent.velocity);
            }
        }
    }

    public void RotateTowards(Vector2 direction)
    {
        if (direction == Vector2.zero) return; // Evita errores si la dirección es (0, 0)

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Restamos 90° para alinear el "up"
        float currentAngle = _activeSpriteTransform.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, _rotationSpeed * Time.deltaTime);
        _activeSpriteTransform.rotation = Quaternion.Euler(0f, 0f, newAngle);
    }
    
    public void SetTarget(Transform target)
    {
        _agent.isStopped = false;
        _agent.SetDestination(target.position);
    }

    public void SetTarget(Vector3 position)
    {
        _agent.isStopped = false;
        _agent.SetDestination(position);
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
    
    public void LookAt(Transform target)
    {
        if (!_agent.isStopped) return;

        var direction = target.position - _activeSpriteTransform.position;
        LookAt(direction);
    }

    public void LookAt(Vector3 direction)
    {
        if (!_agent.isStopped) return;
        
        direction.z = 0f;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Ajustar por transform.up
        _activeSpriteTransform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}