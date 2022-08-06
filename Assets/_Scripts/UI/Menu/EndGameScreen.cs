using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.EventSystems;

public class EndGameScreen : MonoBehaviour, IPointerClickHandler
{

    public TextMeshProUGUI textMesh;
    public GameObject screen;

    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();

        Engine.instance.gameBus.onEndGame += Show;
        screen.SetActive(false);
    }

    void Show(PlayPackage playPackage)
    {        
        screen.SetActive(true);
        StringBuilder sb = new StringBuilder();

        int total = 0;

        int coins = playPackage.gameBoard.GetResource(ResourceType.Coin);
        if(coins > 0)
        {
            total += coins;

            sb.AppendLine("Collected " + coins + " Coins");
        }

        foreach(Resource resource in playPackage.gameBoard.resources)
        {
            if(resource.resourceType == ResourceType.Coin) continue;

            int tradeCount = resource.count / EndGameCoinCollection.ResourcePerCoin;
            int coinValue = tradeCount;
            int soldCount = tradeCount * EndGameCoinCollection.ResourcePerCoin;

            if(coinValue <= 0) continue;

            total += resource.count;
            sb.AppendLine("Sold " + soldCount + " " +  resource.resourceType.ToString() + " for " + coinValue + " Coins");
        }

        sb.AppendLine("\nEarned " + total + " Coins");

        textMesh.SetText(sb.ToString());
    }

    public void OnPointerClick(PointerEventData eventData)
    {        
        screen.SetActive(false);
    }
}
