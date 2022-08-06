using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PushToCardPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    MiniCard miniCard;
    bool hovered = false;

    void Awake()
    {
        miniCard = GetComponentInParent<MiniCard>();
        miniCard.onCardChanged += UpdateUI;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
        Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
        Hide();
    }

    void UpdateUI()
    {
        if(hovered) Show();
    }

    void Show()
    {
        CardPreview.instance?.Show(miniCard?.cardWrapper?.card);
    }

    void Hide()
    {
        CardPreview.instance?.Hide();
    }

    void OnDestroy()
    {
        miniCard.onCardChanged -= UpdateUI;
        if(hovered) CardPreview.instance?.Hide();
    }
}
