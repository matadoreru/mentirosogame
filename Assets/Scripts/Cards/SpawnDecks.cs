using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnDecks : MonoBehaviour
{
    public GameObject decksParent;
    public GameObject cardPrefab;
    public Transform spawnPoint;

    private string[] suits = { "Hearts", "Spades", "Diamond", "Clubs" };
    private string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject parentObject = GameObject.Find("Decks");

            if (parentObject != null)
            {
                foreach (string suit in suits)
                {
                    foreach (string rank in ranks)
                    {
                        string cardName = suit + rank;
                        GameObject cardInstance = PhotonNetwork.Instantiate("Deck/" + suit + "/" + cardName, spawnPoint.position, Quaternion.identity);
                        cardInstance.transform.SetParent(parentObject.transform);
                    }
                }
            }
            else
            {
                Debug.LogError("No se encontr? el GameObject padre");
            }
        }
    }
}