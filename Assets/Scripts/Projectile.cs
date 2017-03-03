using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    //GameObject bullet;
    GameObject bulletCollider;
    AudioClip gunshot;
    //AudioClip reload;
    GameObject flare;
    public bool firing = false;
    public int shotsFired = 0;

    
	// Use this for initialization
	void Start () {
        bulletCollider = Resources.Load("Bullet_Collider") as GameObject;
        gunshot = Resources.Load("fire") as AudioClip;
        //reload = Resources.Load("reload") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && firing == false )//&& shotsFired < 6)
        {
            firing = true;
            GameObject collider = Instantiate(bulletCollider) as GameObject;
            //collider.transform.position = transform.position + Camera.main.transform.forward;
            collider.transform.position = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)).GetPoint(1);
            collider.transform.localRotation = Quaternion.LookRotation(Camera.main.transform.forward);
            collider.transform.localRotation *= Quaternion.Euler(0, 90, 0); // this add a 90 degrees rotation
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            AudioSource.PlayClipAtPoint(gunshot, transform.position);
            rb.velocity = Camera.main.transform.forward * 60;
            shotsFired++;
        }
        /*if (Input.GetKeyDown(KeyCode.R) && firing == false)
        {
            firing = true;
            AudioSource.PlayClipAtPoint(reload, transform.position);
            StartCoroutine(Reload());
            shotsFired = 0;
        }*/
	}

    public void allowShot()
    {
        firing = false;
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(3f); // waits 3 seconds
        firing = false; // will make the update method pick up 
    }
}
