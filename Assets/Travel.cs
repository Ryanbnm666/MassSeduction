using UnityEngine;
using System.Collections;

public class Travel : MonoBehaviour {
    public float speed;
    public GameObject nuke;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        nuke.transform.Translate(Vector2.up * speed * Time.deltaTime);
	}
}
