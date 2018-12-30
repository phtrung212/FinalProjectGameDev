using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MainChar : MonoBehaviour {
    public int level;
    public int dame;
    public string whoIsUsing;
    public bool isAttacking;
    public Camera Camera;
    public int HPMax;
    public int ManaMax;
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
    public Transform pfManaBar;
    Transform ManaBarTransform;
    HealthBar ManaBar;
    public QuaiHPManager Mana;
    public Transform pfLVBar;
    Transform LVBarTransform;
    LevelBar LVBar;
    public ExperenceManager Experence;
    // Use this for initialization
    void Start () {
        isAttacking = false;
        whoIsUsing = "";
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        attack1 = -1;
        HP = new QuaiHPManager(HPMax);
        healthBarTransform = Instantiate(pfHealthBar, new Vector3(Camera.transform.position.x - 7.39f, Camera.transform.position.y+6.19f), Quaternion.identity);
        healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.setup(HP);
        Mana = new QuaiHPManager(ManaMax);
        ManaBarTransform = Instantiate(pfManaBar, new Vector3(Camera.transform.position.x + 1.39f, Camera.transform.position.y -1.5f), Quaternion.identity);
        ManaBar = ManaBarTransform.GetComponent<HealthBar>();
        ManaBar.setup(Mana);
        Experence = new ExperenceManager(level);
        LVBarTransform = Instantiate(pfLVBar, new Vector3(Camera.transform.position.x + 1.39f, Camera.transform.position.y - 1.5f), Quaternion.identity);
        LVBar = LVBarTransform.GetComponent<LevelBar>();
        LVBar.setup(Experence);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = new Vector3(Camera.transform.position.x - 7.39f, Camera.transform.position.y+6.19f);
        ManaBar.transform.position = new Vector3(Camera.transform.position.x - 7.1f, Camera.transform.position.y +5.95f);
        LVBar.transform.position = new Vector3(Camera.transform.position.x - 19.2f, Camera.transform.position.y - 2.35f);
        if (HP.getHP() <= 0)
        {
            HP.returnHP();
            transform.position = new Vector3(0.09f, -3.170004f, transform.position.z);
        }
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer) || Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, QuaiLayer);
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
        if (Input.GetKeyDown(KeyCode.UpArrow) && isTouchingGround && isAttacking == false)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }
        playerAnimation.SetBool("onGround", isTouchingGround);
        if (Input.GetKeyDown(KeyCode.Alpha1) && attack1 == -1)
        {
            Mana.Damage(2);
            playerAnimation.SetBool("attack1", true);
            Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(QuaiCheckPoint.position, QuaiCheckRadius, QuaiLayer);
            if (enemiesToDamege.Length > 0)
            {
                Debug.Log("aaaaaaa");
                enemiesToDamege[0].GetComponent<AutoAttack>().HP.Damage(dame);
                float damg = dame + (Experence.getLevel() - enemiesToDamege[0].GetComponent<AutoAttack>().Level) * 0.1f * dame;
                int damgBlood = (int)damg;
                if (damgBlood > 0)
                    enemiesToDamege[0].GetComponent<AutoAttack>().HP.Damage(damgBlood);
            }

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

    public void setAttacking(string name)
    {
        if (whoIsUsing == "")
        {
            this.whoIsUsing = name;
            isAttacking = true;
        }
    }

    public void cancalAttacking(string name)
    {
        if (whoIsUsing == name)
        {
            this.whoIsUsing = "";
            isAttacking = false;
        }
    }


   
}
