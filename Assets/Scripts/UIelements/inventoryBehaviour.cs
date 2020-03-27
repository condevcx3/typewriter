using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; //For using List.Any() to check for an empty list

public class inventoryBehaviour : MonoBehaviour
{

    //private List<string> itemsText = new List<string>();
    private List<GameObject> itemObjects;
    //private int selection;
    public GameObject player;

    public void open(List<string> itemsText){
        //selection = 0;
        itemObjects = new List<GameObject>();
        GameObject itemTemplate = GameObject.Find("item");
        itemObjects.Add(itemTemplate);
        if (itemsText.Count == 0){
            itemTemplate.GetComponent<Text>().text = "Empty";
        }
        else{
            for (int i = 0; i < itemsText.Count; i++){
                if (i == 0){
                    itemTemplate.GetComponent<Text>().text = itemsText[0];
                }
                else{
                    //Debug.Log(itemsText + " " + itemsText.Count);
                    //float newY = itemObjects[i-1].transform.position.y - itemObjects[0].GetComponent<RectTransform>().rect.height;
                    //Debug.Log(newY);
                    Vector3 newSpot = itemObjects[i-1].transform.position;
                    GameObject tempItem = Instantiate(itemTemplate, gameObject.transform);
                    //tempItem.transform.SetParent(gameObject.transform);
                    // Debug.Log(itemObjects[i-1]);
                    //Debug.Log(newSpot.y);
                    newSpot.y = newSpot.y - 1; //Hardcoded because the height is some number but the scale required is off?? Idk man
                    tempItem.transform.position = newSpot;
                    itemObjects.Add(tempItem);
                    tempItem.GetComponent<Text>().text = itemsText[i];
                }
            }
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.I)){
            player.GetComponent<PlayerController>().enabled = true;
            for (int i = 1; i < itemObjects.Count; i++){
                Destroy(itemObjects[i]);
            }
            gameObject.SetActive(false);
        }
    }
}
