using UnityEngine;
using UnityEngine.UI;

public class BoosterPackGlow : BoosterPackUIUpdate
{
    public Color BaseColor { get; private set; }
    private Color _Color;
    public Color Color 
    { 
        get => _Color; 
        set
        {
            _Color = value;
            SetColor();
        }
    }

    private Image image;

    protected override void OnAwake()
    {
        image = GetComponentInChildren<Image>();
        BaseColor = image.color;
        Color = BaseColor;
        image.enabled = false;
    }

    protected override void UpdateUI(BoosterPack boosterPack)
    {
        image.enabled = boosterPack.CanOpen();
    }

    private void SetColor()
    {
        image.color = Color;
    }
}
