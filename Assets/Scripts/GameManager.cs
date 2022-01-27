using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;

    [SerializeField] private string characterSelectedName;
    [SerializeField] private GameObject characterPrefab;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SelectCharacterToPplay(string characterName)
    {
        characterSelectedName = characterName;
    }

    
    public GameObject UseThisCharacater()
    {
        characterPrefab = (GameObject)Resources.Load(characterSelectedName);
        return characterPrefab;
    }
    



}

  

