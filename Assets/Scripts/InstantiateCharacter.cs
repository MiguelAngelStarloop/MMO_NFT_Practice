using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class InstantiateCharacter : MonoBehaviour
{
    [SerializeField] GameObject character;
    //[SerializeField] CinemachineVirtualCamera virtualCamera;
    private float randomPosition;

    private void Start()
    {     
        InstantiatePlayer();
       //Invoke(nameof(CameraFollow), 0.5f);
    }

    private void Update()
    {
       
       //CameraFollow();
        
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

    /*
    private void CameraFollow()
    {
        if (character != null)
        {
            Transform characterTransform = character.transform;
            virtualCamera.Follow = characterTransform;
            virtualCamera.LookAt = characterTransform;
            Debug.Log(characterTransform);
        }
    }
    */
    
    
}
