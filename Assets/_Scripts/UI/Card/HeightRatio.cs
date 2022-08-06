using UnityEngine;
using UnityEngine.EventSystems;

public class HeightRatio : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public float Value { get; set; }
    public float heightCeiling;
    public float heightFloor;
    private float initialHeight;

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialHeight = eventData.position.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float dif = eventData.position.y - initialHeight - heightFloor;

        dif = Mathf.Clamp(dif, 0, heightCeiling - heightFloor);
        Value = dif / (heightCeiling - heightFloor);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Value = 0;
    }
}
