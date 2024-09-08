using System;
using System.Threading;
using Task = System.Threading.Tasks.Task;

public class GameRunningState : State<GameManager>, IDisposable
{
    private readonly VoiceAudioManager _voiceAudioManager;
    private readonly Action _onScoreHandler;
    private readonly Action _onAllCardsMatchedHandler;
    
    private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();
    
    private Card _currentSelectedCard;

    public GameRunningState(GameManager owner, VoiceAudioManager voiceAudioManager, Action onScoreHandler, Action onAllCardsMatchedHandler) : base(owner)
    {
        _voiceAudioManager = voiceAudioManager;
        _onScoreHandler = onScoreHandler;
        _onAllCardsMatchedHandler = onAllCardsMatchedHandler;
    }

    public override void Enter()
    {
        foreach (var card in Owner.CardsOnTable) card.OnCardClicked += OnCardClickedHandler;
    }

    public override void Update()
    {
        //
    }

    public override void Exit()
    {
        foreach (var card in Owner.CardsOnTable) card.OnCardClicked -= OnCardClickedHandler;
    }

    private void OnCardClickedHandler(Card card)
    {
        card.ShowCard();

        if (_currentSelectedCard != null)
        {
            Owner.GameStateData.IncreaseTurns();
            
            if (_currentSelectedCard.SymbolId == card.SymbolId) OnMatchSuccess(_currentSelectedCard, card);
            else OnMatchFailed(_currentSelectedCard, card);
            
            _currentSelectedCard = null;
        }
        else
        {
            _currentSelectedCard = card;
        }
    }

    private async void OnMatchSuccess(Card previousCard, Card currentCard)
    {
        Owner.GameStateData.IncreaseMatches();
        _onScoreHandler?.Invoke();
        Owner.StreakBonus.IncreaseStreak();
        
        Owner.CardsOnTable.Remove(previousCard);
        Owner.CardsOnTable.Remove(currentCard);

        if (Owner.CardsOnTable.Count == 0)
        {
            _onAllCardsMatchedHandler?.Invoke();
        }
        
        _voiceAudioManager.PlayCorrect();
                    
        await Task.Delay(500);
        if (_cancellationToken.IsCancellationRequested) return;

        previousCard.Destroy();
        currentCard.Destroy();
    }

    private async void OnMatchFailed(Card previousCard, Card currentCard)
    {
        Owner.StreakBonus.Reset();
        
        _voiceAudioManager.PlayWrong();
        
        await Task.Delay(500);
        if (_cancellationToken.IsCancellationRequested) return;

        previousCard.HideCard();
        currentCard.HideCard();
    }

    public void Dispose()
    {
        _cancellationToken.Cancel();
    }
}