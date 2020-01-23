using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Config : MonoBehaviour {




	// Use this for initialization
	void Start () {
        GameObject.Find("Platform_maneger_1").GetComponent<Move_Platform>().enabled = false;
        GameObject.Find("Platform_maneger_2").GetComponent<Move_Platform>().enabled = true;
      }
	
	// Update is called once per frame
	void Update () {
		
	}
}
