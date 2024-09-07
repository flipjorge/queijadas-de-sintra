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

    private async void OnCardClickedHandler(Card card)
    {
        card.ShowCard();

        if (_currentSelectedCard != null)
        {
            var previousCard = _currentSelectedCard;
            _currentSelectedCard = null;

            if (previousCard.SymbolId == card.SymbolId)
            {
                Owner.CardsOnTable.Remove(card);
                Owner.CardsOnTable.Remove(previousCard);

                if (Owner.CardsOnTable.Count == 0)
                {
                    _onAllCardsMatchedHandler?.Invoke();
                }
                    
                await Task.Delay(500);

                previousCard.Destroy();
                card.Destroy();
            }
            else
            {
                await Task.Delay(500);

                previousCard.HideCard();
                card.HideCard();
            }

            return;
        }

        _currentSelectedCard = card;
    }
}