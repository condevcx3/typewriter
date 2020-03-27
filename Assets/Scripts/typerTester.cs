using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class typerTester : MonoBehaviour
{
    public GameObject typerObject;
    private actionTyper typer;

    void Start(){
        typer = typerObject.GetComponent<actionTyper>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.I)){
            typer.receiveAction("TestMessage");
        }
    }
}
