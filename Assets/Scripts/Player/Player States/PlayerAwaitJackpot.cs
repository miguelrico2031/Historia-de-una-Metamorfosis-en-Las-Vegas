using UnityEngine;

public class PlayerAwaitJackpot : APlayerState
{
    private SlotMachine _slot;
    private PlayerMovement _movement;
    private Vector3 _direction;
    public PlayerAwaitJackpot(SlotMachine slot, PlayerController controller) : base(controller)
    {
        _slot = slot;
    }

    public override void Begin(APlayerState lastState = null)
    {
        _slot.StartSpinning();
        _movement = _controller.GetComponent<PlayerMovement>();
        _direction = _movement.GetDirectionTo(_slot.transform.position);
    }

    public override void Update(float deltaTime)
    {
        _movement.LookTowards(_direction);
        if (_slot.HasJackpot)
        {
            var money = _slot.CollectJackpot();
            UIManager.Instance.AddMoney(money);
            _controller.SetState(new PlayerIdle(_controller));
        }
    }
    public override void Exit(APlayerState nextState = null) {}
    
    
    
}