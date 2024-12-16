using UnityEngine;

public class PlayerMoveToSlot : APlayerState
{
    private SlotMachine _slot;
    private PlayerMovement _movement;
    private float _counter;
    private Vector3 _target;
    public PlayerMoveToSlot(PlayerController controller, SlotMachine slot) : base(controller)
    {
        _slot = slot;
        _counter = 0f;
    }

    public override void Begin(APlayerState lastState = null)
    {
        SlotMachineManager.Instance.OnSlotClicked += OnSlotClicked;

        
        _movement = _controller.GetComponent<PlayerMovement>();
        _movement.CancelMovement();
        var target = _slot.GetClosestTarget(_controller.transform.position);
        if(_slot.CurrentState is SlotMachine.State.Enabled)
            _slot.PlayerTarget(target);
        _movement.SetTarget(target);
        _target = target.position;
    }

    public override void Update(float deltaTime)
    {
        _counter += deltaTime;
        if (_counter < .1f) return;
        _counter = 0f;
        if (!_movement.HasReachedTarget()) return;
        if (Vector2.Distance(_controller.transform.position, _target) > .1f) return;
        
        //estado de ponerse en tragaperras
        _movement.CancelMovement();
        SlotMachineManager.Instance.OnSlotClicked -= OnSlotClicked;
        
        if(_slot.CurrentState is SlotMachine.State.Disabled)
            _controller.SetState(new PlayerIdle(_controller));
        else
            _controller.SetState(new PlayerAwaitJackpot(_slot, _controller));
    }
    
    private void OnSlotClicked(SlotMachine slot)
    {
        if (slot == _slot) return;
        SlotMachineManager.Instance.OnSlotClicked -= OnSlotClicked;
        _controller.SetState(new PlayerMoveToSlot(_controller, slot));
    }
    
}