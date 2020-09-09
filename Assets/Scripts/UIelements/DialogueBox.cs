using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{

    public GameObject dialogueText;
    public GameObject displayName;
    private IEnumerator coroutine;
    private string fullText;
    private bool messageDisplayed;
    private PlayerController playerController;
    private List<string> dialoguePipeline;
    private List<string> namePipeline;
    private bool partyMode;

    void Start(){
        partyMode = false;
    }

    public void createDialogue(PlayerController playerControl, List<string> dialogueList, List<string> nameList){
        playerController = playerControl; //new List<string>(playerControl); if you want the convo to start over again
        dialoguePipeline = dialogueList;
        namePipeline = nameList;
        coroutine = typeDialogue();

        StartCoroutine(coroutine);
    }

    public void activatePartyMode(){
        partyMode = true;
    }
    public void deactivatePartyMode(){
        partyMode = false;
    }

    IEnumerator typeDialogue(){
        messageDisplayed = false;
        string currentText = "";
        float delay = 0.025f;
        fullText = dialoguePipeline[0];
        Debug.Log(fullText);
        displayName.GetComponent<Text>().text = namePipeline[0];
        for(int i = 0; i < dialoguePipeline[0].Length; i++){
            currentText += dialoguePipeline[0].Substring(i, 1);
            dialogueText.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
            //Maybe add in some kind of text deleting after three \n's are found
        }
        messageDisplayed = true;
    }

    void Update(){
        if (messageDisplayed && Input.GetKeyDown(KeyCode.Q)){
            if (dialoguePipeline.Count > 1){
                dialoguePipeline.RemoveAt(0);
                namePipeline.RemoveAt(0);
                coroutine = typeDialogue();
                StartCoroutine(coroutine);
                //Debug.Log("New coroutine");
            }
            else{ //Else there are no more messages to display
                if (!partyMode){
                    playerController.enabled = true;
                    gameObject.SetActive(false);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q)){
            StopCoroutine(coroutine);
            dialogueText.GetComponent<Text>().text = fullText;
            messageDisplayed = true;
            //This may need work if message has more than 4 lines
        }
    }
}
