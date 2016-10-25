using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MashThrust : MonoBehaviour {
    public Slider thrustOMeter;
    public string correctKey;
    public float decreaseSpeed;
    public Text tutText;
    public Text thrustText;
    public Text failThrustText;
	// Use this for initialization
	void Start () {
        correctKey = "A";
        failThrustText.enabled = false;
        thrustText.enabled = false;
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
        }

        if(thrustOMeter.value == -100)
        {
            decreaseSpeed = 0;
            correctKey = null;
            tutText.enabled = false;
            failThrustText.enabled = true;

        }
	}
}
