using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class DeckManager : MonoBehaviourPunCallbacks
{
    public GameObject deckParent;
    public int cardsPerPlayer = 7;
    public List<GameObject> totalDeck = new();

    private void Start()
    {
        InitializeDeck();
        if (PhotonNetwork.IsMasterClient)
        {
            ShuffleAndDistribute();
        }
    }

    private void InitializeDeck()
    {
        foreach (Transform card in deckParent.transform)
        {
            totalDeck.Add(card.gameObject);
        }
    }

    private void ShuffleAndDistribute()
    {
        for (int i = totalDeck.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = totalDeck[i];
            totalDeck[i] = totalDeck[randomIndex];
            totalDeck[randomIndex] = temp;
        }

        // Distribuir las cartas
        int playerCount = PhotonNetwork.PlayerList.Length;
        for (int i = 0; i < playerCount; i++)
        {
            for (int j = 0; j < cardsPerPlayer; j++)
            {
                int cardIndex = i * cardsPerPlayer + j;
                if (cardIndex < totalDeck.Count)
                {
                    GameObject card = totalDeck[cardIndex];
                    PhotonView photonView = card.GetComponent<PhotonView>();
                    if (photonView != null)
                    {
                        photonView.RPC("AssignCardToPlayer", RpcTarget.All,PhotonNetwork.PlayerList[i].ActorNumber, card.name);
                    }
                    else
                    {
                        Debug.Log("PhotonView NO ENCONTRADO de carta");
                    }
                }
            }
        }
    }
}
