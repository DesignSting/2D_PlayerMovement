using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;

    private float timer;
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool canRayCast = true;

    public Direction directionFacing;
    private Vector2 direction;
    public int distance;
    public float timeToWalk;
    public GameObject rayPointer;

    public void MoveToPlayer(Transform t)
    {
        StartCoroutine(Move(t));
    }

    public void TurnOffRayCast()
    {
        canRayCast = false;
    }

    private void Update()
    {
        if (canRayCast)
        {
            RaycastHit2D hit = Physics2D.Raycast(rayPointer.transform.position, direction, distance);
            if (hit.collider != null && hit.collider != GetComponent<Collider2D>())
            {
                
                if (hit.collider.tag == "Player")
                {
                    Debug.Log("Hello?");
                    FindObjectOfType<PlayerMovement>().LockPlayer(this);
                    TurnOffRayCast();
                }
               
            }
        }
    }

    private IEnumerator Move(Transform t)
    {
        Debug.Log("Entering Move");
        endPos = t.position;
        timer = 0;
        float stepsBetween = 0;
        switch (directionFacing)
        {
            case Direction.North:
                stepsBetween = Mathf.Abs(t.position.y - transform.position.y);
                break;
            case Direction.East:
                stepsBetween = Mathf.Abs(t.position.x) - Mathf.Abs(transform.position.x);
                break;
            case Direction.South:
                stepsBetween = Mathf.Abs(transform.position.y) - Mathf.Abs(t.position.y);
                break;
            case Direction.West:
                stepsBetween = Mathf.Abs(transform.position.x) - Mathf.Abs(t.position.x);
                break;
        }
        timeToWalk = 1.2f / stepsBetween;

        yield return new WaitForSeconds(1);

        while (timer < 1.0f)
        {
            timer += Time.deltaTime * timeToWalk;
            transform.position = Vector3.Lerp(startPos, endPos, timer);
            yield return null;
        }
        FindObjectOfType<PlayerMovement>().UnlockPlayer();
    }

    private void Start()
    {
        startPos = transform.position;
        switch (directionFacing)
        {
            case Direction.North:
                direction = Vector2.up;
                break;
            case Direction.East:
                direction = Vector2.right;
                break;
            case Direction.South:
                direction = Vector2.down;
                break;
            case Direction.West:
                direction = Vector2.left;
                break;
        }
    }
}
