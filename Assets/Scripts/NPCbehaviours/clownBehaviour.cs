using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clownBehaviour : MonoBehaviour
{
    private SpriteRenderer render;
    public GameObject dialogueBox;
    private DialogueBox dialogueReceiver;
    private List<string> nameList1 = new List<string>(){"???", "You", "???", "You", "???", "You", "Wall", "You", "Wall", "You", "Birthday Wall", "You", "Birthday Wall", "Birthday Wall"};
    private List<string> messageList1 = new List<string>(){"Hey",
    "Woah!!",
    "!! What! What is it?",
    "It-... Uh. You're in the wall?",
    "Oh! That!\nYeah you could say I -AM- the wall!",
    "That's pretty cool. I guess.\nHow is it in there?",
    "Well, it's alright normally,\nbut I'm feeling a bit down today.",
    "How come?",
    "See I wanted to throw this party,\nit's my birthday today,\nbut I don't have any party supplies!",
    "That's horrible!\nAnd I guess you can't really go out and get any, huh?",
    "Ah see, you get it!\nMaybe you could help me out?",
    "Somehow I feel obligated to.",
    "Great!! There's only two things I need\nto get this party started.",
    "I need you to find a Party Hat\nand a Noisemaker!\nI know they're around here somewhere..."};
    private List<string> nameList2 = new List<string>(){"You", "Birthday Wall", "You", "Birthday Wall"};
    private List<string> messageList2 = new List<string>(){"Guess who's got party supplies!",
    "Ooh a guessing game! My favourite!\nIs it Big Al?\nI've heard a lot about that guy.",
    "What? No.\nIt's me! I got you your Party Hat and Noisemaker.",
    "Really!? Well hand them over!!"};
    private List<string> nameList3 = new List<string>(){"???", "???", "???", "You", "???", "???", "You", "???", "You"};
    private List<string> messageList3 = new List<string>(){"Muahahaha!!\nYou fell victim to one of the classic blunders!",
    "The most famous is to never\nget involved in a land war in asia,\nbut only slightly less well known is this,",
    "Never do favours for a powerful being\ntrapped inside of a wall\nwhen death is on the line!",
    "I feel like you're paraphrasing something.",
    "It doesn't matter!\nNow that I'm armed with the Party Hat of Power\nand the Noisemaker of Doom,",
    "I will be unstoppable!!\nAnd it's going to be oh so much fun...",
    "The Party Hat of What?",
    "Ask no more questions!!\nIt's time to party and play!\nAnd you already know what my favourite game is!",
    "...\nAnd what are the rules?"};
    private List<string> nameList4 = new List<string>(){"???", "???", "???", "???"};
    private List<string> messageList4 = new List<string>(){"If you get 3 questions right, you win.\nIf you get 3 questions wrong, I win!",
    "Our lives on the line!",
    "Winner takes all!",
    "Ready or not, let's begin!"};
    private bool inRange;
    private PlayerController playerController;
    private bool spokenTo;
    private bool hasSupplies;
    private bool start4;
    private bool startGame;
    public GameObject theTyper;
    private actionTyper typer;

    public GameObject microphone;
    public GameObject lowerMic;
    public GameObject songControl;
    private AudioSource[] songs;
    public GameObject spotlight1;
    public GameObject spotlight2;
    public GameObject quizBox;

    private Vector3 originPosition;

    void Start(){
        originPosition.x = gameObject.transform.position.x;
        originPosition.y = gameObject.transform.position.y;
        originPosition.z = gameObject.transform.position.z;
        spokenTo = false;
        hasSupplies = false;
        start4 = false; //Start Conversation 4
        startGame = false;
        render = GetComponent<SpriteRenderer>();
        songs = songControl.GetComponents<AudioSource>();
        inRange = false;
        dialogueReceiver = dialogueBox.GetComponent<DialogueBox>();
        typer = theTyper.GetComponent<actionTyper>();
    }
    void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.name == "Player"){
            inRange = true;
            playerController = other.gameObject.GetComponent<PlayerController>();
            render.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.name == "Player" && !hasSupplies){
            inRange = false;
            playerController = null;
            render.enabled = false;
        }
    }

    public void resetText(){
        songs = songControl.GetComponents<AudioSource>();
        inRange = false;
        songs[6].volume = 1.0f;
        this.GetComponent<Animator>().enabled = false;
        nameList1 = new List<string>(){"???", "You", "???", "You", "???", "You", "Wall", "You", "Wall", "You", "Birthday Wall", "You", "Birthday Wall", "Birthday Wall"};
        messageList1 = new List<string>(){"Hey",
        "Woah!!",
        "!! What! What is it?",
        "It-... Uh. You're in the wall?",
        "Oh! That!\nYeah you could say I -AM- the wall!",
        "That's pretty cool. I guess.\nHow is it in there?",
        "Well, it's alright normally,\nbut I'm feeling a bit down today.",
        "How come?",
        "See I wanted to throw this party,\nit's my birthday today,\nbut I don't have any party supplies!",
        "That's horrible!\nAnd I guess you can't really go out and get any, huh?",
        "Ah see, you get it!\nMaybe you could help me out?",
        "Somehow I feel obligated to.",
        "Great!! There's only two things I need\nto get this party started.",
        "I need you to find a Party Hat\nand a Noisemaker!\nI know they're around here somewhere..."};
        nameList2 = new List<string>(){"You", "Birthday Wall", "You", "Birthday Wall"};
        messageList2 = new List<string>(){"Guess who's got party supplies!",
        "Ooh a guessing game! My favourite!\nIs it Big Al?\nI've heard a lot about that guy.",
        "What? No.\nIt's me! I got you your Party Hat and Noisemaker.",
        "Really!? Well hand them over!!"};
        nameList3 = new List<string>(){"???", "???", "???", "You", "???", "???", "You", "???", "You"};
        messageList3 = new List<string>(){"Muahahaha!!\nYou fell victim to one of the classic blunders!",
        "The most famous is to never\nget involved in a land war in asia,\nbut only slightly less well known is this,",
        "Never do favours for a powerful being\ntrapped inside of a wall\nwhen death is on the line!",
        "I feel like you're paraphrasing something.",
        "It doesn't matter!\nNow that I'm armed with the Party Hat of Power\nand the Noisemaker of Doom,",
        "I will be unstoppable!!\nAnd it's going to be oh so much fun...",
        "The Party Hat of What?",
        "Ask no more questions!!\nIt's time to party and play!\nAnd you already know what my favourite game is!",
        "...\nAnd what are the rules?"};
        nameList4 = new List<string>(){"???", "???", "???", "???"};
        messageList4 = new List<string>(){"If you get 3 questions right, you win.\nIf you get 3 questions wrong, I win!",
        "Our lives on the line!",
        "Winner takes all!",
        "Ready or not, let's begin!"};
    }


    private IEnumerator startParty(){
        //render.Sprite =
        //Maybe just start clown animation?
        this.GetComponent<Animator>().enabled = true;

        yield return new WaitForSeconds(1.0f);
        
        Vector3 newPosition = new Vector3();
        newPosition.x = this.gameObject.transform.position.x;
        newPosition.z = this.gameObject.transform.position.z;
        newPosition.y = 3.7f;

        while(newPosition != this.gameObject.transform.position){
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, newPosition, 0.05f);
            //Lerp is great but it takes a looooong time at the end to finish the animation
            if (songs[6].volume > 0){
                songs[6].volume -= 0.05f;
            }
            yield return new WaitForSeconds(0.01f);
        }

        //Play the music
        /*for (int i = 0; i < songs.length(); i++){
            songs[i].Stop();
        }*/
        //songs[6].Stop(); //Stop the background music song
        songs[5].Play(0);

        Vector3 micPosition = new Vector3();
        micPosition.x = -1.8f;
        micPosition.z = this.gameObject.transform.position.z;
        micPosition.y = 5.0f;

        while(micPosition != microphone.gameObject.transform.position){
            microphone.gameObject.transform.position = Vector3.MoveTowards(microphone.gameObject.transform.position, micPosition, 0.05f);
            //Lerp is great but it takes a looooong time at the end to finish the animation
            yield return new WaitForSeconds(0.01f);
        }

        micPosition.x = playerController.gameObject.transform.position.x + 1.5f;
        micPosition.z = playerController.gameObject.transform.position.z;
        micPosition.y = playerController.gameObject.transform.position.y - 0.5f;

        lowerMic.SetActive(true);

        while(micPosition != lowerMic.gameObject.transform.position){
            lowerMic.gameObject.transform.position = Vector3.MoveTowards(lowerMic.gameObject.transform.position, micPosition, 0.2f);
            //Lerp is great but it takes a looooong time at the end to finish the animation
            yield return new WaitForSeconds(0.005f);
        }

        yield return new WaitForSeconds(1.0f);
        spotlight1.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        spotlight2.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        //Start dialogue 3
        dialogueBox.SetActive(true);
        dialogueReceiver.createDialogue(playerController, messageList3, nameList3);


        start4 = true;
    }

    void Update(){
        if (startGame && inRange && playerController.enabled == true && !quizBox.activeSelf){
            playerController.stopMoving();
            playerController.enabled = false;
            this.GetComponent<Animator>().enabled = true;
            //this.GetComponent<BoxCollider2D>().enabled = false;
            //gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x, -3.5f);
            inRange = false;
            start4 = false;
            quizBox.SetActive(true);
            quizBox.GetComponent<quizSegment>().startup();
            songs[5].Stop();
            songs[6].volume = 0;
            dialogueBox.SetActive(true);
        }
        if (start4 && inRange && playerController.enabled == true){
            playerController.enabled = false;
            dialogueBox.SetActive(true);
            dialogueReceiver.createDialogue(playerController, messageList4, nameList4);
            startGame = true;
        }
        if (inRange && Input.GetKeyDown(KeyCode.Q) && hasSupplies && playerController.enabled == true){
            //Change to new sprite, slowly raise up, drop mic from top, start the music
            playerController.enabled = false;
            StartCoroutine(startParty());
        }
        if (inRange && Input.GetKeyDown(KeyCode.Q) && !dialogueBox.activeSelf && playerController.enabled){
            typer.receiveAction(" It's like talking to a brick wall.");
            if (spokenTo && playerController.getInventory().Contains("Party Hat") && playerController.getInventory().Contains("Noisemaker")){
                playerController.enabled = false;
                dialogueBox.SetActive(true);
                dialogueReceiver.createDialogue(playerController, messageList2, nameList2);
                hasSupplies = true;
            }
            else{
                spokenTo = true;
                playerController.enabled = false;
                dialogueBox.SetActive(true);
                dialogueReceiver.createDialogue(playerController, messageList1, nameList1);
            }
        }
    }
}
