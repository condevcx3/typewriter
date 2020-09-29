using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loudJeffBehaviour : MonoBehaviour
{
    private SpriteRenderer render;
    public GameObject dialogueBox;
    private DialogueBox dialogueReceiver;
    // Pre Party Popper
    private List<string> nameList1 = new List<string>(){"LOUD JEFF", "You", "LOUD JEFF", "LOUD JEFF", "LOUD JEFF"};
    private List<string> messageList1 = new List<string>(){"HELLO BOY!\nI HEARD YOU'RE LOOKING FOR A NOISEMAKER!",
    "I am, but how did you know that?",
    "DON'T SWEAT THE MINOR DETAILS!\nIF YOU COMPLETE MY CHALLENGE I WILL GIVE YOU ONE!",
    "IF YOU CAN GET THROUGH MY MAZE!!\nWITHOUT STEPPING ON A SINGLE STICK!!\nTHE NOISEMAKER IS YOURS!!",
    "WHAT ARE YOU STILL DOING HERE?\nTHE CHALLENGE AWAITS!"};
    // Post Party Popper
    private List<string> nameList2 = new List<string>(){"LOUD JEFF"};
    private List<string> messageList2 = new List<string>(){"GHOSTS CAN'T STEP ON THINGS!!\nHAHAHAHAHHAHAHAH"};
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
        inRange = false;
        nameList1 = new List<string>(){"LOUD JEFF", "You", "LOUD JEFF", "LOUD JEFF", "LOUD JEFF"};
        messageList1 = new List<string>(){"HELLO BOY!\nI HEARD YOU'RE LOOKING FOR A NOISEMAKER!",
        "I am, but how did you know that?",
        "DON'T SWEAT THE MINOR DETAILS!\nIF YOU COMPLETE MY CHALLENGE I WILL GIVE YOU ONE!",
        "IF YOU CAN GET THROUGH MY MAZE!!\nWITHOUT STEPPING ON A SINGLE STICK!!\nTHE NOISEMAKER IS YOURS!!",
        "WHAT ARE YOU STILL DOING HERE?\nTHE CHALLENGE AWAITS!"};
        nameList2 = new List<string>(){"LOUD JEFF"};
        messageList2 = new List<string>(){"GHOSTS CAN'T STEP ON THINGS!!\nHAHAHAHAHHAHAHAH"};
    }

    void Update(){
        if (inRange && Input.GetKeyDown(KeyCode.Q) && !dialogueBox.activeSelf && playerController.enabled){
            Debug.Log("Test");
            typer.receiveAction(" You converse with Loud Jeff.");
            playerController.enabled = false;
            dialogueBox.SetActive(true);
            // if player doesn't have party popper yet
            Debug.Log(playerController.getInventory().Contains("Noisemaker"));
            if (!(playerController.getInventory().Contains("Noisemaker"))) {
                dialogueReceiver.createDialogue(playerController, messageList1, nameList1);
            }
            // if the player does have the party popper
            else {
                dialogueReceiver.createDialogue(playerController, messageList2, nameList2);
            }
        }
    }
}
