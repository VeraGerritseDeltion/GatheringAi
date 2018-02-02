using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class Human : MonoBehaviour {

    public NavMeshAgent agent;
    public Work myJobToday;
    public GameObject child;

    [Header("Needs")]
    public int foodNeeded;
    public int daysWithoutFood;

    [Header("Skills")]
    public int lumberLvl;
    public int builderLvl;
    public int gatheringLvl;
    public int farmLvl;

    
    public enum LifeStage { childOrElder, adult }
    [Header("Life Stage")]
    public LifeStage lifeStage;
    public int age;
    int adultAge = 18;
    int elderAge = 60;
    Vector2 deathAge = new Vector2(62, 80);
    float death;
    float deathSpeed;
    public Color color = Color.white;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        death = Mathf.RoundToInt(Random.Range(deathAge.x, deathAge.y));
        deathSpeed = 1 / death;
        StartCoroutine(StartShit());
    }
    IEnumerator StartShit()
    {
        yield return new WaitForSeconds(0.1f);
        WorldManager.instance.allHumans.Add(this);
        ColorAge(deathSpeed * age);
    }

    public void AgeUp()
    {
        if(myJobToday!= null)
        {
            // get reward
        myJobToday.JobDone();
        myJobToday = null;
        }

        //age up
        age++;
        ColorAge(deathSpeed);
        if (age >= adultAge && age < elderAge)
        {
            lifeStage = LifeStage.adult;
            foodNeeded = 10;
        }
        else
        {
            lifeStage = LifeStage.childOrElder;
            foodNeeded = 5;
        }
        if(age == death)
        {
            Death();
        }
        if(age == 30 || age == 50)
        {
            // give birth
            Instantiate(child, WorldManager.instance.home.position, Quaternion.identity);
        }
    }

    void Death()
    {
        // die
        WorldManager.instance.IDied(this);
        Destroy(gameObject);
    }

    void ColorAge(float dS)
    {
        // change color based on age
        color.r -= dS;
        color.g -= dS;
        color.b -= dS;
        GetComponent<Renderer>().material.color = color;
    }

    public void GetWork(Work myJob)
    {
        //assign job
        myJobToday = myJob;
        agent.destination = myJob.location.position;
    }
}
