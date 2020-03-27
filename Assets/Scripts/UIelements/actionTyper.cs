using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; //For using List.Any() to check for an empty list

public class actionTyper : MonoBehaviour
{
    private List<string> pipeline;
    private bool isTyping;
    private string currentText;
    private int characterCount;
    private float delay;
    //private string fullText;
    private int newlineCount;
    public GameObject player;
    public bool pause;

    void Start(){
        pause = false;
        pipeline = new List<string>();
        isTyping = false;
        currentText = "";
        //fullText = "";
        newlineCount = 0;
        characterCount = 0;
        receiveAction("You awaken.");
    }


    public void reset(){
        this.GetComponent<Text>().text = "";
        this.GetComponent<Text>().color = Color.white;
        pause = false;
        pipeline = new List<string>();
        isTyping = false;
        currentText = "";
        //fullText = "";
        newlineCount = 0;
        characterCount = 0;
    }

    public void receiveAction(string actionText){
        if (!pause){
            pipeline.Add(actionText);
            if (!isTyping){
                isTyping = true;
                StartCoroutine(type());
            }
        }
    }

    public void receiveActionBypass(string actionText){ //Special function for bypassing the pause function!
        pipeline.Add(actionText);
        if (!isTyping){
            isTyping = true;
            StartCoroutine(type());
        }
    }

    public void pauseReceiver(bool set){
        pause = set;
    }

    IEnumerator type(){
        AudioSource[] clacks = GetComponents<AudioSource>();
        while(pipeline.Any()){ //While pipeline isn't empty
            for(int i = 0; i < pipeline[0].Length; i++){ //Plus one because substring i is exclusive not inclusive??
                delay = 0.01f + Random.Range(0.0f, 0.1f); //Random time makes it sound better

                characterCount++;
                currentText += pipeline[0].Substring(i, 1);
                //fullText += pipeline[0].Substring(i, 1);
                if (characterCount != 40){ //Currently 16 characters per line
                    if (!clacks[0].isPlaying && newlineCount > 12){
                        clacks[0].Play(0); //The noises are SO annoying
                    } //Multiple AudioSources are required for doing delays less than 0.2f
                    else if (newlineCount > 14){
                        clacks[3].Play(0);
                    }
                }
                else{
                    if (newlineCount > 13){
                        this.GetComponent<Text>().color = Color.red;
                        clacks[1].Play(0); //Technically a ding, not a clack
                    }
                    characterCount = 0;
                    if (pipeline[0].Length < i + 1 && pipeline[0].Substring(i + 1, 1) == " "){
                        i++; //This removes extra spaces after new lines!
                    }
                    if (newlineCount < 17){
                        currentText += "\n";
                        newlineCount ++;
                    }
                    else{ //Dead!
                        pause = true;
                        pipeline = new List<string>();
                        pipeline.Add("");
                        clacks[2].Play(0);//Also play paper shuffling noise, then restart!
                        StartCoroutine(player.GetComponent<PlayerController>().killPlayer());
                        //STOP ALL INCOMING MESSAGES UNTIL RESTART
                    }
                    if (currentText.Length > 120){
                        currentText = currentText.Substring(41);
                    }
                    delay += 0.1f;
                }
                this.GetComponent<Text>().text = currentText;
                yield return new WaitForSeconds(delay);
            }
            pipeline.RemoveAt(0); //Pop
        }
        isTyping = false;
        Debug.Log(newlineCount);
    }
}
