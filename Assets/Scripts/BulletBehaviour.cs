using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    GameObject player;
    float dist;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        dist = Vector3.Distance(transform.position, player.transform.position);
        if(dist > 100)
        {
            player.GetComponent<Projectile>().allowShot();
            Destroy(gameObject);
            
        }
	}

    void OnCollisionEnter(Collision col)
    {
       player.GetComponent<Projectile>().allowShot();
       Destroy(gameObject);
    }
}
