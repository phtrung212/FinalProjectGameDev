using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    private QuaiHPManager quaiHPManager;

    public void setup(QuaiHPManager quaiHPManager)
    {
        this.quaiHPManager = quaiHPManager;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Find("Bar").localScale = new Vector3(quaiHPManager.getHealthPercent(), 1,1);
    }
}
