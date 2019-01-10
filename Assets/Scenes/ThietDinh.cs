using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThietDinh : MonoBehaviour {

    public Canvas menu;
	// Use this for initialization
	void Start () {
        menu.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            menu.enabled = true;
        }
    }
}
