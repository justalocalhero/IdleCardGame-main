using UnityEngine;
using UnityEngine.EventSystems;

public class BoosterPackGlowHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float alphaDelta;
    private BoosterPackGlow boosterPackGlow;

    void Awake()
    {
        boosterPackGlow = GetComponentInChildren<BoosterPackGlow>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {  
        Color color = boosterPackGlow.Color;
        color.a += alphaDelta;

        boosterPackGlow.Color = color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        boosterPackGlow.Color = boosterPackGlow.BaseColor;
    }
}
