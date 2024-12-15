
public class PlayerIdle : APlayerState
{
    public PlayerIdle(PlayerController controller) : base(controller)
    {
        
    }

    public override void Begin(APlayerState lastState = null)
    {
        SlotMachineManager.Instance.OnSlotClicked += OnSlotClicked;
    }
    public override void Update(float deltaTime) {}
    public override void Exit(APlayerState nextState = null) {}


    private void OnSlotClicked(SlotMachine slot)
    {
        SlotMachineManager.Instance.OnSlotClicked -= OnSlotClicked;
        _controller.SetState(new PlayerMoveToSlot(_controller, slot));
    }
}