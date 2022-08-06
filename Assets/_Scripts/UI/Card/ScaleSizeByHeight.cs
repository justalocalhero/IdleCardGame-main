using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(DefaultSize), typeof(HeightRatio))]
public class ScaleSizeByHeight : MonoBehaviour, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    public RectTransform container;
    DefaultSize defaultSize;
    HeightRatio heightRatio;

    public float factor;
    public float easingPower;
    private Vector3 beginSize;

    public void Awake()
    {
        defaultSize = GetComponent<DefaultSize>();
        heightRatio = GetComponent<HeightRatio>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        beginSize = container.transform.localScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        container.transform.localScale = beginSize * (1 - factor * Mathf.Pow(heightRatio.Value, easingPower));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        container.transform.localScale = defaultSize.Value;
    }
}
