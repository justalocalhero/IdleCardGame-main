using TMPro;

public class PermanentUI : TargetObject<Permanent>
{
    public Permanent permanent { get; set; }
    TextMeshProUGUI text;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void HandleSet(Permanent permanent)
    {
        this.permanent = permanent;
        permanent.onRemoved += Kill;
        text.SetText(permanent.GetName());
    }

    public void Kill()
    {
        if(permanent != null) permanent.onRemoved -= Kill;
        Destroy(gameObject);
    }
}
