using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPun
{
    public Camera playerCamera;
    public AudioListener playerAudioListener;

    public void Awake()
    {
        if (photonView.IsMine)
        {
            playerCamera.enabled = true;
            playerAudioListener.enabled = true;
        }
        else
        {
            playerCamera.enabled = false;
            playerAudioListener.enabled = false;
        }
    }
}
