using UnityEngine;
using System.Collections;

public class cloud : MonoBehaviour {

    Rigidbody2D rb2d;
    public float speed;


    // Use this for initialization
    void Start () {
        rb2d = GetComponentInParent<Rigidbody2D>();
        rb2d.AddForce(Vector2.left * speed);
    }
	
	// Update is called once per frame
	void Update () {
   

	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
