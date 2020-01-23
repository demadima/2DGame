using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controler_2 : MonoBehaviour
{
    //переменная для установки макс. скорости персонажа
    float maxSpeed = 5f;

    //переменная для определения направления персонажа вправо/влево
    private bool isFacingRight = true;

    //ссылка на компонент анимаций
    private Animator anim;

    //переменная отвечающая за скорость, за ее наличие
    public float move;

    //находится ли персонаж на земле или в прыжке?
    private bool isGrounded = false;

    //ссылка на компонент Transform объекта для определения соприкосновения с землей

    public Transform groundCheck;

    //радиус определения соприкосновения с землей
    private float groundRadius = 0.2f;

    //ссылка на слой, представляющий землю
    public LayerMask whatIsGround;

    //создадим условные переменные, отвечающие за выбор конкретного персонажа(Zero - фиктивный персонаж)
    private GameObject Person_4;
    private GameObject Person_5;
    private GameObject Person_6;
    private GameObject Person_Zero_2;

    // инициализируем переменую, которые определяют первого игрового персонажа
    public GameObject Char_2;
    // инициализируем переменную, которые определяет первую Платформу-лифт
    public GameObject Platform_2;
    public GameObject Platform_3;
    public GameObject Platform_4;
    public GameObject Platform_5;
    public GameObject Platform_6;

    // инициализируем переменные, которая определяют характеристики персонажей 
    public static int Char_HP;
    private bool time_hp;
    public float Char_Speed;
    public int Vol_Jump;


    // переменная отвечающая за время, после которого нанносится урон от большой высоты
    public float timer = 2f;

    // алмаз
    public static bool isTake3 = false;
    private static GameObject alm3;
    public static bool isTake4 = false;
    private static GameObject alm4;

    void Start()
    {
        // при начале игры инициализируем игровых персонажей
        Person_4 = GameObject.Find("Character_2/Person_4");
        Person_5 = GameObject.Find("Character_2/Person_5");
        Person_6 = GameObject.Find("Character_2/Person_6");
        Person_Zero_2 = GameObject.Find("Character_2/Person_Zero_2");

        // инициализируем игрового персонажа
        Char_2 = GameObject.Find("Character_2");


        // инициализируем первую платформу-лифт
        Platform_2 = GameObject.Find("Platform");
        Platform_3 = GameObject.Find("Platform1");
        Platform_4 = GameObject.Find("Platform2");
        Platform_5 = GameObject.Find("Platform3");
        Platform_6 = GameObject.Find("Platform4");

        //алмаз
        alm3 = GameObject.Find("gemRed1");
        alm4 = GameObject.Find("gemRed2");

        // если игровой персонаж был выбран в главном меню
        if (Global.is_character_1_enable == true)
        {
            // если был выбран первый персонаж
            if (Global.d == 4)
            {
                Person_4.active = true;
                Person_5.active = false;
                Person_6.active = false;
                Person_Zero_2.active = false;
                Person_4.GetComponent<Animator>().enabled = true;
                Person_4.GetComponent<SpriteRenderer>().enabled = true;
                anim = GameObject.Find("Character_2/Person_4").GetComponent<Animator>();
                Char_HP = Global.HP_1;
                Char_Speed = Global.Speed_1;
                Vol_Jump = Global.Vol_Jump_1;
            }
            // если был выбран второй персонаж
            if (Global.e == 5)
            {
                Person_5.active = true;
                Person_4.active = false;
                Person_6.active = false;
                Person_Zero_2.active = false;
                Person_5.GetComponent<Animator>().enabled = true;
                Person_5.GetComponent<SpriteRenderer>().enabled = true;
                anim = GameObject.Find("Character_2/Person_5").GetComponent<Animator>();
                Char_HP = Global.HP_2;
                Char_Speed = Global.Speed_2;
                Vol_Jump = Global.Vol_Jump_2;

            }
            // если был выбран третий персонаж
            if (Global.f == 6)
            {
                Person_6.active = true;
                Person_5.active = false;
                Person_4.active = false;
                Person_Zero_2.active = false;
                Person_6.GetComponent<Animator>().enabled = true;
                Person_6.GetComponent<SpriteRenderer>().enabled = true;
                anim = GameObject.Find("Character_2/Person_6").GetComponent<Animator>();
                Char_HP = Global.HP_3;
                Char_Speed = Global.Speed_3;
                Vol_Jump = Global.Vol_Jump_3;
            } 
        }       
    }

    /// Выполняем действия в методе FixedUpdate, т. к. в компоненте Animator персонажа
    /// выставлено значение Animate Physics = true и анимация синхронизируется с расчетами физики
    void FixedUpdate()
    {
        //определяем, на земле ли персонаж
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        //устанавливаем соответствующую переменную в аниматоре
        anim.SetBool("Ground", isGrounded);

        //устанавливаем в аниматоре значение скорости взлета/падения
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        ///если персонаж в прыжке - выход из метода, чтобы не выполнялись действия, связанные с бегом
        ///если персонаж в прыжке - запуск таймера, который начнет отсчет на время timer
        if (!isGrounded)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if (timer < 0)
            {
                //Debug.Log("WORK!");
                time_hp = true;
            }
            return;
        }
        //в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
        //приэтом нам нужен модуль значения
        anim.SetFloat("Speed", Mathf.Abs(move));

        //обращаемся к компоненту персонажа RigidBody2D. задаем ему скорость по оси Х, 
        //равную значению оси Х умноженное на значение макс. скорости
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        //если нажали клавишу для перемещения вправо, а персонаж направлен влево
        if (move > 0 && !isFacingRight)
            //отражаем персонажа вправо
            Flip();
        //обратная ситуация. отражаем персонажа влево
        else if (move < 0 && isFacingRight)
            Flip();

    }

    private void Update()
    {
        // взял алмаз 
        if (isTake3 == true)
        {
            alm3.active = false;
        }
        if (isTake4 == true)
        {
            alm4.active = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            move = 1f * Char_Speed;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            move = 0;
            //Debug.Log("RightArrow key was released.");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            move = -1f * Char_Speed;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            move = 0;
           // Debug.Log("LeftArrow key was released.");
        }

        //если персонаж на земле и нажат пробел...
        if (isGrounded && Input.GetKeyDown(KeyCode.Backspace))
        {
            //устанавливаем в аниматоре переменную в false
            anim.SetBool("Ground", false);

            //прикладываем силу вверх, чтобы персонаж подпрыгнул
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Vol_Jump));
        }

        // если персонаж коснулся земли и был слишком долго, нанести ему урон 
        if (isGrounded)
        {
            if (time_hp == true)
            {
                if (Person_4.active == true)
                {
                    Char_HP -= 9;
                } else
                Char_HP -= 5;
            }
            time_hp = false;
            timer = 2f;
        }
        if (Char_HP == 0 || Char_HP < 0) { /*Application.LoadLevel(Application.loadedLevel);*/ Check_point.transform();}

    }
    /// Метод для смены направления движения персонажа и его зеркального отражения

    private void Flip()
    {
        //меняем направление движения персонажа
        isFacingRight = !isFacingRight;

        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;

        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;

        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "dieCollider")
        {
            //Application.LoadLevel(Application.loadedLevel);
            Check_point.transform();
        }
        if (col.gameObject.name == "ammo(Clone)")
        {
            //Application.LoadLevel(Application.loadedLevel);
            if (Person_5.active == true) { Char_HP -= 3; } else Char_HP -= 5;
        }
        if (col.gameObject.name == "ammo(inv)(Clone)")
        {
            //Application.LoadLevel(Application.loadedLevel);
            if (Person_5.active == true) { Char_HP -= 3; } else Char_HP -= 5;
        }


        //----------------- Двиг платформы
        if (col.gameObject.name == "Is_platform") { Char_2.transform.SetParent(Platform_2.transform, true); }
        if (col.gameObject.name == "Is_platform1") { Char_2.transform.SetParent(Platform_3.transform, true); }
        if (col.gameObject.name == "Is_platform2") { Char_2.transform.SetParent(Platform_4.transform, true); }
        if (col.gameObject.name == "Is_platform3") { Char_2.transform.SetParent(Platform_5.transform, true); }
        if (col.gameObject.name == "Is_platform4") { Char_2.transform.SetParent(Platform_6.transform, true); }



        //-------- кнопка
        if (col.gameObject.name == "event_distance") { event_for_button.is_pushed = true; }

        //----Хил
        if (col.gameObject.name == "potionRed")
        {
            if (Person_4.active == true && Char_HP == 30) { GameObject.Find("potionRed").active = true; }
            else if (Person_4.active == true && Char_HP < 30)
            {
                GameObject.Find("potionRed").active = false;
                if (Char_HP == 29) { Char_HP += 1; }
                if (Char_HP == 28) { Char_HP += 2; }
                if (Char_HP == 27) { Char_HP += 3; }
                if (Char_HP == 26) { Char_HP += 4; }
                if (Char_HP == 25) { Char_HP += 5; }
                if (Char_HP == 24) { Char_HP += 6; }
                if (Char_HP == 23) { Char_HP += 7; }
                if (Char_HP == 22) { Char_HP += 8; }
                if (Char_HP == 21) { Char_HP += 9; }
                if (Char_HP == 20 || Char_HP < 20) { Char_HP += 10; }
            }

            if (Person_5.active == true && Char_HP == 45) { GameObject.Find("potionRed").active = true; }
            else if (Person_5.active == true && Char_HP < 45)
            {
                GameObject.Find("potionRed").active = false;
                if (Char_HP == 44) { Char_HP += 1; }
                if (Char_HP == 43) { Char_HP += 2; }
                if (Char_HP == 42) { Char_HP += 3; }
                if (Char_HP == 41) { Char_HP += 4; }
                if (Char_HP == 40) { Char_HP += 5; }
                if (Char_HP == 39) { Char_HP += 6; }
                if (Char_HP == 38) { Char_HP += 7; }
                if (Char_HP == 37) { Char_HP += 8; }
                if (Char_HP == 36) { Char_HP += 9; }
                if (Char_HP == 35 || Char_HP < 35) { Char_HP += 10; }
            }


            if (Person_6.active == true && Char_HP == 18) { GameObject.Find("potionRed").active = true; }
            else if (Person_6.active == true && Char_HP < 18)
            {
                GameObject.Find("potionRed").active = false;
                if (Char_HP == 17) { Char_HP += 1; }
                if (Char_HP == 16) { Char_HP += 2; }
                if (Char_HP == 15) { Char_HP += 3; }
                if (Char_HP == 14) { Char_HP += 4; }
                if (Char_HP == 13) { Char_HP += 5; }
                if (Char_HP == 12) { Char_HP += 6; }
                if (Char_HP == 11) { Char_HP += 7; }
                if (Char_HP == 10) { Char_HP += 8; }
                if (Char_HP == 9) { Char_HP += 9; }
                if (Char_HP == 8 || Char_HP < 8) { Char_HP += 10; }
            }


        }

        //----- Дверь

        if (col.gameObject.name == "Door")
        {
            if (CharacterControllerScript.isTake1 == true && CharacterControllerScript.isTake2 == true)
            {
                Application.LoadLevel("TheEnd");
            }
            if (isTake3 == true && isTake4 == true)
            {
                Application.LoadLevel("TheEnd");
            }
            if (CharacterControllerScript.isTake1 == true && isTake4 == true)
            {
                Application.LoadLevel("TheEnd");
            }
            if (CharacterControllerScript.isTake2 == true && isTake3 == true)
            {
                Application.LoadLevel("TheEnd");
            }

        }


        //----Алмазы
        if (col.gameObject.name == "gemRed1")
        {
            alm3.active = false;
            isTake3 = true;

        }
        if (col.gameObject.name == "gemRed2")
        {
            alm4.active = false;
            isTake4 = true;

        }

        if (col.gameObject.name == "Check_pos")
        {
            Check_point.is_2_char_check = true;
            //Debug.Log("WORK!");
            Check_point.res_Char_2 = Char_HP;
        }
        //if (col.gameObject.name == "Area_of_triggered") { Enemy_Char.is_char_2_triggered = true; }// !!!!!!!!!
        if (col.gameObject.name == "Area_of_triggered_left")
        {
            Enemy_Char.is_char_2_triggered_left_1 = true;
            Enemy_Char.is_char_2_triggered_right_1 = false;
            //Enemy_Char.is_char_1_triggered_left = false;
            //Enemy_Char.is_char_1_triggered_right = false;
        }
        if (col.gameObject.name == "Area_of_triggered_right")
        {
            Enemy_Char.is_char_2_triggered_right_1 = true;
            Enemy_Char.is_char_2_triggered_left_1 = false;
            //Enemy_Char.is_char_1_triggered_left = false;
            //Enemy_Char.is_char_1_triggered_right = false;
        }
        if (col.gameObject.name == "Kill_zone")
        {
            Enemy_Char.Zombie_1_HP -= 1;
        }

        if (col.gameObject.name == "ukus")
        {
            Char_HP -= 5;
        }



    }
    void OnTriggerExit2D(Collider2D Platform)
    {
        Char_2.transform.parent = null;
        Enemy_Char.is_char_2_triggered_left_1 = false;
        Enemy_Char.is_char_2_triggered_right_1 = false;
        //Debug.Log("WORK!");
    }
    /*public void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.name == "Area_of_triggered")
        {
            Enemy_Char.is_char_2_triggered = true;
            //Enemy_Char.Triggered();
            Debug.Log("WORK!");
        }
    }*/



}


