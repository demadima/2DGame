using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Char : MonoBehaviour {

    public Transform right_is;
    public Transform left_is;
    public Transform Enemy;

    public static bool is_char_1_triggered_left_1 = false;
    public static bool is_char_1_triggered_right_1 = false;
    public static bool is_char_2_triggered_left_1 = false;
    public static bool is_char_2_triggered_right_1 = false;

    public static int Zombie_1_HP = 2;
    public static bool Zombie_1_die = false;

    //переменная для установки макс. скорости персонажа
    float maxSpeed = 5f;

    //переменная для определения направления персонажа вправо/влево
    private bool isFacingRight = true;

    //ссылка на компонент анимаций
    private Animator anim;

    //переменная отвечающая за скорость, за ее наличие
    public  static float move;

    //находится ли персонаж на земле или в прыжке?
    private bool isGrounded = false;

    //ссылка на компонент Transform объекта для определения соприкосновения с землей
    public Transform groundCheck;

    //радиус определения соприкосновения с землей
    private float groundRadius = 0.2f;

    //ссылка на слой, представляющий землю
    public LayerMask whatIsGround;

    public float timer = 7f;

    void Start () {


        anim = GameObject.Find("Enemy").GetComponent<Animator>();
    }

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

    void Update () {
        right_is.position = new Vector3(Enemy.position.x, Enemy.position.y - 0.8f, Enemy.position.z);
        left_is.position = new Vector3(Enemy.position.x, Enemy.position.y - 0.8f, Enemy.position.z);
        if (isGrounded){}
        if (is_char_1_triggered_left_1 == true && is_char_1_triggered_right_1 == false)
        {
            move = -0.4f;
            timer = 7f;
        }
        if (is_char_2_triggered_left_1 == true && is_char_2_triggered_right_1 == false)
        {
            move = -0.6f;
            timer = 7f;
        }
        if (is_char_1_triggered_right_1 == true && is_char_1_triggered_left_1 == false)
        {          
            move = 0.6f;
            timer = 7f;
        }        
        if (is_char_2_triggered_right_1 == true && is_char_2_triggered_left_1 == false)
        {
            move = 0.6f;
            timer = 7f;
        }
        if (is_char_1_triggered_left_1 == true && is_char_2_triggered_left_1 == true)
        {
            move = -0.6f;
            timer = 30f;
        }
        if (is_char_1_triggered_right_1 == true && is_char_2_triggered_right_1 == true)
        {
            move = 0.6f;
            timer =7f;
        }
        if (is_char_1_triggered_left_1 == true && is_char_2_triggered_right_1 == true)
        {
            move = -0.6f;
            timer = 7f;
        }
        if (is_char_2_triggered_left_1 == true && is_char_1_triggered_right_1 == true)
        {
            move = 0.6f;
            timer = 7f;
        }
        if (is_char_1_triggered_left_1 == false &&
            is_char_1_triggered_right_1 == false &&
            is_char_2_triggered_left_1 == false &&
            is_char_2_triggered_right_1 == false) { timer_enemy(); }
        if (Zombie_1_HP == 0 || Zombie_1_HP < 0)
        {
            Zombie_1_die = true;
            GameObject.Find("Enemy_obj").active = false;
            //Destroy(GameObject.Find("Enemy_obj"));
        }
    }
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
    public void timer_enemy()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            //Debug.Log("WORK!");
            move = 0;
            is_char_1_triggered_left_1 = false;
            is_char_1_triggered_right_1 = false;
            is_char_2_triggered_left_1 = false;
            is_char_2_triggered_right_1 = false;
            timer = 7f;
        }
    }

   
}
