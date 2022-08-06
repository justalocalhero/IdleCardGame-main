using System.Collections.Generic;
using UnityEngine;

public class DiscardRandomCards : IAction, IActionTags
{
    public int DiscardValue { get; set; }
    public Scaling Scaling { get; private set; }
    public GameBoard target { get; set; }

    public ActionTag[] actionTags => _actionTags;
    public ActionTag[] _actionTags = { ActionTag.Discard };

    public DiscardRandomCards(int discardValue)
    {
        DiscardValue = discardValue;
        Scaling = new Scaling();

    }

    public IAction Clone()
    {
        return new DiscardRandomCards(DiscardValue);
    }

    public string GetDescription()
    {
        int value = Scaling.Scale(DiscardValue);

        if(value <= 0) return "";

        return "Discard " + value + " cards at random.";
    }

    public void Play(PlayPackage playPackage)
    {
        int value = Scaling.Scale(DiscardValue);

        if(value <= 0) return;

        playPackage.hand.DiscardRandom(playPackage, value);

    }
}
