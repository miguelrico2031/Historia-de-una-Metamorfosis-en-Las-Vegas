using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GuardSelectTarget : AGuardState
{
    private readonly Collider2D[] _possibleTargets = new Collider2D[15];
    private SlotMachine _slotTarget;
    private Vector3 _target;

    public GuardSelectTarget(GuardController controller) : base(controller) {}

    public override void Begin(AGuardState lastState = null)
    {
        var count = Physics2D.OverlapCircleNonAlloc(_controller.transform.position, _controller.TargetRange, _possibleTargets,
            _controller.TargetLayers);

        if (count == 0)
        {
            SelectPlayerSlotAsTarget();
            return;
        }

        SlotMachine slot;

        List<SlotMachine> notInBufferTargets = new();
        for (int i = 0; i < count; i++)
        {
            slot = _possibleTargets[i].GetComponent<SlotMachine>();
            if (!_controller.IsInBuffer(slot)) 
                notInBufferTargets.Add(slot);
        }

        if (!notInBufferTargets.Any())
        {
            SelectPlayerSlotAsTarget();
            return;
        }

        slot = notInBufferTargets[Random.Range(0, notInBufferTargets.Count)];
        _slotTarget = slot;

        _controller.AddToBuffer(slot);
        _target = _slotTarget.GetClosestTarget(_controller.transform.position).position;
        StartMovement();
    }

    private void SelectPlayerSlotAsTarget()
    {
        // var pos = _controller.transform.position + _controller.TargetRange * 1.5f * (Vector3)Random.insideUnitCircle;
        //
        // NavMesh.SamplePosition(pos, out var hit, .3f, NavMesh.AllAreas);
        // _target = hit.position;
        // _slotTarget = null;
        var player = _controller.GetPlayer();
        _slotTarget = player.LastSlot;

        _controller.ClearBuffer();
        _controller.AddToBuffer(_slotTarget);
        _target = _slotTarget.GetClosestTarget(_controller.transform.position).position;
        StartMovement();
    }

    private void StartMovement()
    {
        _controller.SetState(new GuardMoveToTarget(_controller, _target, _slotTarget));
    }
}