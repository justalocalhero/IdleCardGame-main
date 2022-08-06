using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(DefaultSize))]
public class ScaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{
    public RectTransform container;
    DefaultSize defaultSize;
    public Vector3 Value;
    public float speed, minDistance;
    private Vector3 Target;
    bool active;

    void Awake()
    {
        defaultSize = GetComponent<DefaultSize>();
    }

    void Update()
    {
        if(!active) return;

        container.transform.localScale = Vector3.Lerp(container.transform.localScale, Target, speed * Time.deltaTime);

        TrySnap();
    }

    void TrySnap()
    {
        float distance = Vector3.Distance(Target, container.transform.localScale);

        if(distance <= minDistance) 
        {
            container.transform.localScale = Target;
            active = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.dragging) return;
        if(Input.GetMouseButton(0)) return;

        Target = (defaultSize.Value + Value);
        active = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(eventData.dragging) return;

        Target = defaultSize.Value;
        active = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        active = false;
    }
}
