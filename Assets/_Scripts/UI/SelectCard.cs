using UnityEngine;
using UnityEngine.EventSystems;

public class SelectCard : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IEndDragHandler
{
    private GameInputHandler gameInputHandler;

    CardController cardUI;

    void Awake()
    {        
        gameInputHandler = GetComponentInParent<CardGame>()?.GetComponentInChildren<GameInputHandler>();
    }
    
    void Start()
    {
        cardUI = GetComponentInParent<CardController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.dragging)
        {
            gameInputHandler.SelectedCard = null;
            return;
        }

        gameInputHandler.SelectedCard = cardUI;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        gameInputHandler.SelectedCard = cardUI;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gameInputHandler.SelectedCard = null;
    }
}