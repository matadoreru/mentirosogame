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
    [SerializeField] TMP_Text roomCodeText;
    [SerializeField] TMP_Text errorText;
    [SerializeField] Button startButton;

    private string roomCode;

    public void CreateRoom()
    {
        if (username.text.Length > 10 != username.text.Length <= 0)
        {
            return;
        }

        roomCode = GenerateRandomString(5);

        PhotonNetwork.CreateRoom("H");
    }

    public void JoinRoom()
    {
        if (username.text.Length > 10 != username.text.Length <= 0)
        {
            return;
        }

        PhotonNetwork.JoinRoom(inputCode.text.ToUpper());
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
        roomCodeText.text = "Room code: " + "H";
        PlayerLobbyManager.Instance.UpdatePlayers();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered room: " + newPlayer.UserId);
        PlayerLobbyManager.Instance.UpdatePlayers();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player left room: " + otherPlayer.UserId);
        PlayerLobbyManager.Instance.UpdatePlayers();
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient || PhotonNetwork.CurrentRoom.PlayerCount < 1)
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

    public void ExitLobbyRoom()
    {
        if(panelLobby.activeSelf)
        {
            PhotonNetwork.LeaveLobby();
        }
        if(panelRoom.activeSelf)
        {
            PhotonNetwork.LeaveRoom();
        }
    }


    public override void OnLeftLobby()
    {
        PhotonNetwork.Disconnect();
        SceneLoader.LoadScene(SceneNames.MainMenu);
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
        SceneLoader.LoadScene(SceneNames.MainMenu);
    }
}