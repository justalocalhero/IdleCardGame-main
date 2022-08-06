using UnityEngine;

public class Brain
{
    public void ProcessTurn(GameBoard gameState, GameBus gameBus, Hand hand, Deck deck)
    {
        if(hand.cards.Count == 0) return;

        int index = Random.Range(0, hand.cards.Count);
        hand.cards[index].Prompt();
    }
}
