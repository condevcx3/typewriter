using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quizSegment : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public GameObject c;
    public GameObject d;
    private AudioSource[] audio;
    public Font currentSelection;
    public Font notSelected;
    private bool paused;
    private int selection;
    private int correctCount;
    private int wrongCount;
    //Current question = correctCount + wrongCount
    private PlayerController playerController;
    public GameObject theTyper;
    private actionTyper typer;
    
    public GameObject dialogueBox;
    private DialogueBox dialogueReceiver;
    public GameObject player;

    private bool selectionMade;

    private List<string> questionNum1 = new List<string>(){"Question 1"}; //Formatting like this so I can piggyback off dialogue code I've written
    private List<string> question1 = new List<string>(){"The name of Big Al's store is\nBig Al's Fresh..."};
    private int answer1 = 1; //0-3, corresponding to A, B, C, D
    private List<string> questionNum2 = new List<string>(){"Question 2"}; //Formatting like this so I can piggyback off dialogue code I've written
    private List<string> question2 = new List<string>(){"Which of the following animals\nhas cubed shaped poop?"};
    private int answer2 = 3;
    private List<string> questionNum3 = new List<string>(){"Question 3"}; //Formatting like this so I can piggyback off dialogue code I've written
    private List<string> question3 = new List<string>(){"What did you get from the Grandmother\nto show to her Granddaughter?"};
    private int answer3 = 0;
    private List<string> questionNum4 = new List<string>(){"Question 4"}; //Formatting like this so I can piggyback off dialogue code I've written
    private List<string> question4 = new List<string>(){"Lovely weather we're having today isn't it?\nWhich of these is a cloud formation?"};
    private int answer4 = 2;
    private List<string> questionNum5 = new List<string>(){"Question 5"}; //Formatting like this so I can piggyback off dialogue code I've written
    private List<string> question5 = new List<string>(){"What special event is happening today?\nAnd you'd better get this one right!!"};
    private int answer5 = 2;

    private List<string> youWinNames = new List<string>(){"???", "???", "You"};
    private List<string> youWinText = new List<string>(){"So! You're not as dim as you look!\nAnd sound! And smell!",
    "You may have won this time, but I'll be back!\nYou'll see, I'll never let you leave this place alive!",
    "Aren't we both still dead?\n...\nWhatever."};
    
    public GameObject youWin;
    public GameObject clown;
    public RuntimeAnimatorController clownMelt;

    private void setSelection1(){
        a.GetComponent<Text>().text = "Fish";
        b.GetComponent<Text>().text = "Flesh";
        c.GetComponent<Text>().text = "Frosh";
        d.GetComponent<Text>().text = "Fixings";
    }

    private void setSelection2(){
        a.GetComponent<Text>().text = "Goldfish";
        b.GetComponent<Text>().text = "Boar";
        c.GetComponent<Text>().text = "Lemur";
        d.GetComponent<Text>().text = "Wombat";
    }

    private void setSelection3(){
        a.GetComponent<Text>().text = "Old Key";
        b.GetComponent<Text>().text = "New Key";
        c.GetComponent<Text>().text = "Two Keys";
        d.GetComponent<Text>().text = "Blue Key";
    }
    private void setSelection4(){
        a.GetComponent<Text>().text = "Foundry";
        b.GetComponent<Text>().text = "Alto";
        c.GetComponent<Text>().text = "Cumulus";
        d.GetComponent<Text>().text = "Patronum";
    }
    private void setSelection5(){
        a.GetComponent<Text>().text = "Valentines Day";
        b.GetComponent<Text>().text = "Pancake Day";
        c.GetComponent<Text>().text = "Your Birthday";
        d.GetComponent<Text>().text = "Our Anniversary";
    }

    private void nextQuestion(){
        switch(correctCount + wrongCount){ //Plus one because it starts with the first one anyway
            case 1:
                setSelection2();
                dialogueReceiver.createDialogue(playerController, question2, questionNum2);
                break;
            case 2:
                setSelection3();
                dialogueReceiver.createDialogue(playerController, question3, questionNum3);
                break;
            case 3:
                setSelection4();
                dialogueReceiver.createDialogue(playerController, question4, questionNum4);
                break;
            default:
                setSelection5();
                dialogueReceiver.createDialogue(playerController, question5, questionNum5);
                break;
        }
    }

    private IEnumerator waitNextQuestion(){
        Debug.Log("Start Waiting");
        yield return new WaitForSeconds(4.0f);
        Debug.Log("Done Waiting");
        nextQuestion();
        audio[2].Play(0);
        selectionMade = false;
    }

    private IEnumerator youWinGame(){
        clown.GetComponent<Animator>().runtimeAnimatorController = clownMelt;
        yield return new WaitForSeconds(4.0f);
        typer.receiveAction(" You won the scary trivia game!");
        yield return new WaitForSeconds(2.0f);
        youWin.SetActive(true);
    }


    public void startup()
    {
        audio = this.GetComponents<AudioSource>();
        selection = 0;
        correctCount = 0;
        wrongCount = 0;
        selectionMade = false;
        typer = theTyper.GetComponent<actionTyper>();
        playerController = player.GetComponent<PlayerController>();
        playerController.setPause(true);
        dialogueBox.SetActive(true);
        dialogueReceiver = dialogueBox.GetComponent<DialogueBox>();
        dialogueReceiver.activatePartyMode();
        dialogueReceiver.createDialogue(playerController, question1, questionNum1);
        setSelection1();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused){
            if (selection == 0){
                a.GetComponent<Text>().font = currentSelection;
                b.GetComponent<Text>().font = notSelected;
                c.GetComponent<Text>().font = notSelected;
                d.GetComponent<Text>().font = notSelected;
            }
            else if (selection == 1){
                a.GetComponent<Text>().font = notSelected;
                b.GetComponent<Text>().font = currentSelection;
                c.GetComponent<Text>().font = notSelected;
                d.GetComponent<Text>().font = notSelected;
            }
            else if (selection == 2){
                a.GetComponent<Text>().font = notSelected;
                b.GetComponent<Text>().font = notSelected;
                c.GetComponent<Text>().font = currentSelection;
                d.GetComponent<Text>().font = notSelected;
            }
            else if (selection == 3){
                a.GetComponent<Text>().font = notSelected;
                b.GetComponent<Text>().font = notSelected;
                c.GetComponent<Text>().font = notSelected;
                d.GetComponent<Text>().font = currentSelection;
            }
            if (Input.GetKeyDown(KeyCode.Q) && !selectionMade){
                audio[2].Stop();
                selectionMade = true;
                switch(correctCount + wrongCount){ //Determines which question
                    case 0:
                        if (selection == answer1){
                            //Correct
                            correctCount++;
                            audio[0].Play(0);
                        }
                        else{
                            //Incorrect
                            wrongCount++;
                            audio[1].Play(0);

                        }
                        break;
                    case 1:
                        if (selection == answer2){
                            //Correct
                            correctCount++;
                            audio[0].Play(0);
                        }
                        else{
                            //Incorrect
                            wrongCount++;
                            audio[1].Play(0);
                        }
                        break;
                    case 2:
                        if (selection == answer3){
                            //Correct
                            correctCount++;
                            audio[0].Play(0);
                        }
                        else{
                            //Incorrect
                            wrongCount++;
                            audio[1].Play(0);
                        }
                        break;
                    case 3:
                        if (selection == answer4){
                            //Correct
                            correctCount++;
                            audio[0].Play(0);
                        }
                        else{
                            //Incorrect
                            wrongCount++;
                            audio[1].Play(0);
                        }
                        break;
                    default:
                        if (selection == answer5){
                            //Correct
                            correctCount++;
                            audio[0].Play(0);
                        }
                        else{
                            //Incorrect
                            wrongCount++;
                            audio[1].Play(0);
                        }
                        break;
                }
                if (wrongCount == 3){
                    paused = true;
                    correctCount = 0;
                    wrongCount = 0;
                    dialogueReceiver.deactivatePartyMode();
                    StartCoroutine(playerController.killPlayer());
                    paused = false;
                }
                else if (correctCount == 3){
                    StartCoroutine(youWinGame());
                }
                else{
                    StartCoroutine(waitNextQuestion());
                }
            }
            if (Input.GetKeyDown(KeyCode.D)){
                selection = (selection + 1) % 4;
            }
            else if (Input.GetKeyDown(KeyCode.A)){
                //selection = (selection - 1) % 4; Negatives don't wrap around apparently
                selection = selection - 1;
                if (selection < 0){
                    selection = 3;
                }
            }
        }
    }
}
