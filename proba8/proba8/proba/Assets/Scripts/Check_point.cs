using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_point : MonoBehaviour {
    // переменная которая хранит значение ХП во премя прохождения через Chek Point
    public static int res_Char_1;
    public static int res_Char_2;

    // инициализация двух персонажей
    private static GameObject Char_1;
    private static GameObject Char_2;
    private static GameObject Enemy;

    public static GameObject Enemy_res_1;
    public GameObject Enemy_res_1_1;

    // переменные которые хранят координаты Chek Point
    public static Transform pos_1;
    public static Transform pos_2;
    public static Transform pos_3;

    // переменные которые инициализируют координаты Chek Point
    public  Transform pos_1_1;
    public  Transform pos_2_2;
    public Transform pos_3_3;

    // переменные отвечающие за проверку пересечения двумя перснажами коллайдера Chek Point
    public static bool is_1_char_check = false;
    public static bool is_2_char_check = false;

    void Start ()
    {
        Enemy_res_1 = Enemy_res_1_1;
        // инициализация положения Chek Point
        pos_1 = pos_1_1;
        pos_2 = pos_2_2;
        pos_3 = pos_3_3;
        // инициализация двух персонажей
        Char_1 = GameObject.Find("Character");
        Char_2 = GameObject.Find("Character_2");
        Enemy = GameObject.Find("Enemy_obj");
    }
	

	void Update ()
    {

	}

    // функция отвечающая за смерть игровых песонажей
    public static void transform()
    {

        // если два персонажа пересекли Check Point
        if (is_1_char_check == true && is_2_char_check == true)
        {
            // устанавливаем ХП которые имели персонажи до смерти
            CharacterControllerScript.Char_HP = res_Char_1;
            Character_Controler_2.Char_HP = res_Char_2;

            // перемещение персонажей на место Check Point
            Char_1.transform.position = new Vector3(pos_1.position.x, pos_1.position.y, pos_1.position.z);
            Char_2.transform.position = new Vector3(pos_2.position.x, pos_2.position.y, pos_2.position.z);
            
                                    //----------------------- ЗОМБИ 1 - начало ----------------------------------//
            if (Enemy_Char.Zombie_1_die == true)
            {
                Enemy_Char.Zombie_1_HP = 2;
                Enemy_Char.Zombie_1_die = false;
                //Instantiate(Enemy_res_1, pos_3.transform.position, pos_3.transform.rotation);
                Enemy_res_1.active = true;
                Enemy_res_1.transform.position = new Vector3(pos_3.position.x, pos_3.position.y, pos_3.position.z);
                Enemy_Char.move = 0;
                Enemy_Char.is_char_1_triggered_left_1 = false;
                Enemy_Char.is_char_2_triggered_left_1 = false;
                Enemy_Char.is_char_1_triggered_right_1 = false;
                Enemy_Char.is_char_2_triggered_right_1 = false;
            } else
            {
                    Enemy_Char.Zombie_1_HP = 2;
                    Enemy_res_1.transform.position = new Vector3(pos_3.position.x, pos_3.position.y, pos_3.position.z);
                    Enemy_Char.move = 0;
                    Enemy_Char.is_char_1_triggered_left_1 = false;
                    Enemy_Char.is_char_2_triggered_left_1 = false;
                    Enemy_Char.is_char_1_triggered_right_1 = false;
                    Enemy_Char.is_char_2_triggered_right_1 = false;
            }
                                       //----------------------- ЗОМБИ 1 - конец ----------------------------------//
        }
        else
        {
            Application.LoadLevel(Application.loadedLevel);
            is_1_char_check = false;
            is_2_char_check = false;
                                        //----------------------- ЗОМБИ 1 - начало ----------------------------------//
       if (Enemy_Char.Zombie_1_die == true)
            {
                Enemy_Char.Zombie_1_HP = 2;
                Enemy_Char.Zombie_1_die = false;
                //Instantiate(Enemy_res_1, pos_3.transform.position, pos_3.transform.rotation);
                Enemy_res_1.active = true;
                Enemy.transform.position = new Vector3(pos_3.position.x, pos_3.position.y, pos_3.position.z);
                Enemy_Char.move = 0;
                Enemy_Char.is_char_1_triggered_left_1 = false;
                Enemy_Char.is_char_2_triggered_left_1 = false;
                Enemy_Char.is_char_1_triggered_right_1 = false;
                Enemy_Char.is_char_2_triggered_right_1 = false;
            } else
            {
                Enemy_Char.Zombie_1_HP = 2;
                Enemy.transform.position = new Vector3(pos_3.position.x, pos_3.position.y, pos_3.position.z);
                Enemy_Char.move = 0;
                Enemy_Char.is_char_1_triggered_left_1 = false;
                Enemy_Char.is_char_2_triggered_left_1 = false;
                Enemy_Char.is_char_1_triggered_right_1 = false;
                Enemy_Char.is_char_2_triggered_right_1 = false;
            }
                                         //----------------------- ЗОМБИ 1 - конец ----------------------------------//
        }
    }
}
