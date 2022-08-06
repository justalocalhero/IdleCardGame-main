using UnityEngine;
using UnityEngine.UI;

public class BoosterCardGlow : MonoBehaviour
{
    public Image image;
    public Color newColor, usefulColor;
    private Color defaultColor;

    void Awake()
    {
        GetComponentInParent<BoosterCardPreview>().onCardChanged += UpdateUI;
        defaultColor = image.color;
    }

    void UpdateUI(CardWrapper cardWrapper)
    {
        if(cardWrapper.owned == 1) 
        {
            image.color = newColor;
            image.enabled = true;
        }
        else if(cardWrapper.owned <= 4) 
        {
            image.color = usefulColor;
            image.enabled = true;
        }
        else 
        {
            image.color = defaultColor;
            image.enabled = false;
        }
    }
}