using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class BarrelController : MonoBehaviourPun
{
    public static BarrelController instance;

    private int barrelsAmmmount = 0;
    private Vector3 barrelPosition;

    [SerializeField] private TextMeshProUGUI barrelCount;
    [SerializeField] private GameObject barrelObject;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       
        if (!PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom != null)
        {
            return;
        }

        StartCoroutine(BarrelCreator());
    }

    public void CallBarrel()
    {
        photonView.RPC("AddBarrelCount", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void AddBarrelCount()
    {
        barrelsAmmmount++;
        barrelCount.text = "" + barrelsAmmmount;
    }

    private void InstantiateBarrel()
    {
        PhotonNetwork.Instantiate(barrelObject.name, barrelPosition, Quaternion.identity);
    }

    IEnumerator BarrelCreator()
    {
        for (int i = 0; i< 10; i++)
        {
            int randomPosition = Random.Range(0, 5);
            i = 0;
            barrelPosition = new Vector3(randomPosition, 0, randomPosition);
            InstantiateBarrel();
            yield return new WaitForSeconds(8f);
        }
    }


 
}
