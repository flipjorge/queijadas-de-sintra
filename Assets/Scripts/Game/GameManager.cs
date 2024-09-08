using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelLayoutSO LevelLayout;
    [SerializeField] private Card CardPrefab;
    [SerializeField] private SpritesCollectionSO SymbolsCollection;
    [SerializeField] private GameObject CardsContainer;
    [SerializeField] private UIGameManager UIManager;
    [SerializeField] private VoiceAudioManager VoiceAudioManager;

    public IList<Card> CardsOnTable { get; private set; }
    public GameStateData GameStateData { get; private set; }
    
    public StreakBonus StreakBonus { get; private set; }

    public UIGameManager UI => UIManager;

    private StateMachine<GameManager> _stateMachine;
    private BonusManager _bonusManager;

    private void Awake()
    {
        _stateMachine = new StateMachine<GameManager>(this);
        GameStateData = new GameStateData();
        
        _bonusManager = new BonusManager();

        StreakBonus = new StreakBonus(10);
        _bonusManager.AddBonus(StreakBonus);
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
            SymbolsCollection,
            VoiceAudioManager,
            StartGame);

        _stateMachine.ChangeTo(state);
    }

    private void StartGame(IList<Card> cards)
    {
        CardsOnTable = cards;
        _stateMachine.ChangeTo(new GameRunningState(this, VoiceAudioManager, Score, EndGame));
    }

    private void EndGame()
    {
        _stateMachine.ChangeTo(new GameEndState(this, VoiceAudioManager, RestartGame));
    }

    private void RestartGame()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void Score()
    {
        const int baseScore = 100;
        var calculatedScore = _bonusManager.Calculate(baseScore);
        
        GameStateData.AddScore(calculatedScore);
    }
}