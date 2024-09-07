using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameDealCardsState : State<GameManager>
{
    private readonly LevelLayout _layout;
    private readonly Card _cardPrefab;
    private readonly GameObject _cardsContainer;
    private readonly Card.CardsArgs _onCardsDealtHandler;

    public GameDealCardsState(GameManager owner, LevelLayout layout, Card cardPrefab, GameObject cardsContainer,
        Card.CardsArgs onCardsDealtHandler) :
        base(owner)
    {
        _layout = layout;
        _cardPrefab = cardPrefab;
        _cardsContainer = cardsContainer;
        _onCardsDealtHandler = onCardsDealtHandler;
    }

    public override async void Enter()
    {
        var totalPairs = _layout.Columns * _layout.Rows / 2;

        var symbols = new List<int>(totalPairs * 2);

        for (var i = 0; i < totalPairs; i++)
        {
            symbols.Add(i);
            symbols.Add(i);
        }

        symbols.Shuffle();

        var offsetX = (_layout.Columns - 1) * _layout.Space.x / 2;
        var offsetY = (_layout.Rows - 1) * _layout.Space.y / 2;

        var dealtCards = new List<Card>(totalPairs * 2);

        for (var column = 0; column < _layout.Columns; column++)
        {
            var columnPosition = column * _layout.Space.x - offsetX;

            for (var row = 0; row < _layout.Rows; row++)
            {
                var symbolIndex = column * _layout.Rows + row;
                var rowPosition = row * _layout.Space.y - offsetY;
                var position = new Vector3(columnPosition, 0, rowPosition);

                var card = Object.Instantiate(_cardPrefab, position, Quaternion.identity, _cardsContainer.transform);
                card.Initialize(symbols[symbolIndex]);

                dealtCards.Add(card);
            }
        }

        await Task.Delay(3000);

        foreach (var card in dealtCards) card.HideCard();

        await Task.Delay(500);

        _onCardsDealtHandler?.Invoke(dealtCards);
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