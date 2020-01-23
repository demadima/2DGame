using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {
    public int num;
    public static bool is_character_enable = false;
    public static bool is_character_1_enable = false;
    public static int a;
    public static int b;
    public static int c;
    public static int d;
    public static int e;
    public static int f;

    public static int HP_1;
    public static float Speed_1;
    public static int Vol_Jump_1;

    public static int HP_2;
    public static float Speed_2;
    public static int Vol_Jump_2;

    public static int HP_3;
    public static float Speed_3;
    public static int Vol_Jump_3;

    //Use this for initialization

    private GameObject Play;

    void Start () {
        Play = GameObject.Find("Button");
        Play.active = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (is_character_enable == true && is_character_1_enable == true)
        {
            Play.active = true;
        }
	}
    public void menu(int num)
    {
        if (num == 1)
        {
            a = 1; is_character_enable = true;
            Destroy(GameObject.Find("1st_player_2st_char"));
            Destroy(GameObject.Find("1st_player_3st_char"));
            Destroy(GameObject.Find("2st_player_1st_char"));

            HP_1 = 30; // Стандарт
            Speed_1 = 1f;// Стандарт
            Vol_Jump_1 = 750; // Стандарт 600

        }
        if (num == 2)
        {
            b = 2; is_character_enable = true;
            Destroy(GameObject.Find("1st_player_1st_char"));
            Destroy(GameObject.Find("1st_player_3st_char"));
            Destroy(GameObject.Find("2st_player_2st_char"));

            HP_2 = 45; // Стандарт 30
            Speed_2 = 0.75f;// Стандарт 1f
            Vol_Jump_2 = 600; // Стандарт 600

        }
        if (num == 3)
        {
            c = 3; is_character_enable = true;
            Destroy(GameObject.Find("1st_player_1st_char"));
            Destroy(GameObject.Find("1st_player_2st_char"));
            Destroy(GameObject.Find("2st_player_3st_char"));

            HP_3 = 18; // Стандарт 30
            Speed_3 = 1.5f;// Стандарт 1f
            Vol_Jump_3 = 600; // Стандарт 

        }
        if (num == 4)
        {
            d = 4; is_character_1_enable = true;
            Destroy(GameObject.Find("2st_player_2st_char"));
            Destroy(GameObject.Find("2st_player_3st_char"));
            Destroy(GameObject.Find("1st_player_1st_char"));

            HP_1 = 30; // Стандарт
            Speed_1 = 1f;// Стандарт
            Vol_Jump_1 = 750; // Стандарт 600

        }
        if (num == 5)
        {
            e = 5; is_character_1_enable = true;
            Destroy(GameObject.Find("2st_player_1st_char"));
            Destroy(GameObject.Find("2st_player_3st_char"));
            Destroy(GameObject.Find("1st_player_2st_char"));

            HP_2 = 45; // Стандарт 30
            Speed_2 = 0.75f;// Стандарт 1f
            Vol_Jump_2 = 600; // Стандарт 

        }
        if (num == 6)
        {
            f = 6; is_character_1_enable = true;
            Destroy(GameObject.Find("2st_player_1st_char"));
            Destroy(GameObject.Find("2st_player_2st_char"));
            Destroy(GameObject.Find("1st_player_3st_char"));

            HP_3 = 18; // Стандарт 30
            Speed_3 = 1.5f;// Стандарт 1f
            Vol_Jump_3 = 600; // Стандарт

        }
        if (num == 7)
        {
            Application.LoadLevel("Mymap");        
        }
        if (num == 8)
        {
            Application.LoadLevel(Application.loadedLevel);
            is_character_1_enable = false;
            is_character_enable = false;
            a = 0;
            b = 0;
            c = 0;
            d = 0;
            e = 0;
            f = 0;
        }    
        if (num == 9)
        {
            Application.Quit();
        }   
    }
}
