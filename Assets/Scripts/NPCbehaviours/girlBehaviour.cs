using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girlBehaviour : MonoBehaviour
{
    private SpriteRenderer render;
    public GameObject dialogueBox;
    private DialogueBox dialogueReceiver;
    private List<string> nameList1 = new List<string>(){"Young Girl", "You", "Young Girl", "You", "Young Girl", "You", "Young Girl", "You"};
    private List<string> messageList1 = new List<string>(){"Oh Grandma, I'm so worried!\nJust wandering into the forest all alone...", 
    "Do you need help?",
    "Oh! I'm not supposed to talk to strangers but...\nMy grandma went in there this morning!\nCould you find her and make sure she's okay?",
    "I guess. How come you can't go yourself?",
    "The forest is too dangerous!",
    "But you think I can make it through?",
    "...",
    "Alright, fine.\nI'll let you know when I find her."};
    private List<string> nameList2 = new List<string>(){"Young Girl", "You", "Congratulations!", "Young Girl", "You", "Young Girl"};
    private List<string> messageList2 = new List<string>(){"*GASP* That's Grandma's favourite old key!!\nDoes that mean you found her?",
    "Yeah, she was fishing. She seemed just fine.",
    "You helped the Young Girl!",
    "That's so wonderful! Thank you so much kind stranger!\nYou can keep that old key as payment!!", 
    "Why would I want it??",
    "Because the key opens that gate just north of here!\nDon't tell me you aren't curious about what's on the other side?"};
    
    private bool inRange;
    private PlayerController playerController;

    private int foundGrandma; //0 is not started, 1 is quest accepted, 2 is grandma found
    public GameObject theTyper;
    private actionTyper typer;

    void Start(){
        foundGrandma = 0;
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

    public void setFoundGrandma(int level){
        foundGrandma = level;
    }
    public int getFoundGrandma(){
        return foundGrandma;
    }

    public void resetText(){ //All of the resetText functions are just quick fixes for huge oversights made that I found before submitting!
        nameList1 = new List<string>(){"Young Girl", "You", "Young Girl", "You", "Young Girl", "You", "Young Girl", "You"};
        messageList1 = new List<string>(){"Oh Grandma, I'm so worried!\nJust wandering into the forest all alone...", 
        "Do you need help?",
        "Oh! I'm not supposed to talk to strangers but...\nMy grandma went in there this morning!\nCould you find her and make sure she's okay?",
        "I guess. How come you can't go yourself?",
        "The forest is too dangerous!",
        "But you think I can make it through?",
        "...",
        "Alright, fine.\nI'll let you know when I find her."};
        nameList2 = new List<string>(){"Young Girl", "You", "Congratulations!", "Young Girl", "You", "Young Girl"};
        messageList2 = new List<string>(){"*GASP* That's Grandma's favourite old key!!\nDoes that mean you found her?",
        "Yeah, she was fishing. She seemed just fine.",
        "You helped the Young Girl!",
        "That's so wonderful! Thank you so much kind stranger!\nYou can keep that old key as payment!!", 
        "Why would I want it??",
        "Because the key opens that gate just north of here!\nDon't tell me you aren't curious about what's on the other side?"};
    }

    void Update(){
        if (inRange && Input.GetKeyDown(KeyCode.Q) && !dialogueBox.activeSelf && playerController.enabled){
            playerController.enabled = false;
            typer.receiveAction(" You speak to the girl.");
            dialogueBox.SetActive(true);
            if (foundGrandma == 0 || foundGrandma == 1){
                setFoundGrandma(1);
                dialogueReceiver.createDialogue(playerController, messageList1, nameList1);
            }
            else if (foundGrandma == 2){
                dialogueReceiver.createDialogue(playerController, messageList2, nameList2);
            }
            
            
        }
    }
}
