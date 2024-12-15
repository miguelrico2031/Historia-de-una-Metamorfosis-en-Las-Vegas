public class PlayerAwaitJackpot : APlayerState
{
    private SlotMachine _slot;
    public PlayerAwaitJackpot(SlotMachine slot, PlayerController controller) : base(controller)
    {
        _slot = slot;
    }

    public override void Begin(APlayerState lastState = null)
    {
        _slot.StartSpinning();
    }

    public override void Update(float deltaTime)
    {
        if (_slot.HasJackpot)
        {
            var money = _slot.CollectJackpot();
            UIManager.Instance.AddMoney(money);
            _controller.SetState(new PlayerIdle(_controller));
        }
    }
    public override void Exit(APlayerState nextState = null) {}
    
    
    
}