using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEditor;

public class LobbyControl : MonoBehaviourPunCallbacks
{

    [SerializeField] private Button connectButton;
    [SerializeField] private Button randomButon;
    [SerializeField] private TextMeshProUGUI textLog;
    [SerializeField] private TextMeshProUGUI playersCountText;
    [SerializeField] private byte playersMin = 2;
    [SerializeField] private byte playersMax = 4;  

    private int playersCounter;
    private bool levelIsLoad;

    private void Update()
    {
        PlayersAmmount();
    }

    //Try to connect. This method is called with a button
    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.ConnectUsingSettings())
            {
                textLog.text += "\nConnected to server";
            }
            else
            {
                textLog.text += "\nFail to connect to server";
            }
        }
    }

    //If connected, enable an disable buttons
    public override void OnConnectedToMaster()
    {
        connectButton.interactable = false;
        randomButon.interactable = true;
    }

    ///Try to join a random room. If not, create one (With  the method below)
    ///This method is called with a button
    public void RandomRoom()
    {
        if (!PhotonNetwork.JoinRandomRoom())
        {
            textLog.text += "\nFail to join in a room";
        }

    }

    //Create a new room if there aren´t 
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        textLog.text += "\nNo rooms. Creating one...";
        if (PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions() { MaxPlayers = playersMax }))
        {
            textLog.text += "\nRoom created";
        }
        else
        {
            textLog.text += "\nFail to create a room";
        }

    }

    //This method is showed when joined in a room
    public override void OnJoinedRoom()
    {   
        randomButon.interactable = false;
        textLog.text += "\nJoined!";

    }


    //Shows how many players are in a room
    private void PlayersAmmount()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            playersCounter = PhotonNetwork.CurrentRoom.PlayerCount;
            playersCountText.text = "\nPlayers in this room " + playersCounter + "/" + playersMax;
            if (PhotonNetwork.CurrentRoom.PlayerCount >= playersMin && !levelIsLoad)
            {

                LoadGameLevel();

            }
        }
    }

    //Load game level
    private void LoadGameLevel()
    {
        levelIsLoad = true;
        PhotonNetwork.LoadLevel("Game");
    }


}
