using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPlayerManager : MonoBehaviour {
    //public MainChar gamePlayer;
    public int HP;
    public int HPCurrent;
    public GameObject player;
    // Use this for initialization
    void Start () {
        //gamePlayer = FindObjectOfType<MainChar>();
        HPCurrent = HP;
    }

    // Update is called once per frame
    void Update () {
		if(HPCurrent <= 0)
        {
            //StartCoroutine("RespawnCoroutine");
            //gamePlayer.transform.position = new Vector3(0.09f, -3.170004f, gamePlayer.transform.position.z);
            HPCurrent = HP;
            player.transform.position = new Vector3(0.09f, -3.170004f, player.transform.position.z);
        }
	}

    public IEnumerator RespawnCoroutine()
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        Debug.Log("wait");
        player.transform.position = new Vector3(1.09f, -3.170004f, player.transform.position.z);
        player.gameObject.SetActive(true);
    }

    public void bloodLoss(int blood)
    {
        HPCurrent = HPCurrent - blood;
    }
}
