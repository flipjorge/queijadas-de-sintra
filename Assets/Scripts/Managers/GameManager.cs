using UnityEngine;

public class GameManager : MonoBehaviour
{
    private StateMachine<GameManager> _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine<GameManager>(this);
    }

    private void Start()
    {
        _stateMachine.ChangeTo(new GameInitializeState(this));
    }

    private void Update()
    {
        _stateMachine.Update();
    }
}