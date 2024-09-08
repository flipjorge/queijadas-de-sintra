using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameDealCardsState : State<GameManager>, IDisposable
{
    private readonly LevelLayout _layout;
    private readonly Card _cardPrefab;
    private readonly GameObject _cardsContainer;
    private readonly SpritesCollectionSO _symbolsSprites;
    private readonly Card.CardsArgs _onCardsDealtHandler;
    private readonly VoiceAudioManager _voiceAudioManager;
    
    private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

    public GameDealCardsState(
        GameManager owner,
        LevelLayout layout,
        Card cardPrefab,
        GameObject cardsContainer,
        SpritesCollectionSO symbolsSprites,
        VoiceAudioManager voiceAudioManager,
        Card.CardsArgs onCardsDealtHandler) :
        base(owner)
    {
        _layout = layout;
        _cardPrefab = cardPrefab;
        _cardsContainer = cardsContainer;
        _symbolsSprites = symbolsSprites;
        _onCardsDealtHandler = onCardsDealtHandler;
        _voiceAudioManager = voiceAudioManager;
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

        var symbolSprites = _symbolsSprites.GetInRandomOrder();

        var offsetX = (_layout.Columns - 1) * _layout.Space.x / 2;
        var offsetY = (_layout.Rows - 1) * _layout.Space.y / 2;

        var dealtCards = new List<Card>(totalPairs * 2);

        for (var column = 0; column < _layout.Columns; column++)
        {
            var columnPosition = _cardsContainer.transform.position.x + (column * _layout.Space.x - offsetX);

            for (var row = 0; row < _layout.Rows; row++)
            {
                var symbolIndex = column * _layout.Rows + row;
                var symbolId = symbols[symbolIndex];
                var sprite = symbolSprites[symbolId];
                var rowPosition = _cardsContainer.transform.position.z + (row * _layout.Space.y - offsetY);
                var position = new Vector3(columnPosition, _cardsContainer.transform.position.y, rowPosition);

                var card = Object.Instantiate(_cardPrefab, position, _cardsContainer.transform.rotation, _cardsContainer.transform);
                card.Initialize(symbolId, sprite);

                dealtCards.Add(card);
            }
        }

        var startingPanel = Owner.UI.InstantiatePanel<UIGameStartingPanel>();
        startingPanel.SetMessage("Ready");
        _voiceAudioManager.PlayStarting(VoiceAudioManager.StartingPhase.Ready);

        await Task.WhenAll(Task.Delay(3000), startingPanel.Show());
        if (_cancellationToken.IsCancellationRequested) return;
        
        foreach (var card in dealtCards) card.HideCard();
        
        startingPanel.SetMessage("Set");
        _voiceAudioManager.PlayStarting(VoiceAudioManager.StartingPhase.Set);
        
        await Task.Delay(1000);
        if (_cancellationToken.IsCancellationRequested) return;
        
        startingPanel.SetMessage("Go!");
        _voiceAudioManager.PlayStarting(VoiceAudioManager.StartingPhase.Go);
        
        _onCardsDealtHandler?.Invoke(dealtCards);
        
        await startingPanel.Hide(1000);
        if (_cancellationToken.IsCancellationRequested) return;
    }

    public override void Update()
    {
        //
    }

    public override void Exit()
    {
        //
    }

    public void Dispose()
    {
        _cancellationToken.Cancel();
    }
}