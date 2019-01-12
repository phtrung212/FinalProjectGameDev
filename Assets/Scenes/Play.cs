using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour {
    public GameObject player;
    public int nextMap;
    public int levelNeedToJoin;
	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 20;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(player.GetComponent<OpenMap>().dataBase.lv);
            if (player.GetComponent<OpenMap>().dataBase.lv + 1 >= levelNeedToJoin)
            {
                SceneManager.LoadScene(nextMap, LoadSceneMode.Single);
                SceneManager.UnloadSceneAsync(1);
            }
        }
    }
}
