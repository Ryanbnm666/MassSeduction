using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour {
    List<string> dialoguePass = new List<string>();
    List<string> dialogueFail = new List<string>();
    int pick;
    public Text textBox;
    
    
	// Use this for initialization
	void Start () {
        dialoguePass.Add("Ayyy Gurl");
        dialoguePass.Add("You look ready to blow!");
        dialoguePass.Add("How's a nuke like you end up in a place like this?");
        dialoguePass.Add("You're positively glowing!");
        dialoguePass.Add("You come here often?");

        dialogueFail.Add("I've seen thinner nukes before!");
        dialogueFail.Add("Tell me more about your sister.");
        dialogueFail.Add("Ever thought about having Mini Nukes?");
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Success()
    {
        pick = Random.Range(0, dialoguePass.Count);
        textBox.color = Color.green;
        textBox.text = dialoguePass[pick];
        Debug.Log("Roll Success, printing dialogue.");
        Debug.Log(dialoguePass[pick]);
    }

    public void Failure()
    {
        pick = Random.Range(0, dialogueFail.Count);
        textBox.color = Color.red;
        textBox.text = dialogueFail[pick];
        Debug.Log("Roll Failure, printing dialogue");
        Debug.Log(dialogueFail[pick]);
    }
}
