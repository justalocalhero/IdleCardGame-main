using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManageRaycastOnDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponentInParent<CardController>().GetComponentInChildren<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }    
}
