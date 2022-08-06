using UnityEngine;
using TMPro;

public class CardName : CardUIUpdate
{
    private TextMeshProUGUI text;

    protected override void OnAwake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void UpdateUI(Card card)
    {
        text.SetText(card.Name);
    }
}
