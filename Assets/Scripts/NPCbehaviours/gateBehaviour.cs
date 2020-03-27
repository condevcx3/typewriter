using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateBehaviour : MonoBehaviour
{
    private SpriteRenderer render;
    public Sprite openGate;
    public GameObject dialogueBox;
    private DialogueBox dialogueReceiver;
    private List<string> nameList1 = new List<string>(){"Oh."};
    private List<string> messageList1 = new List<string>(){"It's locked."};
    public GameObject changeCol1;
    public GameObject background;
    
    private bool inRange;
    private PlayerController playerController;
    public GameObject theTyper;
    private actionTyper typer;


    void Start(){
        render = background.GetComponent<SpriteRenderer>();
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

    public void lockGate(){
        changeCol1.SetActive(true);
    }

    void Update(){
        if (inRange && Input.GetKeyDown(KeyCode.Q) && !dialogueBox.activeSelf && playerController.enabled){
            if (!playerController.getInventory().Contains("Old Key")){
                playerController.enabled = false;
                dialogueBox.SetActive(true);
                dialogueReceiver.createDialogue(playerController, messageList1, nameList1);
            }
            else if (render.sprite != openGate){
                typer.receiveAction(" You unlock the gate.");
                render.sprite = openGate;
                changeCol1.SetActive(false);
            }
        }
    }
}
