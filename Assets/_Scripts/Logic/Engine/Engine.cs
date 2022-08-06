using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public GameBoard gameBoard;
    public GameBus gameBus = new GameBus();
    public Deck deck;
    public Hand hand;
    public DiscardPile discardPile;

    public static Engine instance;
    public bool playing = false;

    public void Awake()
    {
        if(instance == null) instance = this;
        else
        {
            Destroy(this);
        }
    }

    public void Begin(Deck deck, List<GameProperty> properties)
    {
        playing = true;

        gameBoard = new GameBoard();

        this.deck = deck;
        
        deck.Shuffle();
        this.hand = new Hand();
        this.discardPile = new DiscardPile(gameBus);

        foreach(GameProperty property in properties)
        {
            property.Register(GetPlayPackage());
        }

        gameBus.RaiseOnBeginGame(GetPlayPackage());

        BeginTurn();
        
        DrawStartingHand();
    }

    public void EndGame()
    {
        playing = false;
        
        gameBus.RaiseOnEndGame(GetPlayPackage());
    }

    public void EndTurn()
    {
        PlayPackage playPackage = GetPlayPackage();

        gameBus.RaiseOnEndTurn(playPackage);
        gameBoard.turn++;

        BeginTurn();
    }

    public void BeginTurn()
    {
        PlayPackage playPackage = GetPlayPackage();

        gameBoard.SetEnergy(gameBoard.maxEnergy);
        hand.Draw(GetPlayPackage(), 1);
        gameBus.RaiseOnBeginTurn(playPackage);
    }

    public PlayPackage GetPlayPackage()
    {
        return new PlayPackage()
        {
            gameBoard = gameBoard,
            gameBus = gameBus,
            hand = hand,
            deck = deck,
            discardPile = discardPile
        };
    }

    public void DrawStartingHand()
    {
        hand.Draw(GetPlayPackage(), 5);
    }
}

public abstract class GameProperty
{
    public abstract void Register(PlayPackage playPackage);
    public abstract void DeRegister(PlayPackage playPackage);
}

public class EnergyGrowth : GameProperty
{
    private int lastCount;
    public int Remaining { get; private set; }

    public override void Register(PlayPackage playPackage)
    {
        playPackage.gameBoard.properties.Add(this);
        playPackage.gameBus.onEndTurn += UpdateMeter;
        playPackage.gameBus.onEndGame += DeRegister;
    }

    public override void DeRegister(PlayPackage playPackage)
    {
        playPackage.gameBoard.properties.Remove(this);
        playPackage.gameBus.onEndTurn -= UpdateMeter;
        playPackage.gameBus.onEndGame -= DeRegister;
    }
    
    public void UpdateMeter(PlayPackage playPackage)
    {
        if(Remaining <= 0)
        {
            Remaining = ++lastCount;
        }
        else if(--Remaining <= 0)
        {
            playPackage.gameBoard.maxEnergy++;
        }
    }
}