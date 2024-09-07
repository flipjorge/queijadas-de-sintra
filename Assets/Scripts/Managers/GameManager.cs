using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelLayoutSO LevelLayout;
    [SerializeField] private Card CardPrefab;
    [SerializeField] private GameObject CardsContainer;
    
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

    public void DealCards()
    {
        _stateMachine.ChangeTo(new GameDealCardsState(this, LevelLayout.Layout, CardPrefab, CardsContainer));
    }
}