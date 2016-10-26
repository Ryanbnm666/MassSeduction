using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class nuke : MonoBehaviour {


    float verticalVel;

    Rigidbody2D rb2d;

    public GameObject explosionQuad;

	// Use this for initialization
	void Start () {
        rb2d = GetComponentInParent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButton("Space"))
        {
            rb2d.AddForce(Vector2.up * 40.0f);
        }

        
        rb2d.rotation = rb2d.velocity.y * 2.0f;

        if(transform.position.x > 20.0f)
        {
            ////////////////////////////////////////////////////////
            /// CODE TO RETURN A NUKE AND GO BACK TO WORLD SCENE ///

            //Set to 2 (returning) and clear current
            Debug.Log(FindObjectOfType<Persist>().current + " SET TO RETURNING");

            FindObjectOfType<Persist>().continent[FindObjectOfType<Persist>().current] = 2;
            FindObjectOfType<Persist>().current = "";
            SceneManager.LoadScene("WorldScene");

            ////////////////////////////////////////////////////////
        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Cloud" || other.tag == "CloudEdge")
        {

            Time.timeScale = 0.0f;

            explosionQuad.SetActive(true);
            //GameObject[] clouds = GameObject.FindGameObjectsWithTag("Cloud");

        }
        
    }

    public void speedUp()
    {
        transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        rb2d.AddForce(Vector2.left * -10.0f);
    }

    //Remove Y constraint from rigid body to start game
    public void activate()
    {
        transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
    }
}
