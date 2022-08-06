using UnityEngine;
using UnityEngine.EventSystems;

public class ClosePackWindow : MonoBehaviour, IPointerClickHandler
{
    PackOpeningWindow packOpeningWindow;
    public float fireDelay;
    float nextFireTime;

    void Awake()
    {
        packOpeningWindow = GetComponentInParent<PackOpeningWindow>();
        packOpeningWindow.onShow += HandleShow;
    }

    public void HandleShow()
    {
        nextFireTime = Time.time + fireDelay;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Time.time <= nextFireTime) return;
        
        packOpeningWindow.Hide();
    }

}
