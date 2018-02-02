using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {
    public Vector3 time;
    public float turnSpeed;
    public float turn;
    bool night;

    private void Update()
    {
        // day night cycle
        turn += turnSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(turn, 0, 0);

        //look if day
        if (turn >= 360)
        {
            turn = 0;
            WorldManager.instance.StartDay();
            night = false;
        }

        //look if night
        if(turn >= 180 && !night)
        {
            night = true;
            WorldManager.instance.EndDay();
        }
    }

}
