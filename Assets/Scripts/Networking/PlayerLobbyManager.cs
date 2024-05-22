using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerLobbyManager : MonoBehaviourPunCallbacks
{
    public GameObject playerCardPrefab;
    public TMP_InputField usernameInput;

    public Transform playerCardContainer;
    private Dictionary<Player, GameObject> playerCards = new Dictionary<Player, GameObject>();

    public static PlayerLobbyManager Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePlayers()
    {
        foreach (KeyValuePair<Player, GameObject> playerCard in playerCards)
        {
            Destroy(playerCard.Value);
        }
        playerCards.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            GameObject card = Instantiate(playerCardPrefab, playerCardContainer);
            playerCards.Add(player, card);

            TMP_Text usernameCard = card.GetComponentInChildren<TMP_Text>();
            if (usernameCard != null)
            {
                usernameCard.text = usernameInput.text;
            }
        }
    }
}
