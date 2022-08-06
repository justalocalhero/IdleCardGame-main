public class GameBus
{   
    public delegate void OnChanged(PlayPackage playPackage);
    public OnChanged onChanged;

    public delegate void OnBeginGame(PlayPackage playPackage);
    public OnBeginGame onBeginGame;

    public void RaiseOnBeginGame(PlayPackage playPackage)
    {
        if(onBeginGame != null) onBeginGame(playPackage);
        if(onChanged != null) onChanged(playPackage);
    }
    
    public delegate void OnEndGame(PlayPackage playPackage);
    public OnEndGame onEndGame;
    
    public void RaiseOnEndGame(PlayPackage playPackage)
    {
        if(onEndGame != null) onEndGame(playPackage);
        if(onChanged != null) onChanged(playPackage);
    }

    public delegate void OnBeginTurn(PlayPackage playPackage);
    public OnBeginTurn onBeginTurn;

    public void RaiseOnBeginTurn(PlayPackage playPackage)
    {
        if(onBeginTurn != null) onBeginTurn(playPackage);
        if(onChanged != null) onChanged(playPackage);
    }

    public delegate void OnEndTurn(PlayPackage playPackage);
    public OnEndTurn onEndTurn;
    
    public void RaiseOnEndTurn(PlayPackage playPackage)
    {
        if(onEndTurn != null) onEndTurn(playPackage);
        if(onChanged != null) onChanged(playPackage);
    }

    public delegate void OnCardPlayed(PlayPackage playPackage, Card card);
    public OnCardPlayed onCardPlayed;

    public void RaiseOnCardPlayed(PlayPackage playPackage, Card card)
    {
        if(onCardPlayed != null) onCardPlayed(playPackage, card);
        if(onChanged != null) onChanged(playPackage);
    }

    public delegate void OnPrompt(PlayPackage playPackage, IPrompt prompt);
    public OnPrompt onPrompt;

    public void RaiseOnPrompt(PlayPackage playPackage, IPrompt prompt)
    {
        if(onPrompt != null) onPrompt(playPackage, prompt);
    }

    public delegate void OnCardDrawn(PlayPackage playPackage, Card card);
    public OnCardDrawn onCardDrawn;

    public void RaiseOnCardDrawn(PlayPackage playPackage, Card card)
    {
        if(onCardDrawn != null) onCardDrawn(playPackage, card);
        if(onChanged != null) onChanged(playPackage);
    }

    public delegate void OnCardDiscarded(PlayPackage playPackage, Card card);
    public OnCardDiscarded onCardDiscarded;

    public void RaiseOnCardDiscarded(PlayPackage playPackage, Card card)
    {
        if(onCardDiscarded != null) onCardDiscarded(playPackage, card);
        if(onChanged != null) onChanged(playPackage);
    }

    public delegate void OnEffectAdded(PlayPackage playPackage, Effect effect);
    public OnEffectAdded onEffectAdded;

    public void RaiseOnEffectAdded(PlayPackage playPackage, Effect effect)
    {
        if(onEffectAdded != null) onEffectAdded(playPackage, effect);
        if(onChanged != null) onChanged(playPackage);
    }

    public delegate void OnEffectRemoved(PlayPackage playPackage, Effect effect);
    public OnEffectRemoved onEffectRemoved;

    public void RaiseOnEffectRemoved(PlayPackage playPackage, Effect effect)
    {
        if(onEffectRemoved != null) onEffectRemoved(playPackage, effect);
        if(onChanged != null) onChanged(playPackage);
    }

    public delegate void ScaleAction(PlayPackage playPackage, Card card, IAction action);
    public ScaleAction scaleAction;
    
    public void RaiseScaleAction(PlayPackage playPackage, Card card, IAction action)
    {
        if(scaleAction != null) scaleAction(playPackage, card, action);
    }

    public delegate void ScaleCost(PlayPackage playPackage, Card card, ICost cost);
    public ScaleCost scaleCost;

    public void RaiseScaleCost(PlayPackage playPackage, Card card, ICost cost)
    {
        if(scaleCost != null) scaleCost(playPackage, card, cost);
    }

    public delegate void OnFieldsChanged(PlayPackage playPackage);
    public OnFieldsChanged onFieldsChanged;

    public void RaiseOnFieldsChanged(PlayPackage playPackage)
    {
        if(onFieldsChanged != null) onFieldsChanged(playPackage);
        if(onChanged != null) onChanged(playPackage);
    }
}