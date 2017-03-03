using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotCount : MonoBehaviour {
    
    Text ammo;
    GameObject player;
	// Use this for initialization
	void Start () {
        ammo = GetComponent<Text>();
        ammo.text = "||||||";
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && player.GetComponent<Projectile>().firing == false)
        {
            ammo.text = "";
            int ammoAmount = 6 - player.GetComponent<Projectile>().shotsFired - 1;
            for(int i=0; i < ammoAmount; i++)
            {
                ammo.text += "|";
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ammo.text = "||||||";
        }
    }
}
