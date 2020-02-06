using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Direction currentDirection;
    private Vector2 playerInput;
    private bool isMoving = false;
    [SerializeField] private bool isLocked;
    private Vector3 startPos;
    private Vector3 endPos;
    private float timer;

    public float movementSpeed;

    [Space(15)]
    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite southSprite;
    public Sprite westSprite;

    [Space(15)]
    public PlayerBoundary northBoundary;
    public PlayerBoundary eastBoundary;
    public PlayerBoundary southBoundary;
    public PlayerBoundary westBoundary;

    [Space(15)]
    public CircleCollider2D thisCollider;
    [SerializeField] private NPC currentNPC;
    [SerializeField] private bool npcMoving;
    private SpriteRenderer playerSprite;


    public void LockPlayer(NPC npc)
    {
        isLocked = true;
        currentNPC = npc;
    }

    public void UnlockPlayer()
    {
        isLocked = false;
        currentNPC = null;
    }



    private IEnumerator Move(Transform t)
    {
        isMoving = true;
        startPos = t.position;
        timer = 0;

        endPos = new Vector3(startPos.x + System.Math.Sign(playerInput.x), startPos.y + System.Math.Sign(playerInput.y), startPos.z);

        while(timer < 1f)
        {
            timer += Time.deltaTime * movementSpeed;
            t.position = Vector3.Lerp(startPos, endPos, timer);
            yield return null;
        }

        isMoving = false;
        yield return 0;
    }

    private void Update()
    {
        if(!isMoving && !isLocked)
        {
            playerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if(Mathf.Abs(playerInput.x) > Mathf.Abs(playerInput.y))
            {
                playerInput.y = 0;
            }
            else
            {
                playerInput.x = 0;
            }

            if(playerInput!= Vector2.zero)
            {
                bool canMove = true;
                if(playerInput.x < 0)
                {
                    currentDirection = Direction.West;
                }
                if (playerInput.x > 0)
                {
                    currentDirection = Direction.East;
                }
                if (playerInput.y < 0)
                {
                    currentDirection = Direction.South;
                }
                if (playerInput.y > 0)
                {
                    currentDirection = Direction.North;
                }

                switch (currentDirection)
                {
                    case Direction.North:
                        playerSprite.sprite = northSprite;
                        canMove = northBoundary.ReturnCanMove();
                        break;
                    case Direction.East:
                        playerSprite.sprite = eastSprite;
                        canMove = eastBoundary.ReturnCanMove();
                        break;
                    case Direction.South:
                        playerSprite.sprite = southSprite;
                        canMove = southBoundary.ReturnCanMove();
                        break;
                    case Direction.West:
                        playerSprite.sprite = westSprite;
                        canMove = westBoundary.ReturnCanMove();
                        break;
                }
                if(canMove)
                    StartCoroutine(Move(transform));
            }

            if(Input.GetKeyUp(KeyCode.Space))
            {
                bool isNPC = false;
                switch (currentDirection)
                {
                    case Direction.North:
                        isNPC = northBoundary.ReturnNPCPresent();
                        break;
                    case Direction.East:
                        isNPC = eastBoundary.ReturnNPCPresent();
                        break;
                    case Direction.South:
                        isNPC = southBoundary.ReturnNPCPresent();
                        break;
                    case Direction.West:
                        isNPC = westBoundary.ReturnNPCPresent();
                        break;
                }
                if(isNPC)
                {
                    Debug.Log("Hi, I am an NPC");
                }
            }
        }

        if (isLocked && currentNPC != null & !isMoving && !npcMoving)
        {
            npcMoving = true;
            GameObject temp = new GameObject();
            Transform t = temp.transform;
            switch (currentNPC.directionFacing)
            {
                case Direction.North:
                    t.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                    break;
                case Direction.East:
                    t.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                    break;
                case Direction.South:
                    t.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    break;
                case Direction.West:
                    t.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                    break;
            }
            Debug.Log("Leaving PlayerMovement");
            currentNPC.MoveToPlayer(t);
        }
    }

    private void Start()
    {
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

}

public enum Direction
{
    North,
    East,
    South,
    West
}
