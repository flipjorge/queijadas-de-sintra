using UnityEngine;

public class GameEndState : State<GameManager>
{
    public GameEndState(GameManager owner) : base(owner)
    {
        //
    }

    public override void Enter()
    {
        Debug.Log("Game Ended");
    }

    public override void Update()
    {
        //
    }

    public override void Exit()
    {
        //
    }
}