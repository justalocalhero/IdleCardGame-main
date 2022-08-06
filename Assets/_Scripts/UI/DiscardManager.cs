using UnityEngine;

public class DiscardManager : MonoBehaviour
{
    CardTray cardTray;

    void Awake()
    {        
        cardTray = GetComponentInParent<CardTray>();
    }

    void Start()
    {        
        Engine.instance.gameBus.onCardDiscarded += Discard;
    }

    void Discard(PlayPackage playPackage, Card card)
    {
        CardController cardUI = cardTray.Get(card);

        if(cardUI == null) return;

        cardTray.Remove(cardUI);
        cardUI.Kill();
    }
}