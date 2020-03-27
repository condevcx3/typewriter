using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardBehaviour : MonoBehaviour
{
    private SpriteRenderer render;
    public GameObject dialogueBox;
    private DialogueBox dialogueReceiver;
    private List<string> nameList1 = new List<string>(){"Guard", "You", "Guard", "Guard", "You", "Guard", "You", "Guard", "You", "Guard"};
    private List<string> messageList1 = new List<string>(){"Stop! You shall not pass this point!",
    "Oh, sorry, I didn't realize...",
    "To get past, you will have to solve a riddle!",
    "Two guards stand before you,\none who only tells the truth\nand one who only lies....",
    "Where's the other guy?",
    "Er, he went to go grab us food.\nWe've been here for quite some time.\nMan... I'm so hungry...",
    "So what now?",
    "If you insist on getting through,\nthen it shall be a fight to the death!",
    "Aren't we already dead?",
    "Enough talk!\nEn Guarde!"};

    private bool battleStarted;
    public GameObject battleBox;
    
    private bool inRange;
    private PlayerController playerController;


    void Start(){
        battleStarted = false;
        render = GetComponent<SpriteRenderer>();
        inRange = false;
        dialogueReceiver = dialogueBox.GetComponent<DialogueBox>();
    }
    void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.name == "Player"){
            inRange = true;
            playerController = other.gameObject.GetComponent<PlayerController>();
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.name == "Player"){
            inRange = false;
            playerController = null;
        }
    }

    public void setBattleStarted(bool set){
        battleStarted = set;
    }

    public void resetText(){
        nameList1 = new List<string>(){"Guard", "You", "Guard", "Guard", "You", "Guard", "You", "Guard", "You", "Guard"};
        messageList1 = new List<string>(){"Stop! You shall not pass this point!",
        "Oh, sorry, I didn't realize...",
        "To get past, you will have to solve a riddle!",
        "Two guards stand before you,\none who only tells the truth\nand one who only lies....",
        "Where's the other guy?",
        "Er, he went to go grab us food.\nWe've been here for quite some time.\nMan... I'm so hungry...",
        "So what now?",
        "If you insist on getting through,\nthen it shall be a fight to the death!",
        "Aren't we already dead?",
        "Enough talk!\nEn Guarde!"};
    }

    void Update(){
        //The reason we can check if playerController is enabled here is because if inRange is false, then it stops checking at that point
        //If the if statement checked all the conditionals, then it could see that it was not initialized. When inRange is true,
        //playerController is initialized
        if (inRange && playerController.enabled && battleStarted){
            playerController.stopMoving();
            playerController.enabled = false;
            battleBox.SetActive(true);
            battleBox.GetComponent<battleSegment>().enemyActivate();

        }
        if (inRange && !dialogueBox.activeSelf && playerController.enabled && !battleStarted){
            playerController.stopMoving();
            playerController.enabled = false;
            dialogueBox.SetActive(true);
            dialogueReceiver.createDialogue(playerController, messageList1, nameList1);
            battleStarted = true;
        }
    }
}
