using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CardVisibility : MonoBehaviour
{
    public GameObject backPrefab;
    private GameObject currentPrefab;

    private int playerIdOwnder = -1;

    [PunRPC]
    public void AssignCardToPlayer(int playerId, string cardName)
    {
        Debug.Log("Assign player a card: " + playerId);
        UpdateCardVisibility(CalculatePath(cardName));
    }

    private void UpdateCardVisibility(string cardPath)
    {
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
        Debug.Log("Card name: " + cardName);
        Debug.Log("Card Index 0: " + cardName[0]);
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
        Debug.Log("Card path: " + returnPath);

        return returnPath;
    }
}
