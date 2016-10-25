using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cloudspawner : MonoBehaviour {

    public cloud cloudPrefab;
    public float spawnTime;

    float spawnTimer;
    float levelTimer;

    public float levelTime;
    public float startTime;

    public Text timerText;
    public Text statusText;

    public GameObject[] edgeClouds;

    public nuke nuke;

    bool inPlay;

    // Use this for initialization
    void Start() {
        spawnTimer = 65.0f;
        levelTimer = levelTime + startTime + 1;
        timerText.text = levelTime.ToString("00.00 F2");
        inPlay = false;

        statusText.text = "Press SPACE to add thrust!";
        statusText.fontSize = 70;
        timerText.fontSize = 100;
    }

    // Update is called once per frame
    void Update() {

        //Increment level timer each frame
        levelTimer = (levelTimer > 0) ? levelTimer -= Time.deltaTime : 0.0f;

        //If level hasn't starte display count down
        if (levelTimer > levelTime + 1)
        {
            timerText.text = Mathf.CeilToInt(levelTimer - levelTime - 1).ToString("0");
        }
        else
        {
            //If the game isn't yet in play, set to in play and activate nuke
            if(!inPlay)
            {
                inPlay = true;
                statusText.text = "";
                nuke.activate();
            }


            //Spawn a new cloud every cloudSpawnSpeed
            spawnCloud(spawnTime);

            //If there are no clouds and the timer is < 0 then the level is complete
            if (!cloudsInScene() && levelTimer <= 0.0f)
            {
                //Update GUI for win state
                statusText.text = "Well Done!";
                statusText.fontSize = 100;
                timerText.enabled = false;

                //Set edge clouds to colliders from triggers so the nuke can 'ride' them
                foreach (GameObject cloud in edgeClouds)
                {
                    cloud.GetComponent<BoxCollider2D>().isTrigger = false;
                }

                //Speed up the nuke to shoot it off screen
                nuke.speedUp();
            }

            //For the first second of play display 'GO!' as time, else display actual time.
            if (levelTimer > levelTime)
            {
                timerText.text = "GO!";
            }
            else
            {
                if(timerText.fontSize != 60)
                {
                    timerText.fontSize = 60;
                }

                //Update timer (make 0 if < 0)
                levelTimer = (levelTimer < 0) ? 0.0f : levelTimer;
                string s = levelTimer.ToString("00.00");
                string[] parts = s.Split('.');
                timerText.text = parts[0] + ":" + parts[1];
            }
            

        }

        

    }

    //Check if there are any clouds in the scene
    bool cloudsInScene()
    {
        object[] gameObjects = FindObjectsOfType(typeof(GameObject));
        foreach (object obj in gameObjects)
        {
            //Loop all game object, return true when a cloud is found
            GameObject gameObj = (GameObject)obj;
            if (gameObj.tag == "Cloud")
            {
                return true;
            }
        }

        return false;
    }


    //Spawn a new cloud if the correct time has elapsed
    void spawnCloud(float spawnTime)
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTime && levelTimer > 3)
        {
            //Reset timer, spawn cloud and give it a speed of 200.0f
            spawnTimer = 0.0f;
            cloud newCloud = Instantiate(cloudPrefab, transform.position + (Vector3.up * Random.Range(-5.0f, 5.0f)), transform.rotation) as cloud;
            newCloud.speed = 200.0f;
        }
    }

}