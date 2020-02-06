using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teleportTransform;

    public void TeleportPlayer()
    {
        FindObjectOfType<PlayerMovement>().TeleportPlayer(teleportTransform);
    }
}
