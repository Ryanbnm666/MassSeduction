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
            SceneManager.LoadScene("DatingScene", LoadSceneMode.Single);
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
