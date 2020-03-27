using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    //TitleText stuff
    public float delay = 0.1f;
    private string currentText = "";

    //Play and Quit stuff
    public Font selected;
    public Font unselected;
    public GameObject playText;
    public GameObject quitText;
    private int selection = 0;

    //Player
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(showTitle());
        playText.GetComponent<Text>().font = selected;
        quitText.GetComponent<Text>().font = unselected;
        
    }

    IEnumerator showTitle(){
        string fullText = this.GetComponent<Text>().text;
        AudioSource[] clacks = GetComponents<AudioSource>();
        for(int i = 0; i < fullText.Length + 1; i++){ //Plus one because substring i is exclusive not inclusive
            currentText = fullText.Substring(0, i);
            if (i != 0 && i != fullText.Length){
                if (!clacks[0].isPlaying){
                    clacks[0].Play(0);
                } //Multiple AudioSources are required for doing delays less than 0.2f
                /*else{
                    clacks[1].Play(0);
                }*/
            }
            if (i == fullText.Length){
                clacks[1].Play(0); //Technically a ding, not a clack
                playText.SetActive(true);
                quitText.SetActive(true);
            }
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        
    }

    void Update(){
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) && playText.activeSelf && selection != 2){
            if (selection == 0){
                selection = 1;
                playText.GetComponent<Text>().font = unselected;
                quitText.GetComponent<Text>().font = selected;
            }
            else{
                selection = 0;
                playText.GetComponent<Text>().font = selected;
                quitText.GetComponent<Text>().font = unselected;
            }
        }

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Q)) && playText.activeSelf && selection == 0){
            selection = 2;
            StartCoroutine(startGame());
        }
        else if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Q)) && playText.activeSelf && selection == 1){
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    IEnumerator startGame(){
        AudioSource[] clacks = GetComponents<AudioSource>();
        clacks[2].Play(0);
        yield return new WaitForSeconds(0.8f);
        playText.SetActive(false);
        quitText.SetActive(false);
        this.GetComponent<Text>().text = "";
        player.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Game");



    }

}
