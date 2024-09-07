using System;
using Task = System.Threading.Tasks.Task;

public class GameRunningState : State<GameManager>
{
    private readonly Action _onAllCardsMatchedHandler; 
    
    private Card _currentSelectedCard;

    public GameRunningState(GameManager owner, Action onAllCardsMatchedHandler) : base(owner)
    {
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
            Owner.Score.IncreaseTurns();
            
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
        Owner.Score.IncreaseMatches();
        
        Owner.CardsOnTable.Remove(previousCard);
        Owner.CardsOnTable.Remove(currentCard);

        if (Owner.CardsOnTable.Count == 0)
        {
            _onAllCardsMatchedHandler?.Invoke();
        }
                    
        await Task.Delay(500);

        previousCard.Destroy();
        currentCard.Destroy();
    }

    private async void OnMatchFailed(Card previousCard, Card currentCard)
    {
        await Task.Delay(500);

        previousCard.HideCard();
        currentCard.HideCard();
    }
}