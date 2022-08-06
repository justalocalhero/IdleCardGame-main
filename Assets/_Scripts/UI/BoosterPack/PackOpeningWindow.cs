using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PackOpeningWindow : MonoBehaviour
{
    private Store store;
    private BoosterPackUI boosterPackUI;
    public BoosterCardPreview prefab;
    public Transform cardContainer, packContainer;
    private List<BoosterCardPreview> previews = new List<BoosterCardPreview>();

    public delegate void OnShow();
    public OnShow onShow;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Register(Store store)
    {
        this.store = store;
    }

    public void Show(List<CardWrapper> cards, BoosterPackUI boosterPackUI)
    {
        gameObject.SetActive(true);

        Clear();

        foreach(CardWrapper card in cards)
        {
            BoosterCardPreview preview = Instantiate(prefab, cardContainer);
            preview.CardWrapper = card;
            previews.Add(preview);
        }

        this.boosterPackUI = boosterPackUI;

        boosterPackUI.transform.SetParent(packContainer);
        boosterPackUI.transform.localPosition = Vector3.zero;

        if(onShow != null) onShow();
    }

    public void Clear()
    {
        foreach(BoosterCardPreview preview in previews)
        {
            Destroy(preview.gameObject);
        }

        previews = new List<BoosterCardPreview>();
    }

    public void Hide()
    {
        Clear();

        boosterPackUI.transform.SetParent(store.cardContainer);
        boosterPackUI.transform.SetSiblingIndex(boosterPackUI.index);

        gameObject.SetActive(false);
    }
}
