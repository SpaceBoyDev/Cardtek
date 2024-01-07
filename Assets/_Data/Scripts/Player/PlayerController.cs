using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    
    public static PlayerController Instance;
    
    //Int value for the Player Health
    [SerializeField] public IntValue playerHealth;
    //Game Event when the PlayerLifeChange
    [SerializeField] public GameEvent onPlayerLifeChange;

    [SerializeField] private CardManager _cardManager;
    
    private bool UsedCard;

    private void Awake() {
        Instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        UsedCard = PlayerInputManager.Instance.GetUseCard();
        
        UseCard();
    }

    void FixedUpdate()
    {
        
    }
    
    //When the player is hit , we decreased the playerhealth and raise the event
    public void SubstractHealth() {
        playerHealth.runtimeValue--;
        onPlayerLifeChange.Raise();

    }

    //When the player is hit , we increased the playerhealth and raise the event
    public void AddHealth() {
        playerHealth.runtimeValue++;
        onPlayerLifeChange.Raise();
    }
      
    private void UseCard()
    {

        if (UsedCard)
        {
            _cardManager.consumeCard();
        }
       
    }
}
