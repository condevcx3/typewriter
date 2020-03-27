using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transition : MonoBehaviour
{
    public GameObject newArea;
    public GameObject oldArea;
    private actionTyper typer; 

    public string direction;
    private float delay = 0.001f;
    private float smoothSpeed = 0.2f;
    private float worldScreenHeight;
    private float worldScreenWidth;

    public Sprite gateSpriteCheck;

    private string toRoad = " You enter an area surrounded by trees.";
    private string toMeadow = " You find yourself in a meadow.";
    private string toForest1 = " This is the entrance to the forest.";
    private string toForest2 = " You continue through the forest.";
    private string toForest3 = " There is a stream here.";
    private string toBigAls = " Welcome to Big Al's Fresh Flesh.";
    private string toGateLocked = " There is a large locked gate.";
    private string toGateOpen = " There is a large open gate here.";
    private string toVoid = " A guard stands in your path.";


    void Start(){
        GameObject typerText = GameObject.Find("TyperText");
        typer = typerText.GetComponent<actionTyper>();
        worldScreenHeight = Camera.main.orthographicSize * 2f;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width; //Used to measure transition distance!
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.name == "Player"){
            //If direction up, y=10, down y=-10, left x=-19, right x=19
            Vector3 newAreaPosition = new Vector3(0,0,0);
            Vector3 newPlayerPosition = new Vector3(0,0,0);
            if (direction == "up"){
                newAreaPosition = new Vector3(0,worldScreenHeight,0);
                newPlayerPosition.x = other.gameObject.transform.position.x;
                newPlayerPosition.y = -other.gameObject.transform.position.y + 0.1f ;
            }
            else if (direction == "down"){
                newAreaPosition = new Vector3(0,-worldScreenHeight,0);
                newPlayerPosition.x = other.gameObject.transform.position.x;
                newPlayerPosition.y = -other.gameObject.transform.position.y - 0.1f;
            }
            else if (direction == "left"){ //Right?
                newAreaPosition = new Vector3(-worldScreenWidth,0,0);
                //Debug.Log(Screen.width + " " + Screen.width / 2);
                newPlayerPosition.x = -other.gameObject.transform.position.x - 0.1f;
                newPlayerPosition.y = other.gameObject.transform.position.y;
            }
            else{
                newAreaPosition = new Vector3(worldScreenWidth,0,0);
                newPlayerPosition.x = -other.gameObject.transform.position.x + 0.1f;
                newPlayerPosition.y = other.gameObject.transform.position.y;
            }
            newArea.transform.position = newAreaPosition;
            newArea.SetActive(true);
            Vector3 oldAreaPosition = oldArea.transform.position;
            StartCoroutine(slide(other, newAreaPosition, oldAreaPosition, newPlayerPosition));
            typedMessage();
        }
    }

    IEnumerator slide(Collider2D player, Vector3 newAreaPosition, Vector3 oldAreaPosition, Vector3 newPlayerPosition){
        //Turn off the players collider while this is happening so it doesn't trigger it multiple times
        BoxCollider2D playerCollider = player.gameObject.GetComponent<BoxCollider2D>();
        PlayerController playerController = player.gameObject.GetComponent<PlayerController>();
        playerController.enabled = false;
        playerCollider.enabled = false;
        Vector3 oldAreaNewPosition = -newAreaPosition; //What a mouthful
        //Vector3 newPlayerPosition = -player.gameObject.transform.position;
        while(oldAreaNewPosition != oldArea.transform.position){
            newArea.transform.position = Vector3.MoveTowards(newArea.transform.position, oldAreaPosition, smoothSpeed);
            oldArea.transform.position = Vector3.MoveTowards(oldArea.transform.position, oldAreaNewPosition, smoothSpeed);
            player.gameObject.transform.position = Vector3.MoveTowards(player.gameObject.transform.position, newPlayerPosition, smoothSpeed);
            //Lerp is great but it takes a looooong time at the end to finish the animation

            yield return new WaitForSeconds(delay);
        }
        oldArea.SetActive(false);
        playerCollider.enabled = true;
        playerController.enabled = true;
    }

    void typedMessage(){
        if (newArea.name == "Road"){
            typer.receiveAction(toRoad);
        }
        else if (newArea.name == "Meadow"){
            typer.receiveAction(toMeadow);
        }
        else if (newArea.name == "Forest (1)"){
            typer.receiveAction(toForest1);
        }
        else if (newArea.name == "Forest (2)"){
            typer.receiveAction(toForest2);
        }
        else if (newArea.name == "Forest (3)"){
            typer.receiveAction(toForest3);
        }
        else if (newArea.name == "BigAl'sZone"){
            typer.receiveAction(toBigAls);
        }
        else if (newArea.name == "Void"){
            typer.receiveAction(toVoid);
        }
        else if (newArea.name == "Gate" && newArea.GetComponent<SpriteRenderer>().sprite == gateSpriteCheck){ //This is fine?
            typer.receiveAction(toGateLocked);
        }
        else if (newArea.name == "Gate"){
            typer.receiveAction(toGateOpen);
        }
    }


}
