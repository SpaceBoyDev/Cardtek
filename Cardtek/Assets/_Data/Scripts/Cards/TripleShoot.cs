using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShoot : Card
{
    
    private CardType _cardType;

    public TripleShoot(CardType cardType)
    {
        _cardType = CardType.TripleShoot;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void actionCard()
    {
        throw new System.NotImplementedException();
    }
}
