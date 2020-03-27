using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfBehaviour : MonoBehaviour
{
    private SpriteRenderer render;
    public GameObject dialogueBox;
    private DialogueBox dialogueReceiver;
    private List<string> nameList1 = new List<string>(){"Wolf"};
    private List<string> messageList1 = new List<string>(){"Grrrr..."} ;
    private List<string> nameList2 = new List<string>(){"You", "You", "Wolf", "You", "Wolf", "You"};
    private List<string> messageList2 = new List<string>(){"If I wasn't already a ghost I'd be afraid of this thing.", 
    "Although...\nThat girl is a ghost and she was scared for some reason.", 
    "Grrr...", 
    "Uh oh.", 
    "Hungry...", 
    "Maybe if I can find him something to eat he'll go away... And not consume my incorporeal form?"};
    private List<string> nameList3 = new List<string>(){"Wolf", "You", "Wolf", "Congratulations!"};
    private List<string> messageList3 = new List<string>(){"Hungry...",
    "Well, I've got this gross old sandwich if you want?",
    "!!\n*The wolf takes the Three Little Pigs Sandwich*",
    "You helped the Wolf!"};
    private bool inRange;
    private PlayerController playerController;
    public GameObject girl;
    private girlBehaviour girlAttributes;
    private bool finished;
    public GameObject theTyper;
    private actionTyper typer;

    //private int messageCounter;

    void Start(){
        finished = false; //This is to make him dissapear after you're done with him!
        //messageCounter = 0;
        girlAttributes = girl.GetComponent<girlBehaviour>();
        render = GetComponent<SpriteRenderer>();
        inRange = false;
        dialogueReceiver = dialogueBox.GetComponent<DialogueBox>();
        typer = theTyper.GetComponent<actionTyper>();
    }
    void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.name == "Player" && other.gameObject.transform.position.x <= this.transform.position.x){
            render.flipX = true;
            inRange = true;
            playerController = other.gameObject.GetComponent<PlayerController>();
        }
        else if (other.gameObject.name == "Player" && other.gameObject.transform.position.x > this.transform.position.x){
            render.flipX = false;
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

    public void setFinished(bool set){
        finished = set;
    }

    public void resetText(){
        nameList1 = new List<string>(){"Wolf"};
        messageList1 = new List<string>(){"Grrrr..."} ;
        nameList2 = new List<string>(){"You", "You", "Wolf", "You", "Wolf", "You"};
        messageList2 = new List<string>(){"If I wasn't already a ghost I'd be afraid of this thing.", 
        "Although...\nThat girl is a ghost and she was scared for some reason.", 
        "Grrr...", 
        "Uh oh.", 
        "Hungry...", 
        "Maybe if I can find him something to eat he'll go away... And not consume my incorporeal form?"};
        nameList3 = new List<string>(){"Wolf", "You", "Wolf", "Congratulations!"};
        messageList3 = new List<string>(){"Hungry...",
        "Well, I've got this gross old sandwich if you want?",
        "!!\n*The wolf takes the Three Little Pigs Sandwich*",
        "You helped the Wolf!"};
    }

    void Update(){
        if (finished && playerController.enabled){
            gameObject.SetActive(false);
        }
        if (inRange && Input.GetKeyDown(KeyCode.Q) && !dialogueBox.activeSelf && playerController.enabled){
            playerController.enabled = false;
            typer.receiveAction(" You talk to the wolf.");
            dialogueBox.SetActive(true);
            //Reference DialogueBox. Give input text. Hand off to DialogueBox Script. Handle variables for story beats over here.
            if (girlAttributes.getFoundGrandma() == 0){
                dialogueReceiver.createDialogue(playerController, messageList1, nameList1);
            }
            else if (girlAttributes.getFoundGrandma() == 1 && !playerController.getInventory().Contains("Three Little Pigs Sandwich")){
                dialogueReceiver.createDialogue(playerController, messageList2, nameList2);
                
            }
            else{
                dialogueReceiver.createDialogue(playerController, messageList3, nameList3);
                playerController.removeItem("Three Little Pigs Sandwich");
                finished = true;
            }

        }
    }
}
