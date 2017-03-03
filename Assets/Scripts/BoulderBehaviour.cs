using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoulderBehaviour : MonoBehaviour {

    AudioClip explode;
    GameObject player;
    GameObject text;
    //Rigidbody rb;
    public bool move = false;
    //int speed;
    // Use this for initialization
    void Start () {
        explode = Resources.Load("bakuhatu131") as AudioClip;
        player = GameObject.FindGameObjectWithTag("Player");
        //speed = 15;
        text = GameObject.FindGameObjectWithTag("Text");
        //rb = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        //RaycastHit hit;
        //transform.rotation = Quaternion.Euler(0, 0, 0);
        /*if (move)
        {
            if (Physics.Raycast(transform.position, transform.up, out hit, 2))
            {
                if (hit.collider.gameObject.tag == "Right")
                {
                    //rb.velocity = transform.right * speed;
                    transform.rotation = Quaternion.Euler(0, 90, 0); ;
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                else if (hit.collider.gameObject.tag == "Left")
                {
                    //rb.velocity = transform.right * -speed;
                    transform.rotation = Quaternion.Euler(0, -90, 0); ;
                    transform.Translate(-Vector3.right * speed * Time.deltaTime);
                }
                else
                {
                    //rb.velocity = transform.forward * speed;
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
            }
        }*/
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.tag);
        if ( col.gameObject.tag == "Player")
        {
            Debug.Log("GAME OVER");
            text.GetComponent<Text>().text = "GAME OVER";
            InvokeRepeating("quitGame", 5, 5);

        }
        if( col.gameObject.tag == "Bullet")
        {
            AudioSource.PlayClipAtPoint(explode, player.transform.position);
            Destroy(gameObject);
        }
    }

    void quitGame()
    {
        Application.Quit();
    }
}
