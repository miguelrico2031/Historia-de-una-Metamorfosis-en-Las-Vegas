
public abstract class APlayerState
{
    protected PlayerController _controller;
    protected APlayerState(PlayerController controller)
    {
        _controller = controller;
    }
    public virtual void Begin(APlayerState lastState = null) {}
    public virtual void Update(float deltaTime) {}
    public virtual void Exit(APlayerState nextState = null) {}
}
