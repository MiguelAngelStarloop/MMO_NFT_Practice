using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class ChatManager : MonoBehaviourPunCallbacks, IPunObservable
{

    public TextMeshProUGUI updatedText;
    public float scrollmovement = -3f;

    private InputField chatInputField;
    private ScrollRect scrollRect;


    private void Awake()
    {
        chatInputField = GameObject.Find("InputField").GetComponent<InputField>();
        scrollRect = GameObject.Find("Chat_window").GetComponent<ScrollRect>();
        updatedText = GameObject.Find("Content_chat").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (chatInputField.isFocused)
            {
                if (chatInputField.text != "" && chatInputField.text.Length > 0 && Input.GetKeyDown(KeyCode.F1))
                {
                    photonView.RPC("SendMessage", RpcTarget.AllBuffered, chatInputField.text);
                    chatInputField.text = "";
                }
            }
        }
    }

    [PunRPC]
    private void SendMessage (string message)
    {
        updatedText.text += "\n["+ photonView.Owner.NickName + "]  " + message;
        scrollRect.verticalNormalizedPosition = scrollmovement;
    }


    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
      
    }
}
