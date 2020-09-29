using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyHatBehaviour : MonoBehaviour
{
    private SpriteRenderer render;
    public GameObject dialogueBox;
    private DialogueBox dialogueReceiver;
    private List<string> nameList1 = new List<string>(){"Congratulations!"};
    private List<string> messageList1 = new List<string>(){"*You got the Party Hat!*\nIt's time to party and play!"};
    private bool inRange;
    private PlayerController playerController;
    public GameObject theTyper;
    private actionTyper typer;
    private bool gotHat;

    void Start(){
        gotHat = false;
        render = GetComponent<SpriteRenderer>();
        inRange = false;
        dialogueReceiver = dialogueBox.GetComponent<DialogueBox>();
        typer = theTyper.GetComponent<actionTyper>();
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

    public void resetText(){
        nameList1 = new List<string>(){"Congratulations!"};
        messageList1 = new List<string>(){"*You got the Party Hat!*\nIt's time to party and play!"};
    }

    void Update(){
        if (inRange && Input.GetKeyDown(KeyCode.Q) && !dialogueBox.activeSelf && playerController.enabled && !gotHat){
            gotHat = true;
            typer.receiveAction(" You snatch the hat!");
            playerController.addItem("Party Hat");
            playerController.enabled = false;
            render.enabled = false;
            dialogueBox.SetActive(true);
            dialogueReceiver.createDialogue(playerController, messageList1, nameList1);
        }
    }
}
