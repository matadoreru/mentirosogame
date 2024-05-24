using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DeckManager : MonoBehaviourPunCallbacks
{
    public GameObject deckParent;
    public int cardsPerPlayer = 1;
    public List<GameObject> totalDeck = new();

    public void InitializeDeck()
    {
        foreach (Transform card in deckParent.transform)
        {
            totalDeck.Add(card.gameObject);
        }
    }

    public void ShuffleAndDistribute()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        // Shuffle carts
        for (int i = totalDeck.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = totalDeck[i];
            totalDeck[i] = totalDeck[randomIndex];
            totalDeck[randomIndex] = temp;
        }

        // Distribute carts to players
        int playerCount = PhotonNetwork.PlayerList.Length;
        for (int i = 0; i < playerCount; i++)
        {
            for (int j = 0; j < cardsPerPlayer; j++)
            {
                int cardIndex = i * cardsPerPlayer + j;
                if (cardIndex < totalDeck.Count)
                {
                    GameObject card = totalDeck[cardIndex];
                    card.GetComponent<PhotonView>().RPC("AssignCardToPlayer", RpcTarget.All, PhotonNetwork.PlayerList[i].ActorNumber, card.name);
                    Debug.Log("Player" + PhotonNetwork.PlayerList[i].ActorNumber);
                    GameObject player = GameObject.Find("Player" + PhotonNetwork.PlayerList[i].ActorNumber);
                    Debug.Log(player);
                    player.GetComponent<PhotonView>().RPC("AddCard", RpcTarget.All, card.name);
                }
            }
        }

        // Update all the card visibility for all players
        for (int i = 0; i < totalDeck.Count; i++)
        {
            GameObject card = totalDeck[i];
            card.GetComponent<PhotonView>().RPC("UpdateCardVisibility", RpcTarget.All, card.name);
        }
    }
}
