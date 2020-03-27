using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grandmaBehaviour : MonoBehaviour
{
    private SpriteRenderer render;
    public GameObject dialogueBox;
    private DialogueBox dialogueReceiver;
    private List<string> nameList1 = new List<string>(){"Grandma"};
    private List<string> messageList1 = new List<string>(){"Hello dear, what brings you to this part of the forest?"};
    private List<string> nameList2 = new List<string>(){"You", "Grandma", "Congratulations!", "You", "Grandma"};
    private List<string> messageList2 = new List<string>(){"Hi, are you the grandmother of the little girl\nstanding outside the forest?\nShe's worried sick about you.",
    "Standing outside the forest? She's supposed to stay home! Poor dear must've gotten worried.\nHere, show her this to let her know I'm alright.",
    "*You got the Old Key!*\nIt's pretty old!\nAnd certainly key shaped.",
    "Sure, but how come you can't go tell her yourself?",
    "My dear, I can't give up prime fishing hour!\nPlease, take it to her! Post haste!"};
    private bool inRange;
    private PlayerController playerController;
    public GameObject girl;
    private girlBehaviour girlAttributes;
    public GameObject theTyper;
    private actionTyper typer;

    void Start(){
        render = GetComponent<SpriteRenderer>();
        inRange = false;
        dialogueReceiver = dialogueBox.GetComponent<DialogueBox>();
        girlAttributes = girl.GetComponent<girlBehaviour>();
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
        nameList1 = new List<string>(){"Grandma"};
        messageList1 = new List<string>(){"Hello dear, what brings you to this part of the forest?"};
        nameList2 = new List<string>(){"You", "Grandma", "Congratulations!", "You", "Grandma"};
        messageList2 = new List<string>(){"Hi, are you the grandmother of the little girl\nstanding outside the forest?\nShe's worried sick about you.",
        "Standing outside the forest? She's supposed to stay home! Poor dear must've gotten worried.\nHere, show her this to let her know I'm alright.",
        "*You got the Old Key!*\nIt's pretty old!\nAnd certainly key shaped.",
        "Sure, but how come you can't go tell her yourself?",
        "My dear, I can't give up prime fishing hour!\nPlease, take it to her! Post haste!"};
    }

    void Update(){
        if (inRange && Input.GetKeyDown(KeyCode.Q) && !dialogueBox.activeSelf && playerController.enabled){
            typer.receiveAction(" You converse with the grandmother.");
            playerController.enabled = false;
            dialogueBox.SetActive(true);
            if (girlAttributes.getFoundGrandma() == 0){
                dialogueReceiver.createDialogue(playerController, messageList1, nameList1);
            }
            else {
                dialogueReceiver.createDialogue(playerController, messageList2, nameList2);
                if (girlAttributes.getFoundGrandma() != 2){
                    playerController.addItem("Old Key");
                    girlAttributes.setFoundGrandma(2);
                }
            }
        }
    }
}
