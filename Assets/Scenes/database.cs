using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class database {
    public string name;
    public int lv;
    public int experence;
    public int lvMap;

	public database(string name, int lv, int experence, int lvMap)
    {
        this.name = name;
        this.lv = lv;
        this.experence = experence;
        this.lvMap = lvMap;
    }

    public database(string name, int lv, int experence)
    {
        this.name = name;
        this.lv = lv;
        this.experence = experence;
    }
}
