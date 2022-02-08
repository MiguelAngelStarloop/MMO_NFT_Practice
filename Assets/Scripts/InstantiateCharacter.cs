using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class InstantiateCharacter : MonoBehaviour
{
    [SerializeField] GameObject character;
    private float randomPosition;

    private void Start()
    {     
        InstantiatePlayer();
    }


    private void InstantiatePlayer() { 
        
        EnableRandomPosition();
        
        character = GameManager.instance.UseThisCharacater();
        PhotonNetwork.Instantiate(character.name, new Vector3 (randomPosition ,0, -5), Quaternion.identity);
    }
   
    private void EnableRandomPosition()
    {
        randomPosition = Random.Range(-3f, 3f);
    }
   
}
