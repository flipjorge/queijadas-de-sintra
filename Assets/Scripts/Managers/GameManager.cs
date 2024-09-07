using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelLayoutSO LevelLayout;
    [SerializeField] private Card CardPrefab;
    [SerializeField] private GameObject CardsContainer;
    [SerializeField] private UIGameManager UIManager;

    public IList<Card> CardsOnTable { get; private set; }
    public Score Score { get; private set; }
    public UIGameManager UI => UIManager;
    
    private StateMachine<GameManager> _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine<GameManager>(this);
        Score = new Score();
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
        CardsOnTable = cards;
        _stateMachine.ChangeTo(new GameRunningState(this, EndGame));
    }

    private void EndGame()
    {
        _stateMachine.ChangeTo(new GameEndState(this, RestartGame));
    }

    private void RestartGame()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}