using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColliderColour : MonoBehaviour
{
    public Color colliderColour;
    private Collider2D thisCollider;

    private void Start()
    {
        thisCollider = GetComponent<Collider2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = colliderColour;
        float xSize = thisCollider.bounds.size.x;
        float ySize = thisCollider.bounds.size.y;
        Gizmos.DrawCube(thisCollider.bounds.center, new Vector3(xSize, ySize, 1));
    }
}
