using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamController : MonoBehaviour {

    Camera camera;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public Transform Background;

    public GameObject Pos_contr;
    private Pos_Controller Pos;

    public Slider healthBar;
    public Slider healthBar_1;

    public Transform bk2;

    int zoom = 10;
    int normal = 7;

    void Start () {

        camera = GetComponent<Camera>();
        Pos = Pos_contr.GetComponent<Pos_Controller>();
    }
	
	void Update () {
        healthBar.value = CharacterControllerScript.Char_HP;
        healthBar_1.value = Character_Controler_2.Char_HP;
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(new Vector3(target.position.x, target.position.y + 0.75f, target.position.z));
            Vector3 delta = new Vector3(target.position.x, target.position.y + 0.75f, target.position.z) - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            bk2.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 20);
        }

        if ((Pos.Pos_Character.position.x - Pos.Pos_Character_1.position.x) > 14 || (Pos.Pos_Character.position.x - Pos.Pos_Character_1.position.x) < -14) //Вышел за пределы по X
        {
            GetComponent<Camera>().orthographicSize = zoom;
            Background.transform.localScale = new Vector3(1.2f, 0.93f, 0);
        } else
        {
            GetComponent<Camera>().orthographicSize = normal;
            Background.transform.localScale = new Vector3(0.9f, 0.7f, 0);
            if ((Pos.Pos_Character.position.y - Pos.Pos_Character_1.position.y) > 5 || (Pos.Pos_Character.position.y - Pos.Pos_Character_1.position.y) < -5) //Вышел за пределы по Y
            {
                GetComponent<Camera>().orthographicSize = zoom;
                Background.transform.localScale = new Vector3(1.2f, 0.93f, 0);
            }
            else
            {
                GetComponent<Camera>().orthographicSize = normal;

                Background.transform.localScale = new Vector3(0.9f, 0.7f, 0);
            }
        }
    }
}
