using UnityEngine;

public class GuardLookAround : AGuardState
{
    private float _speed;
    private float _totalRotation = 0f;
    private Quaternion _startRot;
    private bool _returning = false;
    private float _rotationThreshold;

    public GuardLookAround(GuardController controller) : base(controller)
    {
    }

    public override void Begin(AGuardState lastState = null)
    {
        _speed = _controller.LookAroundRotSpeed * (Random.value > .5f ? 1f : -1f);
        _startRot = _controller.SpriteTransform.rotation;
        _rotationThreshold = 90f;
    }

    public override void Update(float deltaTime)
    {
        float rotationThisFrame = _speed * deltaTime;
        _controller.SpriteTransform.Rotate(0f, 0f, rotationThisFrame);
        _totalRotation += Mathf.Abs(rotationThisFrame);
        if (_totalRotation >= _rotationThreshold)
        {
            if (_returning)
            {
                // _controller.SpriteTransform.rotation = _startRot;
                _controller.SetState(new GuardSelectTarget(_controller));
            }
            else
            {
                _returning = true;
                _speed *= -1f;
                _totalRotation = 0f;
                _rotationThreshold = 180f;
            }
        }
    }
}