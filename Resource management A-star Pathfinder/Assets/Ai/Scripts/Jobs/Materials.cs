using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : Work {
    [Header("Material Atributes")]
    public int reward;

    public override int GetReward()
    {
        return reward;
    }
}
