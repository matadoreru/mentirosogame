using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnDecks : MonoBehaviour
{
    public GameObject decksParent;
    public GameObject cardPrefab;
    public Transform spawnPoint;

    // private string[] suits = { "Hearts", "Spades", "Diamond", "Clubs" };
    private string[] suits = { "Clubs" };

    private string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "A" };

    public void SpawnCards()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject parentDeck = GameObject.Find("Decks");

            if (parentDeck != null)
            {
                foreach (string suit in suits)
                {
                    foreach (string rank in ranks)
                    {
                        string cardName = suit + rank;
                        Vector3 spawnPosition = spawnPoint.position;
                        GameObject cardInstance = PhotonNetwork.Instantiate("Deck/" + suit + "/" + cardName, spawnPosition, Quaternion.identity);
                        cardInstance.transform.SetParent(parentDeck.transform);
                    }
                }
            }
            else
            {
                Debug.LogError("No se encontro el GameObject padre");
            }
        }
    }

}