using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : Work {
    public int woodNeeded;
    public int stoneNeeded;
    public override bool Requirement()
    {
        // enough rss to create item?
        bool ugh = base.Requirement();
        bool ugh2 = false;
        if(WorldManager.instance.wood > 20 && WorldManager.instance.stone > 20)
        {
            ugh2 = true;
        }

        if(ugh && ugh2)
        {
            return true;
        }
        return false;
    }

    public override void JobDone()
    {
        // job done
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, 2);
            if(rand == 0)
            {
                WorldManager.instance.axe++;
            }
            else
            {
                WorldManager.instance.pickaxe++;
            }
        }
    }
}
