using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class AnimatorController : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private float directionDampTime = 0.25f;
    [SerializeField] Transform targetCameraFollow;


    private Animator animator;
    public float health = 1;
    private GameObject hitBox;
    private GameObject playerUi;
    private GameObject cinemachine;
    private bool isFiring;
    

    public float horizontal;
    public float vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.applyRootMotion = true;
    }

    private void Start()
    {
        InvokeElementsFronResources();
    }

    private void Update()
    {
        Movement();
        if (photonView.IsMine)
        {
            ProcessInputs();
        }
        if (hitBox != null && isFiring != hitBox.activeInHierarchy)
        {
            hitBox.SetActive(isFiring);
        }
    }

    private void InvokeElementsFronResources()
    {
        playerUi = Instantiate ((GameObject)Resources.Load ("BaseUI/Player_UI_1"));
        playerUi.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
       
        hitBox = Instantiate((GameObject)Resources.Load("BaseHit/HitBox"));
        hitBox.transform.parent = this.gameObject.transform;
        hitBox.name = ("HitBox");

        if (photonView.IsMine)
        {
            cinemachine = Instantiate((GameObject)Resources.Load("BaseUI/CM_Ccam1"));
            cinemachine.SendMessage("FindPlayer", this, SendMessageOptions.RequireReceiver);
        }
    }


    private void Movement()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
    
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (vertical < 0)
        {
            vertical = 0;
        }
        animator.SetFloat("Speed", horizontal * horizontal + vertical * vertical);
        animator.SetFloat("Direction", horizontal, directionDampTime, Time.deltaTime);

    }


    void ProcessInputs()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isFiring)
            {
                isFiring = true;
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (isFiring)
            {
                isFiring = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (other.gameObject.CompareTag("barrel")) {

            var barrelID = other.GetComponent<PhotonView>().ViewID;
            BarrelController.instance.CallBarrel(barrelID);
        }

        if (!other.name.Contains("HitBox"))
        {
            return;
        }


        else
        {
            health -= 0.1f;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isFiring);
            stream.SendNext(health);

        }
        else
        {
            this.isFiring = (bool)stream.ReceiveNext();
            this.health = (float)stream.ReceiveNext();
        }
    }

}
