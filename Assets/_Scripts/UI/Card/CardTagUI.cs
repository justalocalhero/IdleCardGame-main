using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardTagUI : CardUIUpdate
{
    private TextMeshProUGUI text;

    protected override void OnAwake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void UpdateUI(Card card)
    {
        string toSet = "";
        bool post = true;

        foreach(ArchetypeTag tag in card.archetypeTags)
        {
            if(post) post = false;
            else toSet += " ";
            toSet += tag.ToString();
        }

        text.SetText(toSet);
    }
}
