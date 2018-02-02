using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node{

    public bool walk;
    public Vector3 nodePosition;
    public Node(bool walkable, Vector3 worldPos)
    {
        walk = walkable;
        nodePosition = worldPos;
    }
}
