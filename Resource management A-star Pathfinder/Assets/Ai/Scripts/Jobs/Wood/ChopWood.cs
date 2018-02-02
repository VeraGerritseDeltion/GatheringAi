using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopWood : Wood {

    public override int AxeNeeded()
    {
        return 1;
    }

    public override void JobDone()
    {// reward en use axe
        WorldManager.instance.wood += reward;
        WorldManager.instance.axe--;
    }
}
