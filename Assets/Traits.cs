using UnityEngine;
using System.Collections;

public class Traits : MonoBehaviour {
    public float intelligence = 0.0f;
    public float sass = 0.0f;
    public float outgoing = 0.0f;
    public string trait1;
    public string trait2;
    public string trait3;
    public float like = 0.0f;

    // Use this for initialization
    void Start () {
        intelligence = (Random.value * 100);
        sass = (Random.value * 100);
        outgoing = (Random.value * 100);

	}
	
	// Update is called once per frame
	void Update () {
	    if(intelligence > 50.0f)
        {
            trait1 = "Smart.";
        }
        else
        {
            trait1 = "Ditzy.";
        }

        if (sass > 50.0f)
        {
            trait2 = "Sassy.";
        }
        else
        {
            trait2 = "Polite.";
        }
        if (outgoing > 50.0f)
        {
            trait3 = "Outgoing.";
        }
        else
        {
            trait3 = "Introvert.";
        }
    }
}
