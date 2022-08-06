using UnityEngine;
using UnityEngine.EventSystems;

public class ClearCardBackground : MonoBehaviour, IPointerClickHandler
{
    private GameInputHandler gameInputHandler;

    void Awake()
    {        
        gameInputHandler = GetComponentInParent<CardGame>()?.GetComponentInChildren<GameInputHandler>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameInputHandler.SelectedCard = null;
    }
}
