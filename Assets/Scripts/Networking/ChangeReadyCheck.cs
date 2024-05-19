using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeReadyCheck : MonoBehaviour
{
    public GameObject readyCheck;

    public void ChangeCheck()
    {
        readyCheck.SetActive(!readyCheck.activeSelf);
    }
}
