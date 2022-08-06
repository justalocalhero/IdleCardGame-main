using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static CardPreview instance;
    public float clearTime;
    CardUI cardUI;
    private float nextClearTime;
    bool toClear;

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);

        cardUI = GetComponentInChildren<CardUI>();
    }

    void Start()
    {
        cardUI.gameObject.SetActive(false);
    }

    void Update()
    {
        if(!toClear) return;
        if(Time.time <= nextClearTime) return;

        toClear = false;
        cardUI.gameObject.SetActive(false);
    }

    public void Show(Card card)
    {
        cardUI.gameObject.SetActive(true);
        cardUI.Card = card;

        SetShow();
    }

    public void Hide()
    {
        toClear = true;
        nextClearTime = Time.time + clearTime;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetShow();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hide();
    }

    private void SetShow()
    {
        toClear = false;
    }
}
