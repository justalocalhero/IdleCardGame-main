using UnityEngine;

public abstract class CardUIUpdate : MonoBehaviour
{
    private CardUI cardUI;

    void Awake()
    {
        cardUI = GetComponentInParent<CardUI>();

        cardUI.onUpdateUI += UpdateUI;

        OnAwake();
    }
    
    void OnDestroy()
    {
        cardUI.onUpdateUI -= UpdateUI;
    }

    protected virtual void OnAwake()
    {
        
    }


    protected abstract void UpdateUI(Card card);
}