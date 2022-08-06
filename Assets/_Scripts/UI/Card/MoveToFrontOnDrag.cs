using UnityEngine;
using UnityEngine.EventSystems;

public class MoveToFrontOnDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private int index;

    public void OnBeginDrag(PointerEventData eventData)
    {
        index = transform.GetSiblingIndex();

        transform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetSiblingIndex(index);
    }
}
