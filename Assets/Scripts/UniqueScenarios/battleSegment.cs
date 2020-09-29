using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleSegment : MonoBehaviour
{
    public GameObject attack;
    public GameObject magic;
    public GameObject items;
    public GameObject run;
    private int selection;
    public GameObject player;
    public GameObject opponent;
    private bool pause;
    public Font currentSelection;
    public Font notSelected;
    public GameObject theTyper;
    private actionTyper typer;
    private bool magicSubMenu;
    private bool itemSubMenu;
    private bool oldKeyPresent;
    private bool sandwichPresent;
    public GameObject dialogueBox;
    private DialogueBox dialogueReceiver;
    public GameObject punchAnimation;
    public GameObject slashAnimation; //These are for making my life easier
    private IEnumerator enemyAI;
    public GameObject youWin;

    public GameObject songControl;
    public GameObject stat1;
    public GameObject stat2;
    public GameObject stat3;
    public GameObject stat4;

    // Start is called before the first frame update
    /*void Start()
    {
        Debug.Log("Start");
        dialogueReceiver = dialogueBox.GetComponent<DialogueBox>();
        oldKeyPresent = false;
        sandwichPresent = false;
        magicSubMenu = false;
        itemSubMenu = false;
        selection = 0;
        typer = theTyper.GetComponent<actionTyper>();
        enemyAI = opponentAttack();
        //StartCoroutine()
    }*/

    void Awake()
    {
        Debug.Log("Awake");
        songControl.GetComponents<AudioSource>()[1].Play(0);
        dialogueReceiver = dialogueBox.GetComponent<DialogueBox>();
        oldKeyPresent = false;
        sandwichPresent = false;
        magicSubMenu = false;
        itemSubMenu = false;
        selection = 0;
        typer = theTyper.GetComponent<actionTyper>();
        enemyAI = opponentAttack();
        stat1.transform.position = player.transform.position;
        stat2.transform.position = player.transform.position;
        stat3.transform.position = player.transform.position;
        stat4.transform.position = player.transform.position;
    }

    void OnEnable()
    {
        Debug.Log("Enabled!");
        songControl.GetComponents<AudioSource>()[1].Play(0);
        dialogueReceiver = dialogueBox.GetComponent<DialogueBox>();
        oldKeyPresent = false;
        sandwichPresent = false;
        magicSubMenu = false;
        itemSubMenu = false;
        selection = 0;
        typer = theTyper.GetComponent<actionTyper>();
        enemyAI = opponentAttack();
        stat1.transform.position = player.transform.position;
        stat2.transform.position = player.transform.position;
        stat3.transform.position = player.transform.position;
        stat4.transform.position = player.transform.position;
    }

    public void enemyActivate(){
        StartCoroutine(enemyAI);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause){
            if (!magicSubMenu && !itemSubMenu){
                if (selection == 0){
                    attack.GetComponent<Text>().font = currentSelection;
                    magic.GetComponent<Text>().font = notSelected;
                    items.GetComponent<Text>().font = notSelected;
                    run.GetComponent<Text>().font = notSelected;
                }
                else if (selection == 1){
                    attack.GetComponent<Text>().font = notSelected;
                    magic.GetComponent<Text>().font = currentSelection;
                    items.GetComponent<Text>().font = notSelected;
                    run.GetComponent<Text>().font = notSelected;
                }
                else if (selection == 2){
                    attack.GetComponent<Text>().font = notSelected;
                    magic.GetComponent<Text>().font = notSelected;
                    items.GetComponent<Text>().font = currentSelection;
                    run.GetComponent<Text>().font = notSelected;
                }
                else if (selection == 3){
                    attack.GetComponent<Text>().font = notSelected;
                    magic.GetComponent<Text>().font = notSelected;
                    items.GetComponent<Text>().font = notSelected;
                    run.GetComponent<Text>().font = currentSelection;
                }
                if (Input.GetKeyDown(KeyCode.Q)){
                    switch (selection){
                        case 0:
                            Debug.Log("Attack");
                            StartCoroutine(useAttack());
                            break;
                        case 1:
                            Debug.Log("Magic");
                            magicSelected();
                            break;
                        case 2:
                            Debug.Log("Items");
                            itemSelected();
                            break;
                        default: //case 3
                            Debug.Log("Run");
                            useRun();
                            break;
                    }
                }
            }
            else if (magicSubMenu){
                attack.GetComponent<Text>().font = currentSelection;
                if (Input.GetKeyDown(KeyCode.E)){
                    leaveSubMenu();
                }
                else if (Input.GetKeyDown(KeyCode.Q)){
                    //Only spell available will be Thunder
                    //"You cast Thunder", Play a thunder sound, pause for a bit, "You forgot that Thunder is just the noise, Lightning is the bolt."
                    StartCoroutine(castThunder());
                }
            }
            else if (itemSubMenu){
                if (sandwichPresent){ //Don't have to check for Key because it should always be there.
                    if (selection > 1){
                        selection = 0;
                    }
                    else if (selection < 0){
                        selection = 1;
                    }
                }
                else{
                    selection = 0;
                }
                if (selection == 0){
                    attack.GetComponent<Text>().font = currentSelection;
                    magic.GetComponent<Text>().font = notSelected;
                }
                else if (selection == 1){
                    attack.GetComponent<Text>().font = notSelected;
                    magic.GetComponent<Text>().font = currentSelection;
                }
                if (Input.GetKeyDown(KeyCode.E)){
                    leaveSubMenu();
                }
                else if (Input.GetKeyDown(KeyCode.Q)){
                    switch (selection){
                        case 0:
                            StartCoroutine(useOldKey());
                            break;
                        case 1:
                            StartCoroutine(useSandwich());
                            break;
                    }
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

    void magicSelected(){
        magicSubMenu = true;
        attack.GetComponent<Text>().text = "Thunder";
        magic.GetComponent<Text>().text = "";
        items.GetComponent<Text>().text = "";
        run.GetComponent<Text>().text = "";
    }

    void itemSelected(){
        itemSubMenu = true;
        if (player.GetComponent<PlayerController>().getInventory().Contains("Old Key")){
            attack.GetComponent<Text>().text = "Old Key"; //This should always be true
            magic.GetComponent<Text>().text = "";
            oldKeyPresent = true;
        }
        if (player.GetComponent<PlayerController>().getInventory().Contains("Three Little Pigs Sandwich")){
            magic.GetComponent<Text>().text = "TLP Sandwich";
            sandwichPresent = true;
        }
        items.GetComponent<Text>().text = "";
        run.GetComponent<Text>().text = "";
    }

    void leaveSubMenu(){
        magicSubMenu = false;
        itemSubMenu = false;
        attack.GetComponent<Text>().text = "Attack";
        magic.GetComponent<Text>().text = "Magic";
        items.GetComponent<Text>().text = "Items";
        run.GetComponent<Text>().text = "Run";
        selection = 0;
    }

    IEnumerator castThunder(){
        pause = true;
        typer.receiveAction(" You cast Thunder,");
        yield return new WaitForSeconds(2.0f);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3.0f);
        typer.receiveAction(" You forgot that Thunder is just the noise.");
        pause = false;
        leaveSubMenu();
    }

    IEnumerator useOldKey(){
        pause = true;
        typer.receiveAction(" You pull out the Old Key.");
        yield return new WaitForSeconds(3.5f);
        typer.receiveAction(" He thinks it's pretty cool.");
        pause = false;
        leaveSubMenu();
    }

    IEnumerator useSandwich(){
        pause = true;
        yield return new WaitForSeconds(1.0f);
        opponent.SetActive(false);
        songControl.GetComponents<AudioSource>()[1].Stop();
        songControl.GetComponents<AudioSource>()[4].Play(0);
        yield return new WaitForSeconds(3.0f);


        stat1.SetActive(true);
        Vector3 statPosition = player.transform.position;
        statPosition.y += 1.5f;

        while(statPosition != stat1.gameObject.transform.position){
            stat1.gameObject.transform.position = Vector3.MoveTowards(stat1.gameObject.transform.position, statPosition, 0.05f);
            //Lerp is great but it takes a looooong time at the end to finish the animation
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1.5f);
        stat1.SetActive(false);

        stat2.SetActive(true);

        while(statPosition != stat2.gameObject.transform.position){
            stat2.gameObject.transform.position = Vector3.MoveTowards(stat2.gameObject.transform.position, statPosition, 0.05f);
            //Lerp is great but it takes a looooong time at the end to finish the animation
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1.5f);
        stat2.SetActive(false);

        stat3.SetActive(true);

        while(statPosition != stat3.gameObject.transform.position){
            stat3.gameObject.transform.position = Vector3.MoveTowards(stat3.gameObject.transform.position, statPosition, 0.05f);
            //Lerp is great but it takes a looooong time at the end to finish the animation
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1.5f);
        stat3.SetActive(false);

        stat4.SetActive(true);

        while(statPosition != stat4.gameObject.transform.position){
            stat4.gameObject.transform.position = Vector3.MoveTowards(stat4.gameObject.transform.position, statPosition, 0.05f);
            //Lerp is great but it takes a looooong time at the end to finish the animation
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1.75f);
        stat4.SetActive(false);

        player.GetComponent<PlayerController>().enabled = true;

        songControl.GetComponents<AudioSource>()[4].Stop();
        
        typer.receiveAction(" He decides to let you pass!");

        //youWin.SetActive(true);
        gameObject.SetActive(false);
    }

    IEnumerator useAttack(){
        pause = true;
        typer.receiveAction(" You throw a punch.");
        yield return new WaitForSeconds(1.5f);
        punchAnimation.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        punchAnimation.SetActive(false);
        int rng = Random.Range(1,3);
        if (rng == 1){
            typer.receiveAction(" You remember you have no hands, or arms.");
        }
        else{
            typer.receiveAction(" He says \"That tickles!\"");
        }
        pause = false;
    }

    void useRun(){
        opponent.GetComponent<BoxCollider2D>().enabled = false;
        player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z), 1.0f);
        player.GetComponent<PlayerController>().enabled = true;
        opponent.GetComponent<BoxCollider2D>().enabled = true;
        songControl.GetComponents<AudioSource>()[1].Stop();
        gameObject.SetActive(false);
    }

    IEnumerator opponentAttack(){ //Try to find an on awake method or something
        while (!player.GetComponent<PlayerController>().enabled){
            Debug.Log("enemy AI active");
            yield return new WaitForSeconds(10.0f);
            if (!pause && !player.GetComponent<PlayerController>().enabled){
                typer.receiveAction(" The Guard's stomach grumbles. If only he had some food...");
            }
            yield return new WaitForSeconds(12.0f);
            if (!pause && !player.GetComponent<PlayerController>().enabled){
                Debug.Log("enemy AI attack");
                pause = true;
                typer.receiveAction(" The Guard swings his blade!");
                yield return new WaitForSeconds(1.5f);
                slashAnimation.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                slashAnimation.SetActive(false);
                int rng = Random.Range(1,3);
                if (rng == 1){
                    typer.receiveAction(" You say \"Ouch\" because you think you should.");
                }
                else{
                    typer.receiveAction(" You lose 5 hit points! Or something like that.");
                }
                pause = false;
            }
        }
    }


}
