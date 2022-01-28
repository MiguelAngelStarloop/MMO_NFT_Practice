using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class BarrelController : MonoBehaviourPun
{
    public static BarrelController instance;

    private int barrelsAmmmount = 0;
    private Vector3 barrelPosition = new Vector3(0, 0, -5);

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
        InstantiateBarrel();
    }

    public void AddBarrelCount()
    {
        if (photonView.IsMine)
        {
            barrelsAmmmount++;
            Debug.Log(barrelsAmmmount);
            barrelCount.text = "" + barrelsAmmmount;
        }
    }

    private void InstantiateBarrel()
    {
        PhotonNetwork.Instantiate(barrelObject.name, barrelPosition, Quaternion.identity);
        Debug.Log("Crate Barrel");
    }

 
}
