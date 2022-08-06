using UnityEngine;

public class ClearCardOnEndTurn : MonoBehaviour
{
    public GameInputHandler gameInputHandler;

    public void Awake()
    {        
        gameInputHandler = GetComponentInParent<CardGame>()?.GetComponentInChildren<GameInputHandler>();
    }
    
    public void Start()
    {
        Engine.instance.gameBus.onEndTurn += Fire;
    }

    public void Fire(PlayPackage playPackage)
    {
        gameInputHandler.SelectedCard = null;
    }
}