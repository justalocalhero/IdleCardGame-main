using UnityEngine;
using TMPro;

public class ResourceInfo : MonoBehaviour
{
    private TextMeshProUGUI text;
    public ResourceManager resourceManager;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        resourceManager.onCoinsSet += (int value) => UpdateUI();
        UpdateUI();
    }

    void UpdateUI()
    {
        text.SetText(resourceManager.GetDescription());
    }
}
