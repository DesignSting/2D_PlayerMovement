using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    [SerializeField] private int currentCount;
    [SerializeField] private bool npcPresent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Environment")
        {
            currentCount++;
        }

        if(collision.tag == "NPC")
        {
            currentCount++;
            npcPresent = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Environment")
        {
            currentCount--;
        }
        if (collision.tag == "NPC")
        {
            currentCount--;
            npcPresent = false;
        }
    }

    public bool ReturnCanMove()
    {
        if (currentCount > 0)
            return false;

        else
            return true;
    }

    public bool ReturnNPCPresent()
    {
        return npcPresent;
    }
}

