using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Teleport teleport;
    [SerializeField] private GameObject player;

    public void TeleportPlayer()
    {
        player.GetComponent<PlayerMovement>().TeleportPlayer(teleport.transform);

    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }


}

