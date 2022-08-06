using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniCard : MonoBehaviour
{
    public CardWrapper cardWrapper;
    private Button button;
    public TextMeshProUGUI costText, nameText, countText;
    public int index = -1;

    public delegate void OnClick(int index);
    public OnClick onClick;

    public delegate void OnCardChanged();
    public OnCardChanged onCardChanged;
    
    void Awake()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(OnPointerClick);
    }

    public void Register(CardWrapper wrapper, int index)
    {
        if(cardWrapper != null) cardWrapper.onChanged -= UpdateUI;
        cardWrapper = wrapper;
        wrapper.onChanged += UpdateUI;

        this.index = index;
        
        UpdateUI();

        if(onCardChanged != null) onCardChanged();
    }

    public void UpdateUI()
    {        
        costText.text = cardWrapper.card.Cost.ToString();
        nameText.text = cardWrapper.card.Name;
        countText.text = cardWrapper.inDeck.ToString() + " / " + cardWrapper.owned.ToString();

    }

    public void OnPointerClick()
    {
        if(onClick != null) onClick(index);
    }
}