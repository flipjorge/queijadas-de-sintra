public class StateMachine<T>
{
    private T _owner;
    private State<T> _currentState;
    
    public StateMachine(T owner)
    {
        _owner = owner;
    }

    public void ChangeTo(State<T> state)
    {
        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }
}