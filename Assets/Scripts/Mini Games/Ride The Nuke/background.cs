using UnityEngine;
using System.Collections;

public class background : MonoBehaviour {

    public float speed = 0.5f;
    public bool reverse;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 offset = new Vector2(Time.time * speed, 0);

        if(reverse)
        {
            offset = offset * -1;
        }

        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
