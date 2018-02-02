using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkManager : MonoBehaviour {

    public static WorkManager instance;

    [Header("Jobs")]
    public List<Work> workAvailable = new List<Work>();
    public List<Work> allWork = new List<Work>();
    List<Work> todayWork = new List<Work>();

    private void Awake()
    {
        //making singelton
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CalculateWork(List<Human> workers,float foodNeeded,int woodToUse,int stoneToUse,int axe,int pickaxe)
    {
        // start new day
        todayWork.Clear();

        //check if build and requirements
        CheckAvailable();

        // sorting available jobs
        List<Work> food = new List<Work>(GetFoodJobs());
        List<Work> build = new List<Work>(GetBuildJobs());
        List<Work> create = new List<Work>(GetCreateJobs());
        List<Work> wood = new List<Work>(GetWoodJobs());
        List<Work> ore = new List<Work>(GetStoneJobs());

        //calculate how many people needed to take care of food to sustane tribe
        int peopleNeededForFood = 0;
        if (food.Count != 0)
        {
        peopleNeededForFood = Mathf.CeilToInt(foodNeeded / food[0].GetReward());
        }

        // add food work
        if (food.Count != 0)
        {
            for (int i = 0; i < peopleNeededForFood; i++)
            {
                todayWork.Add(food[0]);
            }
        }

        // add build work
        if (build.Count != 0)
        {
            for (int i = 0; i < build.Count; i++)
            {
                if (woodToUse >= build[i].WoodNeeded() && stoneToUse >= build[i].StoneNeeded())
                {
                    todayWork.Add(build[i]);
                    woodToUse -= build[i].WoodNeeded();
                    stoneToUse -= build[i].StoneNeeded();
                }
            }
        }
        
        // add create work(tools)
        if (create.Count != 0)
        {
            for (int i = 0; i < create.Count; i++)
            {

            }
        }

        // gives rest workers work
            for (int i = 0; i < workers.Count; i++)
            {
                int o = Random.Range(0, 2);
                if(o == 0)
                {
                    if (wood.Count != 0)
                    {
                        for (int p = 0; p < wood.Count; p++)
                        {
                            if (axe - wood[p].AxeNeeded() >= 0)
                            {
                                todayWork.Add(wood[i]);
                                break;
                            }
                        }
                        
                    }
                }
                else
                {
                    if (ore.Count > 0)
                    {
                        for (int p = 0; p < ore.Count; p++)
                        {
                            if (pickaxe - ore[p].PickAxeNeeded() >= 0)
                            {
                                todayWork.Add(ore[p]);
                                break;
                            }
                        }
                    }

                }
            }
        
        // assigning work
        for (int i = 0; i < workers.Count; i++)
        {
            if(todayWork.Count != 0)
            {
                workers[i].GetWork(todayWork[0]);
                todayWork.RemoveAt(0);
            }
        }
    }

    public List<Work> GetFoodJobs()
    {
        if (workAvailable.Count != 0)
        {   // get all jobs that has to do with food
            List<Materials> food = new List<Materials>();
            for (int i = 0; i < workAvailable.Count; i++)
            {
                if (workAvailable[i].workSort == Work.WorkSort.food)
                {
                    food.Add(workAvailable[i] as Materials);
                }
            }
            List<Work> sortedWork = new List<Work>();

            // sort on reward
            List<Materials> sortedFood = new List<Materials>(OrderMaterial(food));
            for (int i = 0; i < sortedFood.Count; i++)
            {
                sortedWork.Add(sortedFood[i]);
            }
            return sortedWork;
        }
        return new List<Work>();
    }

    public List<Work> GetWoodJobs()
    {
        // get all jobs that has to do with wood
        if (workAvailable.Count != 0)
        {
            List<Materials> wood = new List<Materials>();
            for (int i = 0; i < workAvailable.Count; i++)
            {
                if (workAvailable[i].workSort == Work.WorkSort.wood)
                {
                    wood.Add(workAvailable[i] as Materials);
                }
            }

            List<Work> sortedWork = new List<Work>();

            // sort on reward
            List<Materials> sortedFood = new List<Materials>(OrderMaterial(wood));
            for (int i = 0; i < sortedFood.Count; i++)
            {
                sortedWork.Add(sortedFood[i]);
            }
            return sortedWork;
        }
        return new List<Work>();
    }

    public List<Work> GetStoneJobs()
    {
        // get all jobs that has to do with stone
        if (workAvailable.Count != 0)
        {
            List<Materials> stone = new List<Materials>();
            for (int i = 0; i < workAvailable.Count; i++)
            {
                if (workAvailable[i].workSort == Work.WorkSort.ore)
                {
                    stone.Add(workAvailable[i] as Materials);
                }
            }
            List<Work> sortedWork = new List<Work>();

            //sort on reward
            List<Materials> sortedFood = new List<Materials>(OrderMaterial(stone));
            for (int i = 0; i < sortedFood.Count; i++)
            {
                sortedWork.Add(sortedFood[i]);
            }
            return sortedWork;
        }
        return new List<Work>();
    }

    public List<Work> GetBuildJobs()
    {
        // get all jobs that has to do with building
        if (workAvailable.Count != 0)
        {
            List<Build> build = new List<Build>();
            for (int i = 0; i < workAvailable.Count; i++)
            {
                if (workAvailable[i].workSort == Work.WorkSort.build)
                {
                    build.Add(workAvailable[i] as Build);
                }
            }

            List<Work> sortedWork = new List<Work>();

            //sort on priority
            List<Build> sortedFood = new List<Build>(OrderWork(build));
            for (int i = 0; i < sortedFood.Count; i++)
            {
                sortedWork.Add(sortedFood[i]);
            }
            return sortedWork;
        }
        return new List<Work>();
    }
    public List<Work> GetCreateJobs()
    {
        // get all jobs that has to do with creating
        if (workAvailable.Count != 0)
        {
            List<Create> build = new List<Create>();
            for (int i = 0; i < workAvailable.Count; i++)
            {
                if (workAvailable[i].workSort == Work.WorkSort.build)
                {
                    build.Add(workAvailable[i] as Create);
                }
            }

            List<Work> sortedWork = new List<Work>();
            List<Create> sortedFood = new List<Create>(build);
            for (int i = 0; i < sortedFood.Count; i++)
            {
                sortedWork.Add(sortedFood[i]);
            }
            return sortedWork;
        }
        return new List<Work>();
    }


    public List<Materials> OrderMaterial (List<Materials> toOrder)
    {
        // sorting on reward
        List<Materials> inOrder = new List<Materials>();
        List<Materials> notInOrder = toOrder;
        if(toOrder != null)
        {
            while (notInOrder.Count != 0)
            {
                int highest = 0;
                int highestInt = 0;
                for (int o = 0; o < notInOrder.Count; o++)
                {
                    if (notInOrder[o].reward > highestInt)
                    {
                        highest = o;
                        highestInt = notInOrder[o].reward;
                    }
                }
                inOrder.Add(notInOrder[highest]);
                notInOrder.RemoveAt(highest);           
            }
        }
        return inOrder;
    }

    public List<Build> OrderWork (List<Build> toOrder)
    {
        // sorting on priority
        List<Build> inOrder = new List<Build>();
        List<Build> notInOrder = toOrder;
        if (toOrder != null)
        {
            while (notInOrder.Count != 0)
            {
                int highest = 0;
                int highestInt = 0;
                for (int o = 0; o < notInOrder.Count; o++)
                {

                    if (notInOrder[o].importance > highestInt)
                    {
                        highest = o;
                        highestInt = notInOrder[o].importance;
                    }
                }
                inOrder.Add(notInOrder[highest]);
                notInOrder.RemoveAt(highest);
            }
        }
        return inOrder;
    }

    public void CheckAvailable()
    {
        // check if jobs available
        workAvailable.Clear();
        for (int i = 0; i < allWork.Count; i++)
        {
            if (allWork[i].Requirement())
            {
                workAvailable.Add(allWork[i]);
            }
        }
    }

    public void RemoveMeFromWork(Work workdone)
    {
        // removes job when never to be done again
        if(workdone != null)
        {
            for (int i = 0; i < allWork.Count; i++)
            {
                if(allWork[i] == workdone)
                {
                    allWork.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
