﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    [SerializeField] private int currentCount;
    [SerializeField] private bool npcPresent;
    [SerializeField] private bool horizonalLock;
    [SerializeField] private bool verticalLock;
    [SerializeField] private GroundType gt;


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

        if(collision.tag == "Teleport")
        {
            collision.GetComponent<Teleport>().TeleportPlayer();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "CameraLock")
        {
            switch (collision.GetComponent<CameraMovement>().cameraLockType)
            {
                case CameraLocked.HorizonalLocked:
                    horizonalLock = true;
                    break;
                case CameraLocked.VerticalLocked:
                    verticalLock = true;
                    break;
                case CameraLocked.BothLocked:
                    horizonalLock = true;
                    verticalLock = true;
                    break;
            }
        }

        if(collision.tag == "Ground")
        {
            gt = collision.GetComponent<LongGrass>().ReturnGroundType();
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

        if (collision.tag == "CameraLock")
        {
            switch (collision.GetComponent<CameraMovement>().cameraLockType)
            {
                case CameraLocked.HorizonalLocked:
                    horizonalLock = false;
                    break;
                case CameraLocked.VerticalLocked:
                    verticalLock = false;
                    break;
                case CameraLocked.BothLocked:
                    horizonalLock = false;
                    verticalLock = false;
                    break;
            }
        }

        if(collision.tag == "Ground")
        {
            gt = GroundType.Null;
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

    public bool ReturnHorizonalLock()
    {
        if (horizonalLock)
            return true;
        return false;
    }
    public bool ReturnVerticalLock()
    {
        if (verticalLock)
            return true;
        return false;
    }
    public bool ReturnBothLock()
    {
        if (horizonalLock && verticalLock)
            return true;
        return false;
    }

    public GroundType ReturnGroundType()
    {
        return gt;
    }
}

