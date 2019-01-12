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
                {
                    collision.GetComponent<MainChar>().writeData(nextMap - 1);
                    SceneManager.LoadScene(nextMap + 1, LoadSceneMode.Single);
                    SceneManager.UnloadSceneAsync(nextMap);
                }
                else
                {
                    collision.GetComponent<MainChar>().addNote("*** Đường đi phía trước đầy nguy hiểm. Đại hiệp cần đạt " + level.ToString() + " cấp để vượt cảnh ***");
                }
            }
            else
            {
                collision.GetComponent<MainChar>().addNote("*** Đại hiệp đang trong trạng thái chiến đấu không thể vượt cảnh kế tiếp ***");
            }
        }
        //}

        //}
    } 
}
