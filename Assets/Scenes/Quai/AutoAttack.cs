using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour {
    public int bloodLoss;
    public GameObject player;
    public float speed = 5f;
    public float jumpSpeed = 10f;
    private Rigidbody2D rigidBody;
    private Animator playerAnimation;
    bool flag;
    private HPPlayerManager gameHPManager;
    public int count;
    public int temp;
    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        gameHPManager = FindObjectOfType<HPPlayerManager>();
        flag = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (flag == true)
        {
            Debug.Log("ccccc");
            Debug.Log(Mathf.Abs(player.transform.position.x - transform.position.x));
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < 3.5)
            {
                if (count == temp)
                {
                    gameHPManager.bloodLoss(bloodLoss);
                    temp = 0;
                }
                else
                {
                    temp++;
                }
                playerAnimation.SetBool("Attack", true);
            }
            else
            {
                Debug.Log("ccccc11");
                if (player.transform.position.x < transform.position.x)
                {
                    Debug.Log(flag);
                    rigidBody.velocity = new Vector2(-Mathf.Abs(speed), rigidBody.velocity.y);
                    transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if (player.transform.position.x > transform.position.x)
                {
                    rigidBody.velocity = new Vector2(Mathf.Abs(speed), rigidBody.velocity.y);
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                playerAnimation.SetFloat("Speed", 0.2f);
                playerAnimation.SetBool("Attack", false);
            }
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            playerAnimation.SetFloat("Speed", 0f);
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("aaaaa");
            flag = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("bbbbbb");
            flag = false;
        }
    }
}
