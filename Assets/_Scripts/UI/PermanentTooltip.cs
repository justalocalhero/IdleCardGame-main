using UnityEngine;
using UnityEngine.EventSystems;

public class PermanentTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 offset;
    PermanentUI permanentUI;
    RectTransform rect;

    void Awake()
    {
        permanentUI = GetComponentInParent<PermanentUI>();
        rect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners);
        Vector3 topRightCorner = corners[2];

        SingletonTooltip.instance.Push(permanentUI.permanent.GetDescription(), topRightCorner + offset);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SingletonTooltip.instance.Hide();
    }
}