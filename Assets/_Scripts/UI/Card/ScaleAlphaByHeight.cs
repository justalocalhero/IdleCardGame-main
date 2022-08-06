using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(HeightRatio))]
public class ScaleAlphaByHeight : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    CanvasGroup canvasGroup;

    DefaultSize defaultSize;
    HeightRatio heightRatio;

    public float factor;
    public float easingPower;
    private float initialAlpha;

    public void Awake()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        heightRatio = GetComponent<HeightRatio>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialAlpha = canvasGroup.alpha;
    }

    public void OnDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = initialAlpha - initialAlpha * factor * Mathf.Pow(heightRatio.Value, easingPower);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = initialAlpha;
    }
}
