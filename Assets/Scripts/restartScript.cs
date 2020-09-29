using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class restartScript : MonoBehaviour
{
    public GameObject player;
    public GameObject girl;
    public GameObject grandma;
    public GameObject bigAl;
    public GameObject wolf;
    public GameObject clown;
    public GameObject loudJeff1;
    public GameObject loudJeff2;
    public GameObject gateArea;
    public Sprite gateLocked;
    public GameObject roadArea;
    public GameObject meadowArea;
    public GameObject forest1Area;
    public GameObject forest2Area;
    public GameObject forest3Area;
    public GameObject voidArea;
    public GameObject bigAlsArea;
    public GameObject startArea;
    public GameObject dialogueBox;
    public GameObject typer;
    public GameObject battleBox;
    public GameObject guard;
    public GameObject gateCollider;
    public GameObject partyWall;
    public GameObject quizBox;
    public GameObject songControl;
    public GameObject pedestalArea;
    public GameObject pathToStart1;
    public GameObject pathToStart2;
    public GameObject quietForest1;
    public GameObject quietForest2;
    public GameObject quietForest3;

    private List<string> nameList1 = new List<string>(){"You", "You"};
    private List<string> messageList1 = new List<string>(){"What just happened?",
    "I'm back where I started...\nAt least I still have all my stuff."};


    public void resetWorld(){
        //Reset world called on player death
        AudioSource[] songs = songControl.GetComponents<AudioSource>();
        for (int i = 0; i < songs.Length; i++){
            songs[i].Stop();
        }
        player.transform.position = new Vector3(0,0,0);
        girl.GetComponent<girlBehaviour>().setFoundGrandma(0);
        girl.GetComponent<girlBehaviour>().resetText(); //Just resetting text rather than writing whole new dialogue becuase I don't have enough time!
        girl.GetComponent<SpriteRenderer>().flipX = false;
        grandma.GetComponent<SpriteRenderer>().flipX = false;
        grandma.GetComponent<grandmaBehaviour>().resetText();
        wolf.GetComponent<SpriteRenderer>().flipX = false;
        wolf.GetComponent<wolfBehaviour>().setFinished(false);
        wolf.GetComponent<wolfBehaviour>().resetText();
        wolf.SetActive(true);
        bigAl.GetComponent<BigAlBehaviour>().setGivenSandwich(false);
        bigAl.GetComponent<BigAlBehaviour>().resetText();
        guard.GetComponent<guardBehaviour>().resetText();
        guard.GetComponent<guardBehaviour>().setBattleStarted(false);
        clown.GetComponent<clownBehaviour>().resetText();
        loudJeff1.GetComponent<SpriteRenderer>().flipX = false;
        loudJeff1.GetComponent<loudJeffBehaviour>().resetText();
        loudJeff2.GetComponent<SpriteRenderer>().flipX = false;
        loudJeff2.GetComponent<loudJeffBehaviour2>().resetText();
        gateArea.GetComponent<SpriteRenderer>().sprite = gateLocked;
        gateCollider.GetComponent<gateBehaviour>().lockGate();
        gateArea.transform.position = new Vector3(0,0,0);
        gateArea.SetActive(false);
        roadArea.transform.position = new Vector3(0,0,0);
        roadArea.SetActive(false);
        meadowArea.transform.position = new Vector3(0,0,0);
        meadowArea.SetActive(false);
        voidArea.transform.position = new Vector3(0,0,0);
        voidArea.SetActive(false);
        forest1Area.transform.position = new Vector3(0,0,0);
        forest1Area.SetActive(false);
        forest2Area.transform.position = new Vector3(0,0,0);
        forest2Area.SetActive(false);
        forest3Area.transform.position = new Vector3(0,0,0);
        forest3Area.SetActive(false);
        voidArea.transform.position = new Vector3(0,0,0);
        voidArea.SetActive(false);
        bigAlsArea.transform.position = new Vector3(0,0,0);
        bigAlsArea.SetActive(false);
        startArea.transform.position = new Vector3(0,0,0);
        startArea.SetActive(true);
        player.SetActive(true);
        player.GetComponent<SpriteRenderer>().enabled = true;
        battleBox.SetActive(false);
        pedestalArea.SetActive(false);
        pathToStart1.SetActive(false);
        pathToStart2.SetActive(false);
        quietForest1.SetActive(false);
        quietForest2.SetActive(false);
        quietForest3.SetActive(false);


        partyWall.transform.position = new Vector3(0,0,0);
        partyWall.SetActive(false);     
        quizBox.SetActive(false);

        typer.GetComponent<actionTyper>().reset();

        DialogueBox dialogueReceiver = dialogueBox.GetComponent<DialogueBox>();
        dialogueBox.SetActive(true);
        dialogueReceiver.createDialogue(player.GetComponent<PlayerController>(), messageList1, nameList1);

        typer.GetComponent<actionTyper>().receiveAction("You reawaken.");




    }
}
