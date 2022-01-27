using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimatorController : MonoBehaviourPunCallbacks, IPunObservable
{
    private Animator animator;
    [SerializeField] private float directionDampTime = 0.25f;
    public float health = 1;
    private GameObject hitBox;
    private GameObject playerUi;
    private bool isFiring;

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

    }


    private void Movement()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
    
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (v < 0)
        {
            v = 0;
        }
        animator.SetFloat("Speed", h * h + v * v);
        animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);

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

        if (!other.name.Contains("HitBox"))
        {
            return;
        }

        health -= 0.1f;
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
