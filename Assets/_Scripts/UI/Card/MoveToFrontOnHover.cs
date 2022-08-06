using UnityEngine;
using UnityEngine.EventSystems;

public class MoveToFrontOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CardTray cardTray;
    
    void Awake()
    {
        cardTray = GetComponentInParent<CardTray>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.dragging) return;

        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(eventData.dragging) return;

        cardTray.SetToReorder();
    }
}