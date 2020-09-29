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
    public GameObject TitleText;
    public GameObject playText;
    public GameObject quitText;
    public GameObject introText;
    private int selection = 0;
    private IEnumerator coroutine;
    private bool messageDisplayed;
    private string fullText;

    //
    private List<string> introList = new List<string>(){"They say that when an object has been around for hundreds of years, it can gain sentience... or some form that resembles sentience.", 
    "Objects that gain strong ‘wills’ this way can gain characteristics resembling their purpose, and what their desire is when they gain sentience.",
    "The last thing you remember was going through your late grandfather’s attic, sorting through his stored items in preparation of selling his estate. In the back of the attic, you came across a typewriter, with a single half-finished paper sticking out of it. When you went to move the typewriter, you blacked out, and you found yourself here.",
    "“Who are you?” You might be asking? Think of me as the narrator for this unfinished story, and think of yourself as the main character who needs to finish it.",
    "Your actions will correspond to actions that will be written on the page, but keep in mind that only 16 lines can fit on a single page, and whenever you hit that, you will be reset back to the place you started. You will lose most progress that you have made, but you get to keep all of the items that you have found up to that point.",
    "Good luck, protagonist, on your story."};

    //Player
    public GameObject player;

    private bool selectionMade;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(showTitle());
        playText.GetComponent<Text>().font = selected;
        quitText.GetComponent<Text>().font = unselected;
        selectionMade = false;
    }

    IEnumerator showTitle(){
        TitleText.SetActive(true);
        string fullText = TitleText.GetComponent<Text>().text;
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
            TitleText.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        
    }

    IEnumerator showIntroduction() {
        messageDisplayed = false;
        introText.SetActive(true);
        string currentText = "";
        AudioSource[] clacks = GetComponents<AudioSource>();
        float delay = 0.050f;
        fullText = introList[0];
        Debug.Log(fullText);

        for (int i = 0; i < introList[0].Length; i++) {
            currentText += introList[0].Substring(i, 1);
            if (!clacks[0].isPlaying) {
                clacks[0].Play(0);
            }
            if (i == introList[0].Length-1) {
                clacks[1].Play(0);
            }
            introText.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        messageDisplayed = true;
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

        if (messageDisplayed && Input.GetKeyDown(KeyCode.Q) && selection == 2) {
            if (introList.Count > 1) {
                introList.RemoveAt(0);
                coroutine = showIntroduction();
                StartCoroutine(coroutine);
                Debug.Log("New coroutine");
            }
            else {
                player.SetActive(false);
                //yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene("Game");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q) && selection == 2) {
            if (!selectionMade){
                selectionMade = true; //Prevents an error
            }
            else{
                StopCoroutine(coroutine);
                introText.GetComponent<Text>().text = introList[0];
                messageDisplayed = true;
            }
        }
    }

    IEnumerator startGame(){
        AudioSource[] clacks = GetComponents<AudioSource>();
        clacks[2].Play(0);
        yield return new WaitForSeconds(0.8f);
        playText.SetActive(false);
        quitText.SetActive(false);
        TitleText.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(0.3f);
        coroutine = showIntroduction();
        StartCoroutine(coroutine);
        /* player.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Game"); */
    }

}
