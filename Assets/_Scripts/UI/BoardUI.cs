using UnityEngine;
using UnityEngine.EventSystems;

public class BoardUI : TargetObject
{
    protected override void Awake()
    {
        base.Awake();
    }
    
    void Start()
    {
        Engine.instance.gameBus.onBeginGame += UpdateGameBoard;
    }

    void UpdateGameBoard(PlayPackage playPackage)
    {
        target = playPackage.gameBoard;
    }
}

public abstract class TargetObject<T> : TargetObject where T : ITarget
{
    T _Value;
    public T Value 
    { 
        get => _Value;
        set
        {
            if(_Value != null) HandleRemove(_Value);

            _Value = value;
            target = value;

            if(value != null) HandleSet(value);
        }
    }

    public virtual void HandleRemove(T t)
    {

    }

    public virtual void HandleSet(T t)
    {

    }
}

public class TargetObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDropHandler
{
    protected CardTray cardTray;
    protected GameInputHandler gameInputHandler;
    public ITarget target { get; protected set; }

    public delegate void OnEnter();
    public OnEnter onEnter;

    public delegate void OnExit();
    public OnExit onExit;

    public delegate void OnDropped();
    public OnDropped onDropped;

    protected virtual void Awake()
    {        
        gameInputHandler = GetComponentInParent<CardGame>()?.GetComponentInChildren<GameInputHandler>();
        cardTray = GetComponentInParent<CardTray>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CardController selectedCard = gameInputHandler.SelectedCard;

        if(selectedCard == null) return;

        Card card = selectedCard.Card;

        card.ApplyTarget(this.target);

        if(onEnter != null) onEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CardController selectedCard = gameInputHandler.SelectedCard;

        if(selectedCard == null) return;

        Card card = selectedCard.Card;

        card.ClearTargets();

        if(onExit != null) onExit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CardController selectedCard = gameInputHandler.SelectedCard;

        if(selectedCard == null) return;

        if(selectedCard.Card.CanPlay()) 
        {
            cardTray.Remove(selectedCard);
            selectedCard.Card.Prompt();
            Destroy(selectedCard.gameObject);

            gameInputHandler.SelectedCard = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(onDropped != null) onDropped();
    }
}
