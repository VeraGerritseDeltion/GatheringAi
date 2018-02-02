using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : Work {

    public int importance;
    public Work toBuild;
    public GameObject toBuildGO;
    public GameObject worksite;

    int buildingDays;
    public int daysNeeded;
    public int woodNeeded;
    public int stoneNeeded;

    public override void JobDone()
    {
        // continue building
        buildingDays++;
        worksite.SetActive(true);
        if(buildingDays == daysNeeded)
        {
            print("build building");
            WorkManager.instance.RemoveMeFromWork(this);
            toBuild.built = true;
            worksite.SetActive(false);
            toBuildGO.SetActive(true);
        }
    }

    public override int StoneNeeded()
    {
        return stoneNeeded;
    }

    public override int WoodNeeded()
    {
        return woodNeeded;
    }
}
