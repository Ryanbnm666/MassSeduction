using UnityEngine;
using UnityEngine.SceneManagement;
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
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked!");
            SceneManager.LoadScene("DatingScene");
        }
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

public class NORTH_AMERICA
{
     public float deathToll = 8124170;
};
public class SOUTH_AMERICA
{
    public float deathToll = 3143530;
};
public class ASIA
{
    public float deathToll = 17132742;
};
public class AFRICA
{
    public float deathToll = 5120998;
};
public class EUROPE
{ 
    public float deathToll = 4824170;
};

public class OCEANIA
{
    public float deathToll = 2580209;
};

public class Continents
{
    NORTH_AMERICA NORTH_AMERICA;
    SOUTH_AMERICA SOUTH_AMERICA;
    ASIA ASIA;
    AFRICA AFRICA;
    EUROPE EUROPE;
    OCEANIA OCEANIA;
};