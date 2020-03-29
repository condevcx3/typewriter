using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loudJeffBehaviour2 : MonoBehaviour
{
    private SpriteRenderer render;
    public GameObject player;
    public GameObject dialogueBox;
    private DialogueBox dialogueReceiver;
    // Giving Party Popper
    private List<string> nameList1 = new List<string>(){"LOUD JEFF", "LOUD JEFF", "You", "LOUD JEFF"};
    private List<string> messageList1 = new List<string>(){"DID YOU AVOID ALL THE STICKS??\nJOKES ON YOU, YOU'RE A GHOST!!\nYOU CAN'T STEP ON ANYTHING!!!!",
    "YOU'VE BEEN PRANKED!!\nHAHAHAHAHAHA!!",
    "...",
    "ENJOY YOUR PARTY POPPER!!\nHAHAHAHA"};
    private bool inRange;
    private PlayerController playerController;
    public GameObject loudJeff;
    public GameObject theTyper;
    private actionTyper typer;

    void Start(){
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

    public void resetText(){
        nameList1 = new List<string>(){"LOUD JEFF", "LOUD JEFF", "You", "LOUD JEFF"};
        messageList1 = new List<string>(){"DID YOU AVOID ALL THE STICKS??\nJOKES ON YOU, YOU'RE A GHOST!!\nYOU CAN'T STEP ON ANYTHING!!!!",
        "YOU'VE BEEN PRANKED!!\nHAHAHAHAHAHA!!",
        "...",
        "ENJOY YOUR PARTY POPPER!!\nHAHAHAHA"};
    }

    void Update(){
        if (inRange && Input.GetKeyDown(KeyCode.Q) && !dialogueBox.activeSelf && playerController.enabled){
            typer.receiveAction(" You completed the challenge!");
            playerController.enabled = false;
            dialogueBox.SetActive(true);
            // if player doesn't have party popper yet
            if (!(playerController.getInventory().Contains("Noisemaker"))) {
                dialogueReceiver.createDialogue(playerController, messageList1, nameList1);
                playerController.addItem("Noisemaker");
            }
        }
    }
}
