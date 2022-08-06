using UnityEngine;
using UnityEngine.EventSystems;

public class SlideToPosition : MonoBehaviour, IPointerDownHandler
{
    private CardTray cardTray;
    private bool active = false;
    private Vector3 target;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float minDistance;

    void Awake()
    {        
        cardTray = GetComponentInParent<CardTray>();
    }

    void Update()
    {
        if(!active) return;

        transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * speed);

        TrySnap();

        cardTray.SetToReorder();
    }

    public void Set(Vector3 target)
    {
        this.target = target;

        active = true;

        TrySnap();
    }

    private void TrySnap()
    {        
        if(Vector3.Distance(transform.localPosition, target) <= minDistance)
        {
            active = false;
            transform.localPosition = target;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        active = false;
    }
}