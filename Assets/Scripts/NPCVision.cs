using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVision : MonoBehaviour
{

    public void SendToNPC(Transform t)
    {
        //GetComponentInParent<NPC>().MoveToPlayer(t);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("How about this?");
    }

}
