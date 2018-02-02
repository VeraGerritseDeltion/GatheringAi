using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : Materials {

    public override void JobDone()
    {
        // reward ore
        WorldManager.instance.stone += reward;
    }
}
