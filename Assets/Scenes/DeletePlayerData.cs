using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeletePlayerData : MonoBehaviour {

    string filename = "dataplayer.gd";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        Debug.Log("======");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("cba");
            if (File.Exists(Application.persistentDataPath + "/" + filename))
            {
                Debug.Log("abc");
                File.Delete(Application.persistentDataPath + "/" + filename);
            }
        }
    }
}
