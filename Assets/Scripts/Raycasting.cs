using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycasting : MonoBehaviour {

    GameObject terrain;
    GameObject block;
    GameObject boulder;
    GameObject text;
    Vector3 boulderSpawn = new Vector3(262.51f, -48.57f, 240.38f);
    public int keys = 0;
    bool first = true;
	// Use this for initialization
	void Start () {
        terrain = GameObject.FindGameObjectWithTag("Terrain");
        block = GameObject.FindGameObjectWithTag("Block");
        boulder = Resources.Load("Boulder") as GameObject;
        text = GameObject.FindGameObjectWithTag("Text");
    }

    void boulderSpawner()
    {
        Debug.Log("boulder spawning");
        GameObject boulderInit = Instantiate(boulder) as GameObject;
        boulderInit.transform.position = boulderSpawn;
        boulderInit.GetComponent<BoulderBehaviour>().move = true;
        
    }

    // Update is called once per frame
    void Update () {
        RaycastHit hit;
        //Debug.DrawRay(transform.position, -up * 2, Color.green);

        if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "SF_Door")
            {
                block.GetComponent<BoxCollider>().enabled = true;
                terrain.GetComponent<TerrainCollider>().enabled = false;
            }
            if (hit.collider.gameObject.tag == "Threshold")
            {
                if(first == true)
                {
                    InvokeRepeating("boulderSpawner", 5, 5);
                }
                first = false;
            }
            if (hit.collider.gameObject.tag == "Boulder")
            {
                Debug.Log("GAME OVER");
                text.GetComponent<Text>().text = "GAME OVER";
                InvokeRepeating("quitGame", 5, 5);
            }
            if (hit.collider.gameObject.tag == "Finish" && keys >= 3) //make sure player has 3 keys
            {
                Debug.Log(hit.collider.gameObject.name);
                terrain.GetComponent<TerrainCollider>().enabled = true;
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                //Rigidbody rb = player.GetComponent<Rigidbody>();
                //rb.velocity = Camera.main.transform.up * 100;
                player.transform.position = new Vector3(262.02f, 202.21f, 346.06f);
                player.transform.localRotation *= Quaternion.Euler(0, 180, 0); // this add a 90 degrees rotation
            }
            if (hit.collider.gameObject.tag == "Key")
            {
                Debug.Log("HIT KEY");
                keys++;
                Destroy(hit.collider.gameObject);
            }
            if (hit.collider.gameObject.tag == "Heli")
            {
                Debug.Log("YOU WIN");
                text.GetComponent<Text>().text = "YOU WIN";
                InvokeRepeating("quitGame", 5, 5);
            }
        }
    }

    void quitGame()
    {
        Application.Quit();
    }
}
