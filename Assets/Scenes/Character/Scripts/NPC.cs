using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour {

    public int nextMap;
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
        Debug.Log(collision.name);
        if (collision.name == "Player")
        {
            Debug.Log("cccc");
            if (collision.GetComponent<MainChar>().isAttacking == false)
            {
                if (collision.GetComponent<MainChar>().Experence.getLevel() >= level)
                    SceneManager.LoadScene(nextMap, LoadSceneMode.Single);
                else
                {
                    collision.GetComponent<MainChar>().Note2.text = "Địa đạo phía cấp nguy hiểm. Đại hiệp tiên hoạch đắc " + level.ToString() + " cấp.";
                }
            }
            else
            {
                collision.GetComponent<MainChar>().Note2.text = "Đại hiệp. Ngươi tiên cấp hoàn thành đánh bại địch nhân hậu khả dĩ tiến nhập kế tiếp địa đồ ";
            }
        }
        //}

        //}
    } 
}
