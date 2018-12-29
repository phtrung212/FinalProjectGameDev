using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int Damage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for(int i=0;i< enemiesToDamege.Length; i++)
                {
                    enemiesToDamege[i].GetComponent<AutoAttack>().HP.Damage(Damage);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
	}
}
