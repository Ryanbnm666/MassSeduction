using UnityEngine;
using System.Collections;

public class continentScript : MonoBehaviour {

    bool mouseOver = false;
    public string continentName = "<UNDEFINED>";
    string nukePrefix = "\n-> Nuke is ";
    string nukeStatus = "[not launched].";
    int nuke = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        OnGUI();

        if (nuke == 0)
        {
            nukeStatus = "[not launched].";
        }
        else if (nuke == 1)
        {
            nukeStatus = "[incoming].";
        }
        else if (nuke == 2)
        {
            nukeStatus = "[returning].";
        }
	}

    void OnMouseOver()
    {
        mouseOver = true;
    }

    void OnMouseExit()
    {
        mouseOver = false;
    }

    void OnGUI()
    {
        if (mouseOver == true)
        {
            GUI.enabled = true;
            //continentName = GUI.TextField(new Rect(225, 62, 410, 20), continentName, 25);
            continentName = GUI.TextField(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y + 20, 
                160, 38), continentName + nukePrefix + nukeStatus);
        }
        else
        {
            GUI.enabled = false;
        }
        
        
    }
}
