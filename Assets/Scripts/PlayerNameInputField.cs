using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNameInputField : MonoBehaviour
{
    private string playerNamePrefKey = "PlayerName";
    private TMP_InputField _inputField;

    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
    }

    private void Start()
    {
        string defaultName = string.Empty;
        if (PlayerPrefs.HasKey(playerNamePrefKey))
        {
            defaultName = PlayerPrefs.GetString(playerNamePrefKey);
            _inputField.text = defaultName;
        }

        PhotonNetwork.NickName = defaultName;
    }
    
    public void SetPlayerName()
    {
        string value = _inputField.text;
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
}
