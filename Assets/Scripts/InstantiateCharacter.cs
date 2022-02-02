using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InstantiateCharacter : MonoBehaviour
{
    [SerializeField] GameObject character;

    private void Start()
    {     
            InstantiatePlayer();   
    }

 
    private void InstantiatePlayer() { 
        
            character = GameManager.instance.UseThisCharacater();
            PhotonNetwork.Instantiate(character.name, new Vector3 (0,0, -5), Quaternion.identity);      
    }

    /*
    private void EnableRandomPosition()
    {
        randomPosition = Random.Range(0f, 1f);
    }
    */
}
