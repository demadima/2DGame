using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_controller : MonoBehaviour {


    void Start () {
        Destroy(this.gameObject, 5f);
    }
	

	void Update () {
        transform.position += Vector3.left * 4 * Time.deltaTime ;
    }
    void OnCollisionEnter()
    {
        Destroy(this.gameObject);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Character") { Destroy(this.gameObject); }
        if (col.gameObject.name == "Character_2") { Destroy(this.gameObject); }
    }
}
