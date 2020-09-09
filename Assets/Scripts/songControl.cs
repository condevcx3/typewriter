using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class songControl : MonoBehaviour
{
    public GameObject forest1;
    public GameObject forest2;
    public GameObject forest3;
    public GameObject bigAls;
    public GameObject battleBox;
    public GameObject road;
    public GameObject meadow;
    public GameObject partyWall;
    public GameObject quietForest1;
    public GameObject quietForest2;
    public GameObject quietForest3;
    public GameObject pathToStart1;
    public GameObject pathToStart2;

    private AudioSource[] songs;

    void Start(){
        songs = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //This is super lazy, should be one function called on transition, not every frame!!
        if ((forest1.activeSelf || forest2.activeSelf || forest3.activeSelf) && !songs[0].isPlaying){
            songs[0].Play(0);
        }
        else if (!forest1.activeSelf && !forest2.activeSelf && !forest3.activeSelf){
            songs[0].Stop();
        }
        if (bigAls.activeSelf && !songs[2].isPlaying){
            songs[2].Play(0);
        }
        else if (!bigAls.activeSelf){
            songs[2].Stop();
        }
        /*if (battleBox.activeSelf && !songs[1].isPlaying){
            songs[1].Play(0);
        }
        else if (!battleBox.activeSelf){
            songs[1].Stop();
        }*/
        if ((road.activeSelf || meadow.activeSelf) && !songs[3].isPlaying){
            songs[3].Play(0);
        }
        else if (!road.activeSelf && !meadow.activeSelf){
            songs[3].Stop();
        }
        if ((quietForest1.activeSelf || quietForest2.activeSelf || quietForest3.activeSelf || partyWall.activeSelf || pathToStart1.activeSelf || pathToStart2.activeSelf) && !songs[6].isPlaying){
            songs[6].Play(0);
        }
        else if (!quietForest1.activeSelf && !quietForest2.activeSelf && !quietForest3.activeSelf && !pathToStart1.activeSelf && !pathToStart2.activeSelf && !partyWall.activeSelf){
            songs[6].Stop();
        }
    }
}
