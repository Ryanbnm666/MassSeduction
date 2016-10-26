using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MashThrust : MonoBehaviour {
    public Slider thrustOMeter;
    public string correctKey;
    public float decreaseSpeed;
    public Text tutText;
    public Text thrustText;
    public Text failThrustText;

    float returnTimer;

	// Use this for initialization
	void Start () {
        correctKey = "A";
        failThrustText.enabled = false;
        thrustText.enabled = false;

        returnTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        thrustOMeter.value -= (decreaseSpeed * Time.deltaTime);

	    if(Input.GetKeyDown(KeyCode.A))
        {
            if(correctKey == "A")
            {
                Debug.Log(correctKey);
                thrustOMeter.value += 2.5f;
                correctKey = "S";
            }
            
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if(correctKey == "S")
            {
                Debug.Log(correctKey);
                thrustOMeter.value += 2.5f;
                correctKey = "A";
            }
        }
        if(thrustOMeter.value == 100)
        {
            decreaseSpeed = 0;
            Debug.Log("MAXIMUM THRUST");
            tutText.enabled = false;
            thrustText.enabled = true;

            returnTimer += Time.deltaTime;

            if(returnTimer >= 5.0f)
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

        if(thrustOMeter.value == -100)
        {
            decreaseSpeed = 0;
            correctKey = null;
            tutText.enabled = false;
            failThrustText.enabled = true;

        }
	}


    void goToMap()
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
