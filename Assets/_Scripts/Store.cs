using UnityEngine;
using System.Collections.Generic;

public class Store : MonoBehaviour
{    
    public PackOpeningWindow packOpeningWindowPrefab;
    private PackOpeningWindow packOpeningWindow;
    public CardCatalog cardCatalog;
    public BoosterPackUI prefab;
    public Transform cardContainer, overlayContainer;

    public List<BoosterPack> boosters = new List<BoosterPack>();

    public delegate void OnPackOpened();
    public OnPackOpened onPackOpened;

    void Start()
    {
        packOpeningWindow = Instantiate(packOpeningWindowPrefab, overlayContainer);
        packOpeningWindow.Register(this);
        
        //boosters.Add(new TrinketPack());
        boosters.Add(new PrimativePack());
        boosters.Add(new AlphaPack());

        int index = -1;

        foreach(BoosterPack booster in boosters)
        {
            BuildBooster(booster, ++index);
        }
    }

    void BuildBooster(BoosterPack boosterPack, int index)
    {
        BoosterPackUI boosterPackUI = Instantiate(prefab, cardContainer);
        boosterPackUI.Register(boosterPack, this, cardCatalog, index);
        boosterPackUI.onClick += OpenBooster;
    }

    void OpenBooster(BoosterPackUI boosterPackUI)
    {
        if(!boosterPackUI.boosterPack.CanOpen()) return;

        BoosterPack boosterPack = boosterPackUI.boosterPack;

        ResourceManager.instance.Coins -= boosterPack.Cost;
        List<Card> cards = boosterPack.OpenPack();
        List<CardWrapper> wrappers = cardCatalog.AddCards(cards);
        packOpeningWindow.Show(wrappers, boosterPackUI);

        if(onPackOpened != null) onPackOpened();
    }
}
