using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SetPlayerName : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI playerNameText;

    // Start is called before the first frame update
    void Start()
    {
        string playerName = PlayerStats.PlayerName;
        if (playerName != null)
        {
            playerNameText.text = playerName;
        }
    }
}
