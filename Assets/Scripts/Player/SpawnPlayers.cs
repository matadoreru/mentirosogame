using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public Transform spawnPoint;

    void Start()
    {
        PhotonNetwork.Instantiate("Players/" + playerPrefab.name, spawnPoint.position, Quaternion.identity);
    }
}