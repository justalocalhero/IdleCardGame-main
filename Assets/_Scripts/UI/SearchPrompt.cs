using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SearchPrompt : MonoBehaviour
{
    public Button button;
    IPrompt<Card> prompt;
    public Transform container;
    public PromptCard prefab;
    private List<PromptCard> cards = new List<PromptCard>();
    private List<PromptCard> selected = new List<PromptCard>();

    void Awake()
    {
        button.onClick.AddListener(HandlePush);
    }

    public void HandleClick(PromptCard promptCard)
    {
        if(promptCard.selected)
        {
            promptCard.selected = false;
            selected.Remove(promptCard);
        }
        else
        {
            promptCard.selected = true;
            selected.Add(promptCard);

            if(IsExcess())
            {
                selected[0].selected = false;
                selected.RemoveAt(0);
            }
        }

        UpdateUI();
    }

    private bool IsExcess()
    {
        return prompt.IsExcess(GetCards());
    }

    private bool IsValid()
    {
        return prompt.ValidTargets(GetCards());
    }

    public void UpdateUI()
    {
        button.interactable = IsValid();
    }

    public List<Card> GetCards()
    {
        List<Card> toReturn = new List<Card>();

        foreach(PromptCard promptCard in selected)
        {
            toReturn.Add(promptCard.card);
        }

        return toReturn;
    }

    public void HandlePush()
    {

        prompt.Fill(GetCards());

        Clear();
    }

    public void Register(IPrompt<Card> value)
    {
        gameObject.SetActive(true);
        prompt = value;

        List<Card> sorted = new List<Card>();
        
        foreach(Card card in prompt.population)
        {
            int index = 0;

            for(int i = 0; i < sorted.Count; i++)
            {
                if(card.Name.CompareTo(sorted[i].Name) <= 0)
                {
                    index = i;
                    break;
                }
            }

            sorted.Insert(index, card);
        }

        foreach(Card card in sorted)
        {
            Add(card);
        }

        UpdateUI();
    }

    public void Add(Card card)
    {
        PromptCard promptCard = Instantiate(prefab, container);
        promptCard.Register(card);
        cards.Add(promptCard);
        promptCard.onClick += HandleClick;
    }

    public void Remove(PromptCard promptCard)
    {
        promptCard.onClick -= HandleClick;
        cards.Remove(promptCard);
        Destroy(promptCard.gameObject);
    }

    public void Clear()
    {
        selected.Clear();

        for(int i = cards.Count - 1; i >= 0; i--)
        {
            Remove(cards[i]);
        }

        gameObject.SetActive(false);
    }
}
