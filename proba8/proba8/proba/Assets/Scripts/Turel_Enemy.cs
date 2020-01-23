using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turel_Enemy : MonoBehaviour {

    public GameObject fireBall;
    public GameObject fireSpawn;
    public float firePause = 0.5f;

    void Start ()
    {
        StartCoroutine(Fire());
    }

    void Update ()
    {
    }
    IEnumerator Fire()
    {
        Instantiate(fireBall, fireSpawn.transform.position, fireSpawn.transform.rotation);
        yield return new WaitForSeconds(firePause);
        //Debug.Log("After Waiting 2 Seconds");
        StartCoroutine(Fire());
    }
}
