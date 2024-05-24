using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCards : MonoBehaviourPun
{
    public List<GameObject> cartas;

    [PunRPC]
    void AddCard(string cardName)
    {
        GameObject parentDeck = GameObject.Find("Decks");
        Transform cardTransform = FindCardInDeck(parentDeck.transform, cardName);
        if (cardTransform != null)
        {
            GameObject card = cardTransform.gameObject;
            cartas.Add(card);
            Debug.Log("Card added: " + cardName);
        }
        else
        {
            Debug.LogError("Card not found: " + cardName);
        }
    }

    [PunRPC]
    void RemoveCard(string cardName)
    {
        GameObject parentDeck = GameObject.Find("Decks");
        Transform cardTransform = FindCardInDeck(parentDeck.transform, cardName);
        if (cardTransform != null)
        {
            GameObject card = cardTransform.gameObject;
            cartas.Remove(card);
            Debug.Log("Card remove: " + cardName);
        }
        else
        {
            Debug.LogError("Card not found: " + cardName);
        }
    }

    Transform FindCardInDeck(Transform parentDeck, string cardName)
    {
        foreach (Transform child in parentDeck)
        {
            if (child.name == cardName)
            {
                return child;
            }
        }
        return null;
    }

}
