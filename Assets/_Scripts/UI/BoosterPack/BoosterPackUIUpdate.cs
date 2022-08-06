using UnityEngine;

public abstract class BoosterPackUIUpdate : MonoBehaviour
{
    private BoosterPackUI boosterPackUI;

    void Awake()
    {
        boosterPackUI = GetComponentInParent<BoosterPackUI>();

        boosterPackUI.onUpdateUI += UpdateUI;

        OnAwake();
    }
    
    void OnDestroy()
    {
        boosterPackUI.onUpdateUI -= UpdateUI;
    }

    protected virtual void OnAwake()
    {
        
    }


    protected abstract void UpdateUI(BoosterPack boosterPack);
}