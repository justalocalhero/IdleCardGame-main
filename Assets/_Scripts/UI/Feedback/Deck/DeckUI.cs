using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckUI : MonoBehaviour
{
    public Image image;

    private int startingDeckSize;

    void Start()
    {
        Engine.instance.gameBus.onBeginGame += SetStartingDeckSize;
        Engine.instance.gameBus.onChanged += UpdateUI;
    }

    void SetStartingDeckSize(PlayPackage playPackage)
    {
        image.fillAmount = 1;
        startingDeckSize = GetCount(playPackage);
    }

    void UpdateUI(PlayPackage playPackage)
    {
        float value = Mathf.Clamp(GetCount(playPackage), 0 , startingDeckSize);
        float result = Mathf.Clamp01(value / startingDeckSize);

        image.fillAmount = result;
    }

    int GetCount(PlayPackage playPackage)
    {
        return playPackage.deck.Cards.Count;
    }

    void OnDestroy()
    {
        Engine.instance.gameBus.onBeginGame -= SetStartingDeckSize;
        Engine.instance.gameBus.onChanged -= UpdateUI;
    }
}
