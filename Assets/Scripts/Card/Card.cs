using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private TextMeshPro SymbolIdTextfield;
    [SerializeField] private Transform FacesContainer;

    public event CardArgs OnCardClicked;

    public int SymbolId { get; private set; }

    private StateMachine<Card> _stateMachine;
    private FaceDirection _currentFaceDirection;

    public void Initialize(int symbolId, FaceDirection startingFaceDirection = FaceDirection.Up)
    {
        SymbolId = symbolId;
        _currentFaceDirection = startingFaceDirection;

        SymbolIdTextfield.text = SymbolId.ToString();
    }

    private void Awake()
    {
        _stateMachine = new StateMachine<Card>(this);
    }

    private void Start()
    {
        if (_currentFaceDirection == FaceDirection.Up) ShowCard();
        else HideCard();
    }

    private void OnMouseDown()
    {
        if (_currentFaceDirection == FaceDirection.Up) return;

        OnCardClicked?.Invoke(this);
    }

    [ContextMenu("Show")]
    public void ShowCard()
    {
        _currentFaceDirection = FaceDirection.Up;
        _stateMachine.ChangeTo(new CardFaceUpState(this, FacesContainer));
    }

    [ContextMenu("Hide")]
    public void HideCard()
    {
        _currentFaceDirection = FaceDirection.Down;
        _stateMachine.ChangeTo(new CardFaceDownState(this, FacesContainer));
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public enum FaceDirection { Up, Down }

    public delegate void CardArgs(Card card);

    public delegate void CardsArgs(IList<Card> cards);
}