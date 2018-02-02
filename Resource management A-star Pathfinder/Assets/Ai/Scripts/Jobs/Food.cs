using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Materials
{
    public override void JobDone()
    {
        // reward
        WorldManager.instance.food += reward;
    }
}
