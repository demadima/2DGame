using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {
    //переменная для установки макс. скорости персонажа
    float  maxSpeed = 5f;

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
    private GameObject Person_1;
    private GameObject Person_2;
    private GameObject Person_3;
    private GameObject Person_Zero;

    // инициализируем переменую, которые определяют первого игрового персонажа
    public GameObject Char;
    // инициализируем переменную, которые определяет первую Платформу-лифт
    public GameObject Platform;
    public GameObject Platform1;
    public GameObject Platform2;
    public GameObject Platform3;
    public GameObject Platform4;

    // инициализируем переменные, которая определяют характеристики персонажей 
    public static int Char_HP;
    private bool time_hp;
    public float Char_Speed;
    public int Vol_Jump;
    
    // переменная отвечающая за время, после которого нанносится урон от большой высоты
    public float timer = 2f;

    // алмаз
    public static bool isTake1 = false;
    private static GameObject alm1;
    public static bool isTake2 = false;
    private static GameObject alm2;


    void Start () {
        // при начале игры инициализируем игровых персонажей
        Person_1 = GameObject.Find("Character/Person_1");
        Person_2 = GameObject.Find("Character/Person_2");
        Person_3 = GameObject.Find("Character/Person_3");
        Person_Zero = GameObject.Find("Character/Person_Zero");

        // инициализируем игрового персонажа
        Char = GameObject.Find("Character");

        // инициализируем первую платформу-лифт
        Platform = GameObject.Find("Platform");
        Platform1 = GameObject.Find("Platform1");
        Platform2 = GameObject.Find("Platform2");
        Platform3 = GameObject.Find("Platform3");
        Platform4 = GameObject.Find("Platform4");

        //алмаз
        alm1 = GameObject.Find("gemRed1");
        alm2 = GameObject.Find("gemRed2");

        // если игровой персонаж был выбран в главном меню
        if (Global.is_character_enable == true)
        {
            // если был выбран первый персонаж
            if (Global.a == 1)
            {
                Person_1.active = true;
                Person_2.active = false;
                Person_3.active = false;
                Person_Zero.active = false;
                Person_1.GetComponent<Animator>().enabled = true;
                Person_1.GetComponent<SpriteRenderer>().enabled = true;               
                anim = GameObject.Find("Character/Person_1").GetComponent<Animator>();
                Char_HP = Global.HP_1;
                Char_Speed = Global.Speed_1;
                Vol_Jump = Global.Vol_Jump_1;
            }
            // если был выбран второй персонаж
            if (Global.b == 2)
            {
                Person_2.active = true;
                Person_1.active = false;
                Person_3.active = false;
                Person_Zero.active = false;
                Person_2.GetComponent<Animator>().enabled = true;
                Person_2.GetComponent<SpriteRenderer>().enabled = true;
                anim = GameObject.Find("Character/Person_2").GetComponent<Animator>();
                Char_HP = Global.HP_2;
                Char_Speed = Global.Speed_2;
                Vol_Jump = Global.Vol_Jump_2;
            }
            // если был выбран третий персонаж
            if (Global.c == 3)
            {
                Person_3.active = true;
                Person_1.active = false;
                Person_2.active = false;
                Person_Zero.active = false;
                Person_3.GetComponent<Animator>().enabled = true;
                Person_3.GetComponent<SpriteRenderer>().enabled = true;              
                anim = GameObject.Find("Character/Person_3").GetComponent<Animator>();
                Char_HP = Global.HP_3;
                Char_Speed = Global.Speed_3;
                Vol_Jump = Global.Vol_Jump_3;
            }
        }
    }
    /// Выполняем действия в методе FixedUpdate, т. к. в компоненте Animator персонажа
    /// выставлено значение Animate Physics = true и анимация синхронизируется с расчетами физики
    void FixedUpdate () {
        //определяем, на земле ли персонаж
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        //устанавливаем соответствующую переменную в аниматоре
        anim.SetBool("Ground", isGrounded);

        //устанавливаем в аниматоре значение скорости взлета/падения
        anim.SetFloat("vSpeed", GetComponent <Rigidbody2D>().velocity.y);

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
       GetComponent <Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent <Rigidbody2D>().velocity.y);

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
        if (isTake1 == true)
        {
            alm1.active = false;
        }
        if (isTake2 == true)
        {
            alm2.active = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            move = 1f * Char_Speed;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            move = 0;
            //Debug.Log("D key was released.");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            move = -1f * Char_Speed;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            move = 0;
            //Debug.Log("A key was released.");
        }

        //если персонаж на земле и нажат пробел...
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            //устанавливаем в аниматоре переменную в false
            anim.SetBool("Ground", false);

            //прикладываем силу вверх, чтобы персонаж подпрыгнул
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Vol_Jump));
        }

        // если персонаж коснулся земли и был слишком долго, нанести ему урон 
        if (isGrounded)
        {
            if (time_hp == true) {
                if (Person_1.active == true)
                {
                    Char_HP -= 9;
                }
                else Char_HP -= 5;
                                 }
            time_hp = false;
            timer = 2f;
        }
        if (Char_HP == 0 || Char_HP < 0) { /*Application.LoadLevel(Application.loadedLevel);*/ Check_point.transform(); }

    }

    /// Метод для смены направления движения персонажа и его зеркального отражения

    public void Flip()
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
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "dieCollider")
        {
            //Application.LoadLevel(Application.loadedLevel);
            Check_point.transform();
        }
        if (col.gameObject.name == "ammo(Clone)")
        {
            //Application.LoadLevel(Application.loadedLevel);
            if (Person_2.active == true) { Char_HP -= 3; } else Char_HP -= 5;
        }  
        if (col.gameObject.name == "ammo(inv)(Clone)")
        {
            //Application.LoadLevel(Application.loadedLevel);
            if (Person_2.active == true) { Char_HP -= 3; } else Char_HP -= 5;
        }


        //----------------- Двиг платформы
        if (col.gameObject.name == "Is_platform") { Char.transform.SetParent(Platform.transform, true); }
        if (col.gameObject.name == "Is_platform1") { Char.transform.SetParent(Platform1.transform, true); }
        if (col.gameObject.name == "Is_platform2") { Char.transform.SetParent(Platform2.transform, true); }
        if (col.gameObject.name == "Is_platform3") { Char.transform.SetParent(Platform3.transform, true); }
        if (col.gameObject.name == "Is_platform4") { Char.transform.SetParent(Platform4.transform, true); }



        //-------- кнопка
        if (col.gameObject.name == "event_distance") { event_for_button.is_pushed = true; }


        //----Хил
        if (col.gameObject.name == "potionRed")
        {
         if (Person_1.active == true && Char_HP == 30) { GameObject.Find("potionRed").active = true;}
         else if (Person_1.active == true && Char_HP < 30 ) { GameObject.Find("potionRed").active = false;
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

         if (Person_2.active == true && Char_HP == 45) { GameObject.Find("potionRed").active = true; }
         else if (Person_2.active == true && Char_HP < 45)
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
             if (Char_HP == 35 || Char_HP <35 ) { Char_HP += 10; }
         }


         if (Person_3.active == true && Char_HP == 18) { GameObject.Find("potionRed").active = true; }
         else if (Person_3.active == true && Char_HP < 18)
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

        // ---Топоры
        /*if (col.gameObject.name == "axe") { Char_HP -= 3; }*/
        

        //----- Дверь
       
        if (col.gameObject.name == "Door")
        {           
                  if(isTake1 == true && isTake2 == true){
                  Application.LoadLevel("TheEnd");   
                  }
                  if (Character_Controler_2.isTake3 == true && Character_Controler_2.isTake4 == true)
                  {
                      Application.LoadLevel("TheEnd");
                  }
                  if (isTake1 == true && Character_Controler_2.isTake4 == true)
                  {
                      Application.LoadLevel("TheEnd");
                  }
                  if (isTake2 == true && Character_Controler_2.isTake3 == true)
                  {
                      Application.LoadLevel("TheEnd");
                  }               
            
        }


        //----Алмазы

        if (col.gameObject.name == "gemRed1") {
            alm1.active = false;
            isTake1 = true;
        
        }

        if (col.gameObject.name == "gemRed2")
        {
            alm2.active = false;
            isTake2 = true;

        }


        //---Чекпоинт
        if (col.gameObject.name == "Check_pos")
        {
            Check_point.is_1_char_check = true;
            //Debug.Log("WORK!");
            Check_point.res_Char_1 = Char_HP;
        }
        //if (col.gameObject.name == "Area_of_triggered") { Enemy_Char.is_char_1_triggered = true; } // !!!!!!!!!
        if (col.gameObject.name == "Area_of_triggered_left")
        {
            Enemy_Char.is_char_1_triggered_left_1 = true;
            Enemy_Char.is_char_1_triggered_right_1 = false;
           //Enemy_Char.is_char_2_triggered_left = false;
            //Enemy_Char.is_char_2_triggered_right = false;
        }
        if (col.gameObject.name == "Area_of_triggered_right")
        {
            Enemy_Char.is_char_1_triggered_right_1 = true;
            Enemy_Char.is_char_1_triggered_left_1 = false;
            //Enemy_Char.is_char_2_triggered_left = false;
            //Enemy_Char.is_char_2_triggered_right = false;
        }
        if (col.gameObject.name == "Kill_zone")
        {
            Enemy_Char.Zombie_1_HP -= 1;
        }
        if (col.gameObject.name == "ukus") {
            Char_HP -= 5;
        }
    }
    public void OnTriggerExit2D(Collider2D Platform)
    {
        Char.transform.parent = null;
        Enemy_Char.is_char_1_triggered_left_1 = false;
        Enemy_Char.is_char_1_triggered_right_1 = false;
    }
    /*public void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.name == "Area_of_triggered")
        {
            Enemy_Char.is_char_1_triggered = true;
            //Enemy_Char.Triggered();
            Debug.Log("WORK!");
        }
    }*/
}
