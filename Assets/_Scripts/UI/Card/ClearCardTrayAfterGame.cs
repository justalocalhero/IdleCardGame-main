using UnityEngine;

public class ClearCardTrayAfterGame : MonoBehaviour
{
    CardTray cardTray;

    void Awake()
    {
        cardTray = GetComponentInParent<CardTray>();
    }
    
    void Start()
    {
        Engine.instance.gameBus.onEndGame += (PlayPackage playPackage) => cardTray.Clear();
    }
}
