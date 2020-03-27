using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAlBehaviour : MonoBehaviour
{
    private SpriteRenderer render;
    public GameObject dialogueBox;
    private DialogueBox dialogueReceiver;
    private List<string> nameList1 = new List<string>(){"Big Al", "Big Al", "You", "Big Al", "You", "Big Al", "Big Al", "Congratulations!", "Big Al"};
    private List<string> messageList1 = new List<string>(){"Whaddya lookin at, punk?\nScram! You're scaring away all my good business.",
    "Unless... Are you gonna be good business for me, kid?",
    "What do you even sell here?",
    "Didn't ya read the sign?\nFreshest flesh this side of the forest my guy.",
    "If I'm being honest, it's probably the worst slogan\nI've ever heard.",
    "Well if I'M bein honest, you're about the rudest\ncustomer I've ever had!",
    "Tell you what, take this here sandwich for free.\nMaybe then you'll learn to respect the Big Al's brand!",
    "*You got the Three Little Pigs Sandwich!*\n... You're not sure rotted animal carcasses between two slices of bread counts as a sandwich.",
    "Now scram for real!!\nI can't be seen giving out handouts all the time, it's bad for business."};
    private List<string> nameList2 = new List<string>(){"You", "Big Al", "Congratulations!", "Big Al"};
    private List<string> messageList2 = new List<string>(){"Hey Big Al, you got any more of those sandwiches?",
    "Couldn't get enough of the Fresh Flesh, eh?\nI knew this would happen!\nI got another sandwich just for you kid.",
    "*You got the Three Little Pigs Sandwich!*\nIs this really a \'congratulation\'s moment?",
    "But you owe me after this one!\nA favour for a favour, how about that, eh?"};
    private bool inRange;
    private PlayerController playerController;
    //public GameObject girl;
    //private girlBehaviour girlAttributes;
    private bool givenSandwich;
    public GameObject theTyper;
    private actionTyper typer;

    void Start(){
        givenSandwich = false;
        //girlAttributes = girl.GetComponent<girlBehaviour>();
        render = GetComponent<SpriteRenderer>();
        inRange = false;
        dialogueReceiver = dialogueBox.GetComponent<DialogueBox>();
        typer = theTyper.GetComponent<actionTyper>();
    }
    void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.name == "Player" && other.gameObject.transform.position.x <= this.transform.position.x){
            //render.flipX = true; I don't think Big Al should flip, he's too cool for that
            inRange = true;
            playerController = other.gameObject.GetComponent<PlayerController>();
        }
        else if (other.gameObject.name == "Player" && other.gameObject.transform.position.x > this.transform.position.x){
            //render.flipX = false;
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

    public void setGivenSandwich(bool set){
        givenSandwich = set;
    }

    public void resetText(){
        nameList1 = new List<string>(){"Big Al", "Big Al", "You", "Big Al", "You", "Big Al", "Big Al", "Congratulations!", "Big Al"};
        messageList1 = new List<string>(){"Whaddya lookin at, punk?\nScram! You're scaring away all my good business.",
        "Unless... Are you gonna be good business for me, kid?",
        "What do you even sell here?",
        "Didn't ya read the sign?\nFreshest flesh this side of the forest my guy.",
        "If I'm being honest, it's probably the worst slogan\nI've ever heard.",
        "Well if I'M bein honest, you're about the rudest\ncustomer I've ever had!",
        "Tell you what, take this here sandwich for free.\nMaybe then you'll learn to respect the Big Al's brand!",
        "*You got the Three Little Pigs Sandwich!*\n... You're not sure rotted animal carcasses between two slices of bread counts as a sandwich.",
        "Now scram for real!!\nI can't be seen giving out handouts all the time, it's bad for business."};
        nameList2 = new List<string>(){"You", "Big Al", "Congratulations!", "Big Al"};
        messageList2 = new List<string>(){"Hey Big Al, you got any more of those sandwiches?",
        "Couldn't get enough of the Fresh Flesh, eh?\nI knew this would happen!\nI got another sandwich just for you kid.",
        "*You got the Three Little Pigs Sandwich!*\nIs this really a \'congratulation\'s moment?",
        "But you owe me after this one!\nA favour for a favour, how about that, eh?"};
    }

    void Update(){
        if (inRange && Input.GetKeyDown(KeyCode.Q) && !dialogueBox.activeSelf && playerController.enabled){
            typer.receiveAction(" You shoot the breeze with Big Al.");
            if (!givenSandwich){
                if (!playerController.getInventory().Contains("Three Little Pigs Sandwich")){
                    playerController.addItem("Three Little Pigs Sandwich");
                }
                givenSandwich = true;
                playerController.enabled = false;
                dialogueBox.SetActive(true);
                dialogueReceiver.createDialogue(playerController, messageList1, nameList1);
            }
            else if (givenSandwich && !playerController.getInventory().Contains("Three Little Pigs Sandwich")){
                playerController.addItem("Three Little Pigs Sandwich");
                playerController.enabled = false;
                dialogueBox.SetActive(true);
                dialogueReceiver.createDialogue(playerController, messageList2, nameList2);
            }
            else{
                playerController.enabled = false;
                dialogueBox.SetActive(true);
                dialogueReceiver.createDialogue(playerController, messageList1, nameList1);
            }
        }
    }
}
