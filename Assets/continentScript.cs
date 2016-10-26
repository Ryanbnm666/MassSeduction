using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class continentScript : MonoBehaviour {

    bool mouseOver = false;
    public string continentName = "<UNDEFINED>";
    public string contName;
    public string nukePrefix = "\n-> Nuke is ";
    public string nukeStatus = "[not launched].";

    float returnTimer;
   
    public int nuke = 0;

    // Use this for initialization
    void Start () {
        returnTimer = 0.0f;
        contName = continentName;
        nuke = FindObjectOfType<Persist>().continent[contName];
    }
	
	// Update is called once per frame
	void Update () {
        OnGUI();

        FindObjectOfType<Persist>().continent[contName] = nuke;

        if (nuke == 0)
        {
            nukeStatus = "[not launched].";
            
        }
        else if (nuke == 1)
        {
            nukeStatus = "[incoming].";
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if (nuke == 2)
        {
            nukeStatus = "[returning].";
        }
        else if (nuke == 3)
        {
            nukeStatus = "[returned].";
            GetComponent<SpriteRenderer>().color = Color.black;
        }


        if(nuke == 2)
        {
            returnTimer += Time.deltaTime;
        }

        if(returnTimer > 10.0f)
        {
            nuke = 3;
        }
    }

    void OnMouseOver()
    {
        mouseOver = true;
        if (Input.GetMouseButtonDown(0) && nuke == 1)
        {
            Debug.Log("clicked!");
            FindObjectOfType<Persist>().current = contName;
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
            continentName = GUI.TextField(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y + 20, 160, 38), continentName + nukePrefix + nukeStatus);
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