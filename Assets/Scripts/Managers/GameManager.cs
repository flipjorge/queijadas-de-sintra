using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelLayoutSO LevelLayout;
    [SerializeField] private Card CardPrefab;
    [SerializeField] private GameObject CardsContainer;

    public IList<Card> CardsOnTable => _cardsOnTable;
    public Score Score => _score;
    
    private StateMachine<GameManager> _stateMachine;
    private IList<Card> _cardsOnTable;
    private Score _score;

    private void Awake()
    {
        _stateMachine = new StateMachine<GameManager>(this);
        _score = new Score();
    }

    private void Start()
    {
        var state = new GameInitializeState(this, DealCards);
        
        _stateMachine.ChangeTo(state);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void DealCards()
    {
        var state = new GameDealCardsState(
            this,
            LevelLayout.Layout,
            CardPrefab,
            CardsContainer,
            StartGame);
        
        _stateMachine.ChangeTo(state);
    }

    private void StartGame(IList<Card> cards)
    {
        _cardsOnTable = cards;
        _stateMachine.ChangeTo(new GameRunningState(this, EndGame));
    }

    private void EndGame()
    {
        _stateMachine.ChangeTo(new GameEndState(this));
    }
}