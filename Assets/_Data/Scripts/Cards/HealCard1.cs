using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCard1 : Card {
    public int healAmount;

    private CardType _cardType;
    public HealCard1() {
        _cardType = CardType.Heal;
    }
    
    public override void actionCard() {
        
        PlayerController.Instance.AddHealth();
        
    }
    
}
