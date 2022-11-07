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
    private bool isUsedPowerUp;

    [SerializeField] private TextMeshProUGUI barrelCount;
    [SerializeField] private TextMeshProUGUI barrelsPanelCount;
    [SerializeField] private GameObject barrelObject;
    [SerializeField] private GameObject powerUPText;

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

    public void CallBarrel(int barrelID)
    {
        photonView.RPC("AddBarrelCount", RpcTarget.AllBuffered);
        photonView.RPC("DestroyBarrel", RpcTarget.MasterClient, barrelID);
    }

    [PunRPC]
    public void AddBarrelCount()
    {
        barrelsAmmmount++;
        barrelCount.text = "" + barrelsAmmmount;
        barrelsPanelCount.text = "" + barrelsAmmmount;
       
    }
    [PunRPC]
    private void DestroyBarrel (int thisBarrel)
    {
        PhotonNetwork.Destroy(PhotonView.Find(thisBarrel).gameObject);

    }

    private void InstantiateBarrel()
    {
        PhotonNetwork.Instantiate(barrelObject.name, barrelPosition, Quaternion.identity);
    }

    IEnumerator BarrelCreator()
    {
        for (int i = 0; i< 10; i++)
        {
            int randomPosition = Random.Range(-10, -15);
            i = 0;
            barrelPosition = new Vector3(randomPosition, 0, randomPosition);
            //InstantiateBarrel(); //Funciona regular porque hay paredes. Hay que hacerlo mejor, como instanciarlo en un array de posiciones fijas.
            yield return new WaitForSeconds(8f);
        }
    }

    #region BARRELS PANEL

    public void UseBarrelButton()
    {
        photonView.RPC("UseBarrel", RpcTarget.AllBuffered);
        isUsedPowerUp = true;
    }

    [PunRPC]
    private void UseBarrel()
    {
        barrelsAmmmount--;
        barrelCount.text = "" + barrelsAmmmount;
        barrelsPanelCount.text = "" + barrelsAmmmount;
    }

    public void ExitButton()
    {
        if (isUsedPowerUp == true)
        {
            powerUPText.SetActive(true);
            isUsedPowerUp = false;
            Invoke(nameof(DisablePowerUpText), 1f);
        }
    }

    private void DisablePowerUpText()
    {
        powerUPText.SetActive(false);
    }

    #endregion

}
