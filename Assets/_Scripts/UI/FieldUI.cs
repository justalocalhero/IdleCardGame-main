using UnityEngine;

public class FieldUI : TargetObject<Field>
{
    public PermanentUI prefab;
    public Transform container;

    public override void HandleRemove(Field field)
    {
        field.onPermanentSet -= HandlePermanent;
    }

    public override void HandleSet(Field field)
    {
        field.onPermanentSet += HandlePermanent;
    }

    protected override void Awake()
    {
        base.Awake();
        Value = new TestField();
    }

    void HandlePermanent(Permanent permanent)
    {
        PermanentUI permanentUI = Instantiate(prefab, container);
        permanentUI.Value = permanent;
    }
}
