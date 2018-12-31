using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelBar : MonoBehaviour
{

    private ExperenceManager experenceManager;
    
    public void setup(ExperenceManager experenceManager)
    {
        this.experenceManager = experenceManager;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("Bar").localScale = new Vector3(experenceManager.getExperencePercent(), 1, 1);
    }
}
