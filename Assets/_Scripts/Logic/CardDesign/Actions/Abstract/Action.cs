using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public interface ICardComponent : IDescription
{

}

public interface IOnRegisterEffects
{
    void OnRegister(Card card);
}

public interface IOnRemoveEffects
{
    void OnRemove(Card card);
}

public interface IPropertyCount : IDescription
{
    int Count(PlayPackage playPackage);
}

public interface IScaleWithProperty
{
    IPropertyCount propertyCount { get; }
}

public interface IAction : ICardComponent, IClonable<IAction>
{
    void Play(PlayPackage playPackage);
}

public interface IActionTags : ICardComponent
{
    ActionTag[] actionTags { get; }
}

public interface IKeywords : ICardComponent
{
    Keyword[] keywords { get; }
}

public static class ActionExtensions
{
    public static void ScaleAndPlay(this IAction i, PlayPackage playPackage, Card card)
    {
        IAction clone = i.Clone();

        playPackage.gameBus.RaiseScaleAction(playPackage, card, clone);

        clone.Play(playPackage);
    }

    public static void ScaleAndDescribe(this IAction i, PlayPackage playPackage, Card card, System.Text.StringBuilder stringBuilder)
    {
        IAction clone = i.Clone();

        playPackage.gameBus.RaiseScaleAction(playPackage, card, clone);

        stringBuilder.AppendLine(clone.GetDescription());
    }
}

public interface IResourceType
{
    ResourceType ResourceType { get; }
}

public interface IDescription
{
    string GetDescription();
}

public interface ITargetting<T> : ITargetting
    where T : class, ITarget
{
    T target { get; }
}

public interface ITargetting
{
    bool CanTarget(ITarget target);
    bool CanTargetAny(PlayPackage playPackage);
    void ApplyTarget(ITarget target);
    bool HasTarget();
    void ClearTarget();    
}

public interface ICost : ICardComponent, IClonable<ICost>
{
    bool CanPay(PlayPackage playPackage, Card card);
    void Pay(PlayPackage playPackage, Card card);
}

public delegate void OnPromptFilled();

public interface IPrompt : ICardComponent
{
    event OnPromptFilled onPromptFilled;
    bool Filled();
    bool CanFill(PlayPackage playPackage);
    void Prompt(PlayPackage playPackage);
    void Clear();
}

public interface IPrompt<T> : IPrompt
{
    List<T> population { get; }
    List<T> targets { get; }
    void Fill(List<T> targets);
    bool ValidTargets(List<T> targets);
    bool IsExcess(List<T> targets);
}

public static class CostExtensions
{
    public static void ScaleAndPay(this ICost i, PlayPackage playPackage, Card card)
    {
        ICost clone = i.Clone();

        playPackage.gameBus.RaiseScaleCost(playPackage, card, clone);

        clone.Pay(playPackage, card);
    }

    public static void ScaleAndDescribe(this ICost i, PlayPackage playPackage, Card card, System.Text.StringBuilder stringBuilder)
    {
        ICost clone = i.Clone();

        playPackage.gameBus.RaiseScaleCost(playPackage, card, clone);

        stringBuilder.AppendLine(clone.GetDescription());
    }
}

public class DefaultTargetting : ITargetting<GameBoard>
{
    public GameBoard target { get; set; }

    public void ApplyTarget(ITarget target)
    {
        if(CanTarget(target)) this.target = target as GameBoard;
    }

    public bool CanTarget(ITarget target)
    {
        return target is GameBoard;
    }

    public void ClearTarget()
    {
        target = null;
    }

    public bool HasTarget()
    {
        return target != null;
    }

    public bool CanTargetAny(PlayPackage playPackage)
    {
        return CanTarget(playPackage.gameBoard);
    }
}