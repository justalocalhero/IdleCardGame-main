using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyCountdown : MonoBehaviour
{
    public Transform container;
    public GameObject prefab;

    private List<GameObject> pips = new List<GameObject>();

    void Start()
    {
        Engine.instance.gameBus.onChanged += UpdateUI;
    }

    private void UpdateUI(PlayPackage playPackage)
    {
        int count = 0;

        foreach(EnergyGrowth property in playPackage.gameBoard.properties)
        {
            count += property.Remaining;
        }
        
        for(int i = 0; i <= count - pips.Count; i++)
        {
            pips.Add(Instantiate(prefab, container));
        }

        for(int i = pips.Count - count; i > 0; i--)
        {
            Destroy(pips[pips.Count - 1]);
            pips.RemoveAt(pips.Count - 1);
        }
    }
}