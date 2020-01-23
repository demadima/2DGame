using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class event_for_button : MonoBehaviour {

    public static bool is_pushed = false;
    public Transform movingPlatform;
    public Transform position2;
    private Vector3 newPosition;
    public float smooth;


    void Start ()
    {
        newPosition = position2.position;

    }

    void FixedUpdate()
    {

      if (is_pushed == true)
        {
           GameObject.Find("Platform_maneger_1").GetComponent<Move_Platform>().enabled = true;
            movingPlatform.position = Vector3.Lerp(movingPlatform.position, newPosition, smooth * Time.deltaTime);
            GameObject.Find("Platform_maneger_2").GetComponent<Move_Platform>().enabled = false;
                       
        }
    }

    void Update () {

	}
}
