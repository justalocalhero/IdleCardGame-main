using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    public DeckManager deckManager;

    public delegate void OnChanged(bool valid);
    public OnChanged onChanged;

    void Awake()
    {
        deckManager.onChanged += Update;
    }

    public void Begin()
    {
        Engine.instance.Begin(deckManager.GetDeck(), new List<GameProperty>(){new EnergyGrowth()});
    }

    public void Update()
    {
        if(onChanged != null) onChanged(deckManager.ValidDeck());
    }
}
