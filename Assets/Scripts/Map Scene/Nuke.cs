using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Nuke : MonoBehaviour {


    GameObject nukeOrigin;
    GameObject nukeDestination;

    Transform nukeStart;
    Transform nukeEnd;
    public float nukeSpeed;

    float startTime;
    float journeyLength;

    bool nukeEnabled;

    int originID;

    public float fracJourney;

    // Use this for initialization
    void Start () {
        fracJourney = 0.0f;

        nukeStart = nukeOrigin.transform;
        nukeEnd = nukeDestination.transform;

        startTime = Time.time;
        journeyLength = Vector3.Distance(nukeStart.position, nukeEnd.position);

        nukeEnabled = true;
    }
	
	// Update is called once per frame
	void Update () {

        if(nukeEnabled)
        {
            
            float distCovered = (Time.time - startTime) * nukeSpeed;

            fracJourney = distCovered / journeyLength;



            if (fracJourney < 1.0f)
            {

                Vector3 between = nukeStart.position - nukeEnd.position;

                if(between != Vector3.zero)
                {
                    float angle = Mathf.Atan2(between.y, between.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle + 90.0f, Vector3.forward);
                }

                


                /* Give arc to nuke launch (not working)
                Vector3 between = nukeStart.position - nukeEnd.position;
                Vector3 rightAngle = Vector3.Cross(Vector3.up, between);
                Vector3 normalRight = Vector3.Normalize(rightAngle);
                Vector3 addSpace = normalRight * (-Mathf.Pow(fracJourney - 0.5f, 2.0f) * 4 + 1);
                */
                transform.position = Vector3.Lerp(nukeStart.position, nukeEnd.position, fracJourney);

                // Nuke scales up as it gets to middle of journey, then scales down at the end
                float nukeSize = (-Mathf.Pow(fracJourney - 0.5f, 2.0f) * 2 + 1) / 6;
                transform.localScale = new Vector3(nukeSize, nukeSize, nukeSize);
            }
            else
            {
                //Nuke has reached destination! Explode!
                if(nukeDestination.name == "WestKorea")
                {
                    //Debug.Log("GAME OVER! NUKE LANDED IN WEST KOREA");
                    GameObject mapManagerObj = GameObject.Find("MapManager");
                    MapManager mapManager = mapManagerObj.GetComponent<MapManager>();
                    mapManager.gameOver();
                }
                else
                {
                    Debug.Log("WELL DONE! NUKE LANDED IN " + nukeDestination.name);
                    nukeEnabled = false;
                }
            }
        }

       
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("DatingScene", LoadSceneMode.Additive);
        }
    }


    //Reverse the nuke
    public void reverseNuke()
    {
        
        //Swap start and end transforms
        Transform tempTransform = nukeEnd;
        nukeEnd = nukeStart;
        nukeStart = tempTransform;

        //Swap start and end game objects
        GameObject tempGameObj = nukeOrigin;
        nukeOrigin = nukeDestination;
        nukeDestination = tempGameObj;

        //Store the old speed for calculates, set new speed to 0.2
        float oldSpeed = nukeSpeed;
        nukeSpeed = 0.2f;

        //Calculate the total travel time for nuke to get to current location
        float travelTime = Time.time - startTime;

        //Calculate the difference in speed
        float speedDifference = oldSpeed / nukeSpeed;

        //Calculate how long it would have taken to get to the end
        float estTotalTime = (travelTime / fracJourney);

        //Calculate the inveres percentage of journey (if nuke had gotten 20%, convert to 80%)
        float inverseFracJourney = (fracJourney * -1) + 1;

        //New total travel time is the old time * inverse fraction * speed differnce
        float newTravelTime = estTotalTime * inverseFracJourney * speedDifference;

        //New start time is current time - new est travel time
        startTime = Time.time - newTravelTime;

    }

    public void setRoute(GameObject origin, GameObject destination)
    {
        nukeOrigin = origin;
        nukeDestination = destination;        
    }

    public void setOrigin(int origin)
    {
        originID = origin;
    }

    public int getOrigin()
    {
        return originID;
    }
}
