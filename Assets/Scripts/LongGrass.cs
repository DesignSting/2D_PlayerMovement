using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongGrass : MonoBehaviour
{
    public GroundType thisGround;
    
    public GroundType ReturnGroundType()
    {
        return thisGround;
    }
}

public enum GroundType
{
    Null,
    LongGrass,
    Water,
    Forest
}
