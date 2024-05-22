using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayerSeats : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Transform> seatsLocations = new List<Transform>();
    [SerializeField] GameObject seatPlayerPrefab;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int index = 0;
            foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
            {
                PhotonNetwork.Instantiate(seatPlayerPrefab.name, seatsLocations[index].position, Quaternion.identity);
            }
        }
    }
}
