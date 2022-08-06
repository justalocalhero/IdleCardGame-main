using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardController : MonoBehaviour
{
    private GameInputHandler gameInputHandler;
    private CardUI cardUI;
    private Card _card;
    public Card Card 
    { 
        get => _card;
        set
        {
            _card = value;
            cardUI.Card = value;
        }
    }

    void Awake()
    {
        cardUI = GetComponentInChildren<CardUI>();
        gameInputHandler = GetComponentInParent<CardGame>()?.GetComponentInChildren<GameInputHandler>();
    }

    public void Start()
    {
        Engine.instance.gameBus.onChanged += (PlayPackage playPackage) => UpdateUI();
        gameInputHandler.onCardSelected += (CardController card) => UpdateUI();
        gameInputHandler.onCardCleared += UpdateUI;
        UpdateUI();
    }

    void OnDestroy()
    {
        Engine.instance.gameBus.onChanged -= (PlayPackage playPackage) => UpdateUI();
        gameInputHandler.onCardSelected -= (CardController card) => UpdateUI();
        gameInputHandler.onCardCleared -= UpdateUI;
    }

    void UpdateUI()
    {
        cardUI.UpdateUI();
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
