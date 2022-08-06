using UnityEngine;
using UnityEngine.EventSystems;

public class Snap : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler
{
    private CardTray cardTray;
    private CardController cardUI;    
    public int index = -1;

    void Start()
    {
        cardUI = GetComponent<CardController>();
        cardTray = GetComponentInParent<CardTray>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        index = cardTray.GetIndex(cardUI);
        cardTray.Remove(cardUI);
        cardTray.SetHeld(index);
    }

    public void OnDrag(PointerEventData eventData)
    {
        index = cardTray.CalculateTargetIndex(GetComponent<RectTransform>());

        if(cardUI.Card.CanPlay())
        {
            cardTray.ClearHeld();
        }
        else
        {
            cardTray.SetHeld(index);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(cardUI.Card.CanPlay()) 
        {
            cardUI.Card.Prompt();
            Destroy(cardUI.gameObject);
        }
        else cardTray.Insert(index, cardUI);

        cardTray.ClearHeld();
    }
}
