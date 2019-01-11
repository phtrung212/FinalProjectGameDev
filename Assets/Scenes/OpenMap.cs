using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class OpenMap : MonoBehaviour {

    string filename = "dataplayer.gd";
    public GameObject map1;
    public GameObject map2;
    public GameObject map3;
    public GameObject map4;
    public GameObject map5;
    public GameObject map6;
    public GameObject map7;
    public GameObject map8;
    public GameObject map9;
    public GameObject map10;
    public GameObject map11;
    public GameObject map12;
    public database dataBase;
    // Use this for initialization
    void Start () {
        dataBase = new database(name, 0, 0, 1);
        if (File.Exists(Application.persistentDataPath + "/" + filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + filename, FileMode.Open);
            dataBase = (database)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + filename);
            bf.Serialize(file, dataBase);
            file.Close();
            /*BinaryFormatter bff = new BinaryFormatter();
            FileStream filef = File.Create(Application.persistentDataPath + "/" + filename);
            bff.Serialize(filef, dataBase);
            filef.Close();*/
        }
        map1.GetComponent<Animator>().SetInteger("level", dataBase.lv+1);
        map2.GetComponent<Animator>().SetInteger("level", dataBase.lv+1);
        map3.GetComponent<Animator>().SetInteger("level", dataBase.lv+1);
        map4.GetComponent<Animator>().SetInteger("level", dataBase.lv+1);
        map5.GetComponent<Animator>().SetInteger("level", dataBase.lv+1);
        map6.GetComponent<Animator>().SetInteger("level", dataBase.lv+1);
        map7.GetComponent<Animator>().SetInteger("level", dataBase.lv+1);
        map8.GetComponent<Animator>().SetInteger("level", dataBase.lv+1);
        map9.GetComponent<Animator>().SetInteger("level", dataBase.lv+1);
        map10.GetComponent<Animator>().SetInteger("level", dataBase.lv+1);
        map11.GetComponent<Animator>().SetInteger("level", dataBase.lv+1);
        map12.GetComponent<Animator>().SetInteger("level", dataBase.lv+1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
