using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Barrel : MonoBehaviourPun, IPunObservable
{
    private MeshRenderer mesh;
    private bool meshActive;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        meshActive = true;
    }

    
   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BarrelController.instance.AddBarrelCount();
            DestroyThisBarrel();
        }
        
    }
    

    public void DestroyThisBarrel()
    {
        //Destroy(this.gameObject);
        meshActive = false;
    }

    private void Update()
    {
        mesh.enabled = meshActive;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
        if (stream.IsWriting)
        {
            stream.SendNext(meshActive);

        }
        else
        {
            this.meshActive = (bool)stream.ReceiveNext();
        }
        
    }
}
