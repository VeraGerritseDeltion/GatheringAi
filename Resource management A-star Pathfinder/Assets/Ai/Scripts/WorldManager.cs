using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {
    [Header("Home")]
    public Transform home;

    [Header("InStorage")]
    public int food;
    public int wood;
    public int stone;
    public int axe;
    public int pickaxe;

    [Header("Humans")]
    public List<Human> adults = new List<Human>();
    public List<Human> tooYoughAndOld = new List<Human>();
    public List<Human> allHumans = new List<Human>();

    public GameObject baby;

    public static WorldManager instance;

    public void Awake()
    {
        // singelton
        if(instance == null)
        {
            instance = this;
        }
    }

    public void StartDay()
    {
        //start day
        adults.Clear();
        tooYoughAndOld.Clear();
        for (int i = 0; i < allHumans.Count; i++)
        {
            allHumans[i].child = baby;
            if(allHumans[i].lifeStage == Human.LifeStage.adult)
            {
                adults.Add(allHumans[i]);
            }
            else
            {
                tooYoughAndOld.Add(allHumans[i]);
            }
        }
        WorkManager.instance.CalculateWork(adults, CalculateFoodNeeded(allHumans),wood,stone,axe,pickaxe);
    }

    public void EndDay()
    {
        //age humans and get rewards
        for (int i = 0; i < allHumans.Count; i++)
        {
            allHumans[i].AgeUp();
            allHumans[i].agent.destination = home.position;
        }
        Feed(allHumans);
    }

    public void Feed(List<Human> allHuman)
    {
        //feed humans
        int foodNeeded = 0;
        for (int i = 0; i < allHuman.Count; i++)
        {
            foodNeeded += allHuman[i].foodNeeded;
        }
        food -= foodNeeded;
    }

    int CalculateFoodNeeded(List<Human> allHuman)
    {
        // calculate food needed
        int maxFood = 0;
        for (int i = 0; i <allHuman.Count; i++)
        {
            maxFood += allHuman[i].foodNeeded;
        }
        maxFood -= food;
        return maxFood;
    }

    public void IDied(Human deadHuman)
    {
        //if villager died
        for (int i = 0; i < allHumans.Count; i++)
        {
            if(allHumans[i] == deadHuman)
            {
                allHumans.RemoveAt(i);
                return;
            }
        }
    }
}
