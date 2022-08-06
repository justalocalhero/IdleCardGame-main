using UnityEngine;
using UnityEngine.EventSystems;

public class SetCardTrayIndexOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    CardTray cardTray;

    void Awake()
    {        
        cardTray = GetComponentInParent<CardTray>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.dragging) return;

        cardTray.SetHover(transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardTray.ClearHover();
    }
}