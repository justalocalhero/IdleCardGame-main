using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Drag : MonoBehaviour, IDragHandler
{
    private Camera cam;
    private Transform tran;

    void Start()
    {
        tran = GetComponentInParent<CardController>().transform;
        cam = Camera.main;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = cam.WorldToScreenPoint(tran.position);
        Vector3 delta = eventData.delta;

        Vector3 newPos = (cam.ScreenToWorldPoint(pos + delta));
        newPos.z = 0;

        tran.position = newPos;
    }
}
