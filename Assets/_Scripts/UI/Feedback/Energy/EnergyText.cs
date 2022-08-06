using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyText : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {
        Engine.instance.gameBus.onChanged += UpdateUI;
    }

    private void UpdateUI(PlayPackage playPackage)
    {
        text.SetText(playPackage.gameBoard.energy + " / " + playPackage.gameBoard.maxEnergy);
    }
}
