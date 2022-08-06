using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class BoosterPackUI : MonoBehaviour, IPointerClickHandler
{
    public int index;
    private Store store;
    private CardCatalog cardCatalog;
    public delegate void OnUpdateUI(BoosterPack boosterPack);
    public OnUpdateUI onUpdateUI;

    private TextMeshProUGUI textMesh;
    public BoosterPack boosterPack { get; set; }

    public delegate void OnClick(BoosterPackUI boosterPackUI);
    public OnClick onClick;

    void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        ResourceManager.instance.onCoinsSet += (int count) => UpdateUI();
    }

    void OnDestroy()
    {
        ResourceManager.instance.onCoinsSet -= (int count) => UpdateUI();
        store.onPackOpened -= UpdateUI;
    }

    public void Register(BoosterPack boosterPack, Store store, CardCatalog cardCatalog, int index)
    {
        this.index = index;
        this.cardCatalog = cardCatalog;
        this.boosterPack = boosterPack;
        this.store = store;

        store.onPackOpened += UpdateUI;

        UpdateUI();
    }

    public void UpdateUI()
    {
        textMesh.SetText(boosterPack.GetDescription() + "\n\n" + CompareDescription(boosterPack, cardCatalog));

        if(onUpdateUI != null) onUpdateUI(boosterPack);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(onClick != null) onClick(this);

        UpdateUI();
    }

    public string CompareDescription(BoosterPack boosterPack, CardCatalog cardCatalog)
    {
        int collected = 0;
        int playset = 0;
        int cards = 0;

        foreach(BoosterBucket bucket in boosterPack.buckets)
        {
            foreach(Card card in bucket.cards)
            {
                cards++;
                foreach(CardWrapper cardWrapper in cardCatalog.cards)
                {
                    if(cardWrapper.card.Name != card.Name) continue;
                    if(cardWrapper.owned > 0) collected++;
                    if(cardWrapper.owned >= DeckManager.maxPerName) playset++;
                }
            }
        }

        string collectedString = "Collected: " + collected + " / " + cards;
        string playsetString = "Playsets: " + playset + " / " + cards;

        return collectedString + "\n" + playsetString;
    }
}
