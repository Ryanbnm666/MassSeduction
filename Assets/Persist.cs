using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Persist : MonoBehaviour {

    public Dictionary<string, int> continent;

    public string current;

    public int ID;

	// Use this for initialization
	void Start () {
        ID = Random.Range(1, 10000);

        GameObject persistCheck = GameObject.Find("Persist");

        if (persistCheck != null)
        {
            Persist perCk = persistCheck.GetComponent<Persist>();

            if(perCk.ID != ID)
            {
                Destroy(gameObject);
            }
        }


        continent = new Dictionary<string, int>();

        continent.Add("NORTH_AMERICA", 0);
        continent.Add("SOUTH_AMERICA", 0);
        continent.Add("ASIA", 0);
        continent.Add("AFRICA", 0);
        continent.Add("EUROPE", 0);
        continent.Add("OCEANIA", 0);

        

        DontDestroyOnLoad(transform.gameObject);


    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void dest()
    {
        Destroy(gameObject);
    }
}
