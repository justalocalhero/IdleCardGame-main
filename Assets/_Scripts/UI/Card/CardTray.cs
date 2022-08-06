using System.Collections.Generic;
using UnityEngine;

public class CardTray : MonoBehaviour
{
    public CardController prefab;
    public float spacing, hoverSpacing;
    public Transform container;

    private Vector2 cardSize;

    private List<CardController> cards = new List<CardController>();
    private RectTransform rect;

    private Transform hovered;
    private int heldIndex = -1;
    private bool toReorder = false;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        RectTransform prefabRect = prefab.GetComponent<RectTransform>();

        float width = prefabRect.rect.width;
        float height = prefabRect.rect.height;

        cardSize = new Vector2(width, height);

        CardController[] initialCards = GetComponentsInChildren<CardController>();

        foreach(CardController card in initialCards)
        {
            cards.Add(card);
        }

        RepositionCards();
    }

    void Update()
    {
        if(!toReorder) return;
        toReorder = false;
        ReorderCards();
    }

    public void RepositionCards()
    {
        Vector2 center = rect.rect.center;

        for(int i = 0; i < cards.Count; i++)
        {            

            float x = center.x + (i + .5f - .5f * cards.Count) * ClampWidth();

            CardController card = cards[i];

            Vector3 pos = card.transform.position;

            float half = .5f * Mathf.Clamp((cards.Count - 1), 1, (cards.Count - 1));
            float dist = i - half;
            float abs = dist * dist;

            pos.x = x;
            pos.y = -abs * 3f / half;
            pos.z = 0;

            if(i < heldIndex) pos.x -= hoverSpacing;

            card.GetComponent<SlideToPosition>()?.Set(pos);
        }
    }

    public void ReorderCards()
    {
        List<Transform> ordered = new List<Transform>();

        foreach(CardController card in cards)
        {
            Transform current = card.transform;

            if(current == hovered) continue;

            bool found = false;

            for(int i = 0; i < ordered.Count; i++)
            {                
                if(current.position.x <= ordered[i].position.x) continue;

                found = true;
                ordered.Insert(i, current);
                
                break;
            }

            if(found) continue;

            ordered.Add(current);
        }

        if(hovered != null) ordered.Add(hovered);

        for(int i = 0; i < ordered.Count; i++)
        {
            ordered[i].SetSiblingIndex(i);
        }
    }

    public void SetHeld(int index)
    {
        if(index == heldIndex) return;

        heldIndex = index;
        RepositionCards();
    }

    public void ClearHeld()
    {
        SetHeld(-1);
    }
    
    public void SetHover(Transform tr)
    {
        if(hovered == tr) return;

        hovered = tr;
        SetToReorder();
    }

    public void ClearHover()
    {
        SetHover(null);
    }

    public void SetToReorder()
    {
        toReorder = true;
    }

    public void Insert(int index, CardController card)
    {
        cards.Insert(index, card);
        RepositionCards();
    }

    public void Add(CardController card)
    {
        cards.Add(card);
        RepositionCards();
    }

    public void Remove(CardController card)
    {
        if(!cards.Contains(card)) return;
        
        cards.Remove(card);
        RepositionCards();
    }

    public CardController Get(Card card)
    {
        foreach(CardController cardUI in cards)
        {
            if(cardUI.Card == card) return cardUI;
        }

        return null;
    }

    public void Clear()
    {
        foreach(CardController card in cards)
        {
            card.Kill();
        }
        cards = new List<CardController>();
    }

    public int GetIndex(CardController card)
    {
        for(int i = 0; i < cards.Count; i++)
        {
            if(cards[i] == card) return i;
        }

        return -1;
    }

    public int CalculateTargetIndex(RectTransform targetRect)
    {
        Vector2 center = rect.rect.center;
        float x = targetRect.localPosition.x;

        int cardCount = cards.Count + 1;
        float index = (x - center.x) / ClampWidth() + .5f * cardCount -.5f;

        return Mathf.Clamp(Mathf.RoundToInt(index), 0, cards.Count);
    }

    public float ClampWidth()
    {
        float width = cardSize.x + spacing;
        float divWidth = rect.rect.width / cards.Count;
        float clampWidth = Mathf.Clamp(width, 0, divWidth);

        return clampWidth;
    }
}
