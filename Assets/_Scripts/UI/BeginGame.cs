using UnityEngine;
using UnityEngine.UI;

public class BeginGame : MonoBehaviour
{    
    public GameLauncher gameLauncher;
    private Button button;

    void Awake()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(Fire);
        gameLauncher.onChanged += UpdateUI;
    }

    void UpdateUI(bool ready)
    {
        button.interactable = ready;
    }

    void Fire()
    {
        gameLauncher.Begin();
    }
}