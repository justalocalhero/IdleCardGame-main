using UnityEngine;
using UnityEngine.UI;

public class ActivateHitboxIfValidTarget : MonoBehaviour
{
    private GameInputHandler gameInputHandler;
    TargetObject targetObject;
    Image hitbox;

    void Awake()
    {        
        gameInputHandler = GetComponentInParent<CardGame>()?.GetComponentInChildren<GameInputHandler>();
    }

    void Start()
    {
        targetObject = GetComponentInParent<TargetObject>();
        hitbox = GetComponentInChildren<Image>();

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
        hitbox.enabled = true;
    }

    void Select(CardController cardUI)
    {
        Card card = cardUI.Card;
        
        hitbox.enabled = card.CanTarget(targetObject.target) && card.CanPayCosts() && card.CanFindTargets();
        
    }

}