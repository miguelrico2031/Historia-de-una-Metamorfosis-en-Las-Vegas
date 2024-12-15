using UnityEngine;

public class GuardMoveToTarget : AGuardState
{
    private Vector3 _target;
    private SlotMachine _slotTarget;

    public GuardMoveToTarget(GuardController controller, Vector3 target, SlotMachine slotTarget = null) :
        base(controller)
    {
        _target = target;
        _slotTarget = slotTarget;
    }

    public override void Begin(AGuardState lastState = null)
    {
        _controller.Movement.SetTarget(_target);
    }

    public override void Update(float deltaTime)
    {
        if (!_controller.Movement.HasReachedTarget()) return;

        _controller.Movement.CancelMovement();
        _controller.SetState(new GuardLookAround(_controller));
    }
}