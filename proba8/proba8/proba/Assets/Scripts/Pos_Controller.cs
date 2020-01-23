using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos_Controller : MonoBehaviour {

    public Transform Pos_Character;
    public Transform Pos_Character_1;
    public Transform Pos_point;
    //private Vector3 velocity = Vector3.zero;

    void Start () {
		
	}
	

	void Update () {

        Pos_point.position = new Vector3 ((Pos_Character.position.x + Pos_Character_1.position.x)/2, (Pos_Character.position.y + Pos_Character_1.position.y)/2, -10);

    }

}
