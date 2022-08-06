using UnityEngine;
using UnityEngine.UI;

public class PromptGlow : MonoBehaviour
{
    private PromptCard promptCard;
    public Image image;

    void Awake()
    {
        promptCard = GetComponentInParent<PromptCard>();

        promptCard.onSelectedChanged += UpdateUI;
    }

    void UpdateUI()
    {
        image.enabled = promptCard.selected;
    }
}