using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Materials {
    public override void JobDone()
    {
        // reward
        WorldManager.instance.wood += reward;
    }
}
