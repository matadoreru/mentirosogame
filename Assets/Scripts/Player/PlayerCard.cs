using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCard : MonoBehaviour
{
    [SerializeField] string username;

    public string Username { get => username; set => username = value; }
}
