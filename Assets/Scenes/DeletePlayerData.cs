using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DeletePlayerData : MonoBehaviour {
    public Canvas note;
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
                database dataBase = new database(name, 0, 0, 1);
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/" + filename);
                bf.Serialize(file, dataBase);
                file.Close();
            }
            note.enabled = true;
        }
    }
}
