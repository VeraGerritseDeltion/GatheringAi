using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Work : MonoBehaviour {

    public enum WorkSort {food,wood,ore,build,create};
    public WorkSort workSort;
    public Transform location;
    public bool built;

    private void Awake()
    {
        StartCoroutine(StartAll());
    }
    IEnumerator StartAll()
    {
        // adds itself to all works
        yield return new WaitForSeconds(0.1f);
        WorkManager.instance.allWork.Add(this);
    }
    public virtual int GetReward()
    {
        return 0;
    }

    public virtual bool Requirement()
    {
        if(built == true)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public virtual int WoodNeeded()
    {
        return 0;
    }

    public virtual int StoneNeeded()
    {
        return 0;
    }

    public virtual void JobDone()
    {

    }

    public virtual int AxeNeeded()
    {
        return 0;
    }

    public virtual int PickAxeNeeded()
    {
        return 0;
    }
}
