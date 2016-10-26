using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour {

    public float randomNumer;

    public GameObject[] launchSite;
    public GameObject westKorea;

    public GameObject nukePrefab;

    float mapTimer;

    public bool activeScene;

    bool[] nukesLaunched;
    float[] launchTimes;

    List<GameObject> nukes;


    const int NA = 0; //North America
    const int SA = 1; //South America
    const int OC = 2; //Oceania
    const int EU = 3; //Europe
    const int AS = 4; //Asia
    const int AF = 5; //Africa

    // Use this for initialization
    void Start () {
        mapTimer = 0.0f;

        activeScene = true;

        nukesLaunched = new bool[6];
        launchTimes = new float[6];

        for(int i = 0; i < 6; i++)
        {
            nukesLaunched[i] = false;
        }

        launchTimes[NA] = 3.0f;
        launchTimes[SA] = 30.0f;
        launchTimes[OC] = 60.0f;
        launchTimes[EU] = 90.0f;
        launchTimes[AS] = 120.0f;
        launchTimes[AF] = 150.0f;

        nukes = new List<GameObject>();

        

    }
	
	// Update is called once per frame
	void Update () {
        mapTimer += Time.deltaTime;

        //At 3 seconds launch nuke from North America to West Korea
        if(mapTimer > 3.0f && nukesLaunched[NA] == false)
        {
            nukesLaunched[NA] = true;
            addNuke(launchSite[NA], westKorea, NA);
        }

        //At 20 seconds launch nuke from Africa to West Korea
        if (mapTimer > 20.0f && nukesLaunched[AF] == false)
        {
            nukesLaunched[AF] = true;
            addNuke(launchSite[AF], westKorea, AF);
        }

        //At 40 seconds launch nuke from South America to West Korea
        if (mapTimer > 40.0f && nukesLaunched[SA] == false)
        {
            nukesLaunched[SA] = true;
            addNuke(launchSite[SA], westKorea, SA);
        }

        //At 60 seconds launch nuke from Oceania to West Korea
        if (mapTimer > 60.0f && nukesLaunched[OC] == false)
        {
            nukesLaunched[OC] = true;
            addNuke(launchSite[OC], westKorea, OC);
        }

        //At 80 seconds launch nuke from Europe to West Korea
        if (mapTimer > 80.0f && nukesLaunched[EU] == false)
        {
            nukesLaunched[EU] = true;
            addNuke(launchSite[EU], westKorea, EU);
        }

        //At 100 seconds launch nuke from Asia to West Korea
        if (mapTimer > 100.0f && nukesLaunched[AS] == false)
        {
            nukesLaunched[AS] = true;
            addNuke(launchSite[AS], westKorea, AS);
        }


    }

    void addNuke(GameObject origin, GameObject destination, int originID)
    {
        GameObject newNuke = (GameObject)Instantiate(Resources.Load("Nuke"));
        Nuke newNukeControl = newNuke.GetComponent<Nuke>();
        newNukeControl.setRoute(origin, destination);
        newNuke.transform.position = origin.transform.position;
        newNukeControl.setOrigin(originID);
        nukes.Add(newNuke);
    }

    public void gameOver()
    {
        SceneManager.LoadScene("Game Over", LoadSceneMode.Single);
    }

}
