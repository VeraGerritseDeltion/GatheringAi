using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour {


	void Update () {

        // speed up scene
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Time.timeScale = 5;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 10;
        }
    }
}
