using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TienNhapDiaDo : MonoBehaviour {
    string filename = "dataplayer.gd";
    public database dataBase;

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
            dataBase = new database(name, 0, 0, 1);
            if (File.Exists(Application.persistentDataPath + "/" + filename))
            {
                Debug.Log("1111111111");
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + filename, FileMode.Open);
                dataBase = (database)bf.Deserialize(file);
                file.Close();
            }
            else
            {
                Debug.Log("2222222222");
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/" + filename);
                bf.Serialize(file, dataBase);
                file.Close();
            }
            Debug.Log(dataBase.lvMap);
            SceneManager.LoadScene(dataBase.lvMap + 1, LoadSceneMode.Single);
            SceneManager.UnloadSceneAsync(0);
        }
    }
}
