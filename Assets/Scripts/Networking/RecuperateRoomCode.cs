using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class RecuperateRoomCode : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text roomCodeTxt;
    [SerializeField] private string playerRoomCode;

    void Start()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("roomCode", out object roomCode))
        {
            playerRoomCode = (string)roomCode;
            roomCodeTxt.text = "Room code: " + playerRoomCode;
        }
    }
}
