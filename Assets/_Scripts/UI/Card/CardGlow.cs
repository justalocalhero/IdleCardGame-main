using UnityEngine;
using UnityEngine.UI;

public class CardGlow : CardUIUpdate
{
    private Image glow;
    private GameInputHandler gameInputHandler;

    protected override void OnAwake()
    {
        glow = GetComponentInChildren<Image>();
        gameInputHandler = GetComponentInParent<CardGame>()?.GetComponentInChildren<GameInputHandler>();
    }

    protected override void UpdateUI(Card card)
    {
        if(!Engine.instance.playing) glow.enabled = true;
        else glow.enabled = (gameInputHandler.SelectedCard == null) && card.CanPayCosts() && card.CanFindTargets();
    }
}