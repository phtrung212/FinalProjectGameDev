using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour {

    public int level;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        //if (collision.gameObject.tag == "Player") {
        //Debug.Log("1111111");
        //{
        Debug.Log("2222222222");
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        //}

        //}
    } 
}
