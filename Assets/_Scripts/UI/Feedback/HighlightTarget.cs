using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighlightTarget : MonoBehaviour
{
    private GameInputHandler gameInputHandler;
    TargetObject targetObject;
    Image highlight;

    void Awake()
    {
        gameInputHandler = GetComponentInParent<CardGame>()?.GetComponentInChildren<GameInputHandler>();
    }

    void Start()
    {
        targetObject = GetComponentInParent<TargetObject>();
        highlight = GetComponentInChildren<Image>();

        gameInputHandler.onCardSelected += Select;
        gameInputHandler.onCardCleared += Clear;
    }

    void OnDestroy()
    {
        gameInputHandler.onCardSelected -= Select;
        gameInputHandler.onCardCleared -= Clear;
    }

    void Clear()
    {
        highlight.enabled = false;
    }

    void Select(CardController cardUI)
    {
        Card card = cardUI.Card;

        highlight.enabled = card.CanTarget(targetObject.target) && card.CanPayCosts() && card.CanFindTargets();
        
    }
}