using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gamerunner : MonoBehaviour {

    float levelTimer;

    public GameObject indicator;

    public Button[] button;
    Dictionary<ButtonColour, Button> colouredButton;
    ButtonColour[] colourList;

    public GameObject explosionQuad;
    public Canvas UICanvas;

    public Image gameOver;

    float timeLeft;
    float waitTime;
    public Slider timeLeftSlide;
    float timeMultiplier;

    public Text scoreText;

    ButtonColour newColour;

    bool buttonPressed;
    bool gameRunning;
    bool gameWon;
    bool gameStarted;

    int totalPressed;

	// Use this for initialization
	void Start () {
        levelTimer = -1.0f;
        totalPressed = 0;
        waitTime = 0.0f;
        timeLeft = 15.0f;
        timeMultiplier = 1.0f;
        //Set colours
        colourList = new ButtonColour[9];
        colourList[0] = new ButtonColour("red");
        colourList[1] = new ButtonColour("yellow");
        colourList[2] = new ButtonColour("green");
        colourList[3] = new ButtonColour("lightblue");
        colourList[4] = new ButtonColour("darkblue");
        colourList[5] = new ButtonColour("white");
        colourList[6] = new ButtonColour("purple");
        colourList[7] = new ButtonColour("orange");
        colourList[8] = new ButtonColour("black");

        gameWon = false;
        gameStarted = false;
        gameOver.enabled = false;
        gameOver.GetComponentInChildren<Text>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        levelTimer += Time.deltaTime;

        //Show Ready Set Go at begning of level
        readySetGo();

        if(gameRunning)
        {
            timeLeft -= Time.deltaTime * timeMultiplier;
            timeLeftSlide.value = timeLeft;

            if (buttonPressed)
            {
                updateColours();
                totalPressed++;
                scoreText.text = totalPressed.ToString();
                timeLeft += 0.50f;
            }

            if(totalPressed > 40 && totalPressed < 50)
            {
                timeMultiplier = 1.2f;
            }
            else if (totalPressed >= 50 && totalPressed < 60)
            {
                timeMultiplier = 1.6f;
            }
            else if (totalPressed >= 60)
            {
                timeMultiplier = 2.0f;
            }



            if (timeLeft < 0.0f)
            {
                gameRunning = false;
                waitTime = levelTimer + 5.0f;

                if (totalPressed < 40)
                {
                    UICanvas.enabled = false;
                    explosionQuad.SetActive(true);
                }
                else
                {
                    gameOver.enabled = true;
                    gameOver.GetComponentInChildren<Text>().enabled = true;
                    gameOver.GetComponentInChildren<Text>().text = "WELL DONE!";
                }
            }
        }


        //If wait timer has been set then the level is over, go back to dating scene
        if (levelTimer > waitTime && waitTime != 0.0f)
        {
            SceneManager.LoadScene("DatingScene", LoadSceneMode.Single);
        }

    }

    //Update all button colours
    void updateColours()
    {
        //Shuffle colours
        shuffleColours(colourList);

        //Set colours
        setColours();

        //Select random colour from shuffled list
        int randColour = Random.Range(0, 8);
        newColour = colourList[randColour];

        //Set indicator to chosen colour
        indicator.GetComponent<Image>().color = newColour.colour;
        indicator.GetComponentInChildren<Text>().text = "";

        //Button pressed is false
        buttonPressed = false;
    }

    void setColours()
    {
        colouredButton = new Dictionary<ButtonColour, Button>();


        for (int i = 0; i < button.Length; i++)
        {
            colouredButton.Add(colourList[i], button[i]);
            button[i].image.color = colourList[i].colour;
        }

    }


    //Shuffle a list of colours
    void shuffleColours(ButtonColour[] colourList)
    {
        for(int i = 0; i < colourList.Length; i++)
        {
            int rand = Random.Range(0,i);
            ButtonColour tempColour = colourList[i];
            colourList[i] = colourList[rand];
            colourList[rand] = tempColour;

        }
    }


    void readySetGo()
    {
        if (levelTimer > 0.0f && levelTimer < 1.5f)
        {
            indicator.GetComponentInChildren<Text>().text = "READY";
        }
        if (levelTimer > 1.5f && levelTimer < 3.0f)
        {
            indicator.GetComponentInChildren<Text>().text = "SET";
        }
        if (levelTimer > 3.0f && levelTimer < 3.5f)
        {
            indicator.GetComponentInChildren<Text>().text = "GO!";
        }

        //If ready set go is done, start game and update colours
        if (levelTimer >= 3.5f && !gameRunning && !gameStarted)
        {
            gameRunning = true;
            gameStarted = true;
            updateColours();
            
        }
    }

    public void press(string btnName)
    {
        foreach(KeyValuePair<ButtonColour, Button> button in colouredButton)
        {
            //Find the buttn that is pressed
            if(button.Value.name == btnName)
            {
                //Check it it's the correct colour
                if(button.Key.strColour == newColour.strColour)
                {
                    Debug.Log("CORRECT COLOUR PRESSED");
                    buttonPressed = true;
                }
                else
                {
                    Debug.Log("WRONG BUTTON");
                    timeLeft -= 0.25f;
                }
            }
        }
    }
}

class ButtonColour
{

    public Color colour;
    public string strColour;

    public ButtonColour(string strColour)
    {
        this.strColour = strColour;
        switch (strColour)
        {
            case "red":
               colour = new Color(1.0f, 0.0f, 0.0f);
               break;

            case "yellow":
                colour = new Color(1.0f, 1.0f, 0.0f);
                break;

            case "green":
                colour = new Color(0.0f, 1.0f, 0.0f);
                break;

            case "lightblue":
                colour = new Color(0.45f, 1.0f, 1.0f);
                break;

            case "darkblue":
                colour = new Color(0.0f, 0.0f, 0.69f);
                break;

            case "white":
                colour = new Color(1.0f, 1.0f, 1.0f);
                break;

            case "purple":
                colour = new Color(0.48f, 0.0f, 0.48f);
                break;

            case "orange":
                colour = new Color(1.0f, 0.37f, 0.0f);
                break;

            case "black":
                colour = new Color(0.0f, 0.0f, 0.0f);
                break;

            default:
                //Default to Magenta
                colour = new Color(1.0f, 0.0f, 1.0f);
                break;

        }
    }
}