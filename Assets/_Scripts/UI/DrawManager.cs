using UnityEngine;

public class DrawManager : MonoBehaviour
{
    CardTray cardTray;
    CardController prefab;
    Transform container;

    void Awake()
    {        
        cardTray = GetComponentInParent<CardTray>();
    }

    void Start()
    {
        Engine.instance.gameBus.onCardDrawn += Draw;
        prefab = cardTray.prefab;
        container = cardTray.container;
    }

    void Draw(PlayPackage playPackage, Card card)
    {
        CardController cardUI = Instantiate(prefab, container);
        cardUI.Card = card;
        cardTray.Add(cardUI);
    }
}
