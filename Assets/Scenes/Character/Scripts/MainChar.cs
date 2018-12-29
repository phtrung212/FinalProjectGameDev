using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MainChar : MonoBehaviour {
    public int HPMax;
    public float speed = 5f;
    public float jumpSpeed = 10f;
    private float moment = 0f;
    private Rigidbody2D rigidBody;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    private Animator playerAnimation;
    int attack1;
    public LayerMask QuaiLayer;
    public Transform QuaiCheckPoint;
    public float QuaiCheckRadius;
    public Transform pfHealthBar;
    Transform healthBarTransform;
    HealthBar healthBar;
    public QuaiHPManager HP;
    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        attack1 = -1;
        HP = new QuaiHPManager(HPMax);
        healthBarTransform = Instantiate(pfHealthBar, new Vector3(transform.position.x - 2f, transform.position.y + 2.5f), Quaternion.identity);
        healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.setup(HP);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = new Vector3(transform.position.x - 2.5f, transform.position.y + 3f);
        if (HP.getHP() <= 0)
        {
            HP.returnHP();
            transform.position = new Vector3(0.09f, -3.170004f, transform.position.z);
        }
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        moment = Input.GetAxis("Horizontal");
        if (moment > 0f)
        {
            playerAnimation.SetFloat("Speed", 1);
            Debug.Log("vao day roi");
            Debug.Log(transform.position);
            rigidBody.velocity = new Vector2(moment * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (moment < 0)
        {
            playerAnimation.SetFloat("Speed", 1);
            Debug.Log("vao day roi");
            Debug.Log(transform.position);
            rigidBody.velocity = new Vector2(moment * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            playerAnimation.SetFloat("Speed", 0);
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }
        playerAnimation.SetBool("onGround", isTouchingGround);
        if (Input.GetKeyDown(KeyCode.Alpha1) && attack1 == -1)
        {
            playerAnimation.SetBool("attack1", true);
            Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(QuaiCheckPoint.position, QuaiCheckRadius, QuaiLayer);
            if (enemiesToDamege.Length > 0)
                enemiesToDamege[0].GetComponent<AutoAttack>().HP.Damage(20);

            attack1++;
        }
        else
        {
            if (attack1 < 100 && attack1 >= 0)
            {
                playerAnimation.SetBool("attack1", true);
                attack1++;
            }
            else
            {
                attack1 = -1;
                playerAnimation.SetBool("attack1", false);
            }
        }



    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Quai")
        {
            
        }
    }
}
