using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CardVisibility : MonoBehaviourPun
{
    public GameObject backPrefab;
    private GameObject currentPrefab;

    private int playerIdOwnder = -1;

    [PunRPC]
    public void AssignCardToPlayer(int playerId, string cardName)
    {
        playerIdOwnder = playerId;
        photonView.RPC("UpdateCardVisibility", RpcTarget.All, cardName);
    }

    [PunRPC]
    public void UpdateCardVisibility(string cardName)
    {
        Debug.Log("Update Card Visibility: " + cardName);
        string cardPath = CalculatePath(cardName);

        GameObject parentObject = GameObject.Find("Decks");
        Vector3 positionCard = this.gameObject.transform.position;

        Destroy(this.gameObject);  

        if (playerIdOwnder == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            currentPrefab = PhotonNetwork.Instantiate(cardPath, positionCard, Quaternion.identity);
            currentPrefab.transform.SetParent(parentObject.transform);
        }
        else
        {
            currentPrefab = PhotonNetwork.Instantiate("Deck/CardNoVisible", positionCard, Quaternion.identity);
            currentPrefab.transform.SetParent(parentObject.transform);
        }
    }

    private string CalculatePath(string cardName)
    {
        cardName = cardName.Replace("(Clone)", "");
        string returnPath;

        if (cardName[0] == 'H')
        {
            returnPath = "Deck/Hearts/" + cardName;
        }
        else if (cardName[0] == 'S')
        {
            returnPath = "Deck/Spades/" + cardName;
        }
        else if (cardName[0] == 'D')
        {
            returnPath = "Deck/Diamond/" + cardName;
        }
        else
        {
            returnPath = "Deck/Clubs/" + cardName;
        }

        return returnPath;
    }
}
