using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Barrel : MonoBehaviourPunCallbacks, IPunObservable
{

    private void Start()
    {
        photonView.ObservedComponents.Add (this);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
    
}
