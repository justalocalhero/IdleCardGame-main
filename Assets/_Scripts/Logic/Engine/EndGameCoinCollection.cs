using UnityEngine;

public class EndGameCoinCollection : MonoBehaviour
{    
    public const int ResourcePerCoin = 3;
    ResourceManager resourceManager;

    void Start()
    {
        resourceManager = GetComponentInParent<ResourceManager>();

        Engine.instance.gameBus.onEndGame += Handle;
    }

    void Handle(PlayPackage playPackage)
    {
        int coins = playPackage.gameBoard.GetResource(ResourceType.Coin);

        foreach(Resource resource in playPackage.gameBoard.resources)
        {
            if(resource.resourceType == ResourceType.Coin) continue;

            int coinValue = resource.count / ResourcePerCoin;
            coins += coinValue;
        }

        resourceManager.Coins += coins;

    }
}
