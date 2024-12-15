public abstract class AGuardState
{
    protected GuardController _controller;
    protected AGuardState(GuardController controller)
    {
        _controller = controller;
    }
    public virtual void Begin(AGuardState lastState = null) {}
    public virtual void Update(float deltaTime) {}
    public virtual void Exit(AGuardState nextState = null) {}
}