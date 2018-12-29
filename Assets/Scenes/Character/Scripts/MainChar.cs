using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar : MonoBehaviour {
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
    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        attack1 = 100;
    }

    // Update is called once per frame
    void Update()
    {
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


        if (Input.GetKeyDown(KeyCode.Alpha1) && attack1 == 100)
        {
                playerAnimation.SetBool("attack1", true);
                attack1 = 0;
        } 
        else
        {
            if (attack1 < 100)
            {
                playerAnimation.SetBool("attack1", true);
                attack1++;
            }
            else
            {
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
