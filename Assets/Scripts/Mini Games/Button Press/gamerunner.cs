using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gamerunner : MonoBehaviour {



    float levelTimer;               //Time since the level started
    float timeLeft;                 //Time left in the slider
    float waitTime;                 //Time to wait until moving back to dating scene
    float timeMultiplier;           //After a score of 40 the multiplier goes to to make it harder


    public Slider timeLeftSlide;    //Slider that displays the time left
    public GameObject indicator;    //The colour indicator at the bottom of the screen

    public Canvas UICanvas;         //The UI canvas that stores all UI elements (disabled when nuke explodes)
    public Button[] button;         //Array containing all colour buttons
    public Image gameOver;          //Game over text used to display 'Well done'
    public Text scoreText;          //Text item that displays the score

    Dictionary<ButtonColour, Button> colouredButton;    //Dictionary linking buttons with colour properties
    ButtonColour[] colourList;          //List of possible button colours that is shuffled each correct press
    ButtonColour newColour;             //The colour that is represented in the indicator
    public GameObject explosionQuad;    //The explosion quad that displays nuke explosion on failure
 
    bool buttonPressed;             //Bool storing is a button was recently corretly pressed
    bool gameRunning;               //Bool to store running state
    bool gameWon;                   //Bool to store if game is won
    bool gameStarted;               //Bool to store if game has been started

    int totalPressed;               //Total score

	// Use this for initialization
	void Start () {

        //Initialize variables
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

        //Initialize variables
        gameWon = false;
        gameStarted = false;

        //Hide game over text
        gameOver.enabled = false;
        gameOver.GetComponentInChildren<Text>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Increment time each frame
        levelTimer += Time.deltaTime;

        //Show Ready Set Go at begning of level
        readySetGo();

        //Do logic if the game is now running
        if(gameRunning)
        {
            //Subtract from time left multiplied by time difficulty multiplier and set slider value
            timeLeft -= Time.deltaTime * timeMultiplier;
            timeLeftSlide.value = timeLeft;

            //If the correct button has just been pressed generate new colours, add score and add time to timer
            if (buttonPressed)
            {
                updateColours();
                totalPressed++;
                scoreText.text = totalPressed.ToString();
                timeLeft += 0.50f;
            }

            //Change difficulty multiplier after 40
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


            //If no time is left then the game is over
            if (timeLeft < 0.0f)
            {
                gameRunning = false;
                waitTime = levelTimer + 5.0f; //Wait time is the time that the level will change to the dating scene (now + 5s)

                //If score is < 40 then the player has lost, hide canvas and show explosion
                if (totalPressed < 40)
                {
                    UICanvas.enabled = false;
                    explosionQuad.SetActive(true);
                }
                //If score is > 40 then the player has won, congratulate the player!
                else
                {
                    gameOver.enabled = true;
                    gameOver.GetComponentInChildren<Text>().enabled = true;
                    gameOver.GetComponentInChildren<Text>().text = "WELL DONE!\n" + "Score: " + totalPressed ;
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

    //Set the colours for each button to the newly shuffled colour list
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
        //Loop through all colours and shuffle them
        for(int i = 0; i < colourList.Length; i++)
        {
            int rand = Random.Range(0,i);
            ButtonColour tempColour = colourList[i];
            colourList[i] = colourList[rand];
            colourList[rand] = tempColour;

        }
    }


    //Display 'Ready', 'Set', and 'Go' in the indicator - also start the game once sequence is over
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
            updateColours();    //Shuffle and set coloured buttons
            
        }
    }

    //Called by a button when it is pressed
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
                    //The button is pressed, logic for what happens now is in the main loop
                    buttonPressed = true;
                }
                else
                {
                    //The wrong button has been pressed, punish the player by subtracting 1/4 seconds
                    timeLeft -= 0.25f;
                }
            }
        }
    }
}


//Button colour class stores a colour along with it's string representation
class ButtonColour
{

    public Color colour;
    public string strColour;

    public ButtonColour(string strColour)
    {
        //Construct the ButtonColour using the string and setting the appropriate colour
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