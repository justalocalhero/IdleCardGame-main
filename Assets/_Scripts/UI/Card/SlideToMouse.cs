using UnityEngine;
using UnityEngine.EventSystems;

public class SlideToMouse : MonoBehaviour, IDragHandler
{
    private Camera cam;
    public float factor;
    
    void Awake()
    {
        cam = Camera.main;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = cam.WorldToScreenPoint(transform.position);
        Vector2 dif = eventData.position - pos;
        Vector2 delta = eventData.delta;
        float hyp = Mathf.Sqrt(delta.x * delta.x + delta.y * delta.y);
        float calcFactor = hyp * factor * Time.deltaTime;
        float clampedFactor = Mathf.Clamp01(calcFactor);

        Vector3 newPos = cam.ScreenToWorldPoint((pos + dif * clampedFactor));
        newPos.z = 0;
        transform.position = newPos;
    }
}