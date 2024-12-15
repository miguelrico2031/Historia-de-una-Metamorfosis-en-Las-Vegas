public class PlayerMoveToSlot : APlayerState
{
    private SlotMachine _slot;
    private PlayerMovement _movement;
    private float _counter;
    public PlayerMoveToSlot(PlayerController controller, SlotMachine slot) : base(controller)
    {
        _slot = slot;
        _counter = 0f;
    }

    public override void Begin(APlayerState lastState = null)
    {
        _movement = _controller.GetComponent<PlayerMovement>();
        _movement.CancelMovement();
        _movement.SetTarget(_slot.GetClosestTarget(_controller.transform.position));
        // _slot.Disable();
    }

    public override void Update(float deltaTime)
    {
        _counter += deltaTime;
        if (_counter < .1f) return;
        _counter = 0f;
        if (!_movement.HasReachedTarget()) return;
        
        //estado de ponerse en tragaperras
        _movement.CancelMovement();
        _movement.LookAt(_slot.transform);
        _controller.SetState(new PlayerAwaitJackpot(_slot, _controller));
    }
    public override void Exit(APlayerState nextState = null) {}
    
}