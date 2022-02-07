using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamera : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private Transform playerTransform;
    private bool isSettingTarget;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {  
         FollowPlayer();    
    }

    private void FollowPlayer()
    {
        if (isSettingTarget == true)
        {
            virtualCamera.Follow = playerTransform;
            virtualCamera.LookAt = playerTransform;
        }
    }

    public void FindPlayer(AnimatorController player)
    {
        playerTransform = player.transform;
        isSettingTarget = true;

    }


}
