public abstract class State<T>
{
    protected readonly T Owner;

    protected State(T owner)
    {
        Owner = owner;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}