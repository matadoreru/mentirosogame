using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Text;

public class RoomController : MonoBehaviourPunCallbacks
{
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    [SerializeField] GameObject panelLobby;
    [SerializeField] GameObject panelRoom;

    [SerializeField] TMP_InputField inputCode;
    [SerializeField] TMP_InputField username;
    [SerializeField] TMP_Text errorText; // usar para mostrar errores
    [SerializeField] Button startButton;
    [SerializeField] Transform playerCardContainer;
    [SerializeField] GameObject playerCardPrefab;

    private Dictionary<Player, GameObject> playerCards = new Dictionary<Player, GameObject>();

    public void CreateRoom()
    {
        if (username.text.Length > 10 != username.text.Length <= 0)
        {
            return;
        }

        string roomCode = GenerateRandomString(5);

        PhotonNetwork.CreateRoom(roomCode);
    }

    public void JoinRoom()
    {
        if (username.text.Length > 10 != username.text.Length <= 0)
        {
            return;
        }

        PhotonNetwork.JoinRoom(inputCode.text);
    }

    private string GenerateRandomString(int length)
    {
        System.Random random = new();
        StringBuilder randomCode = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            randomCode.Append(chars[random.Next(chars.Length)]);
        }
        Debug.Log(randomCode);
        return randomCode.ToString();
    }

    public override void OnJoinedRoom()
    {
        panelLobby.SetActive(false);
        panelRoom.SetActive(true);
        UpdatePlayers();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered room: " + newPlayer.UserId);
        UpdatePlayers();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player left room: " + otherPlayer.UserId);
        UpdatePlayers();
    }

    private void UpdatePlayers()
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
        }
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient || PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            startButton.interactable = false;
            return;
        }
        startButton.interactable = true;
    }

    public void StartGame()
    {
        SceneLoader.LoadScene(SceneNames.Game);
    }
}