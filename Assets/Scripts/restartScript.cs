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

    private List<string> nameList1 = new List<string>(){"You", "You"};
    private List<string> messageList1 = new List<string>(){"What just happened?",
    "I'm back where I started...\nAt least I still have all my stuff."};


    public void resetWorld(){
        //Reset world called on player death
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

        typer.GetComponent<actionTyper>().reset();

        DialogueBox dialogueReceiver = dialogueBox.GetComponent<DialogueBox>();
        dialogueBox.SetActive(true);
        dialogueReceiver.createDialogue(player.GetComponent<PlayerController>(), messageList1, nameList1);

        typer.GetComponent<actionTyper>().receiveAction("You reawaken.");




    }
}
