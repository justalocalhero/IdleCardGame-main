using UnityEngine;
using TMPro;

public class CardCost : CardUIUpdate
{
    private TextMeshProUGUI text;

    protected override void OnAwake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void UpdateUI(Card card)
    {
        text.SetText(card.Cost.ToString());
    }
}
