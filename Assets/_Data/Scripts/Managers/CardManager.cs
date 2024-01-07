using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    [SerializeField] private Stack<Card> StackedCards;
    

    // Start is called before the first frame update
    void Start()
    {

        StackedCards = new Stack<Card>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Card getCard()
    {
        return StackedCards.Peek();
    }

    public Card consumeCard()
    {
        Card actualCard = StackedCards.Pop();

        actualCard.actionCard();

        return actualCard;
    }
    
    public void addCard(Card card)
    {
        StackedCards.Push(card);
    }
}
