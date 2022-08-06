using UnityEngine;

public class DefaultSize : MonoBehaviour
{    
    public RectTransform container;
    public Vector3 Value { get; set; }

    public void Awake()
    {
        Value = container.transform.localScale;
    }
}
