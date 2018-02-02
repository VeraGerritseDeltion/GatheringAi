using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineOre : Ore {
    public override int PickAxeNeeded()
    {
        return 1;
    }

    public override void JobDone()
    {
        // reward and use pickaxe
        WorldManager.instance.stone += reward;
        WorldManager.instance.pickaxe--;
    }
}
