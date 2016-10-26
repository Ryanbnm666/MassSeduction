using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class drawScript : MonoBehaviour {
	LineRenderer line;
	Vector3 mousePos = new Vector3 (0, 0, 0);
	public bool mouseDown = false;
	List<Vector3> pointsList;
	public float score = 0;
	public int lives = 3;
	public int maxScore = 530;
	public bool alreadyCollided = false;
	public Text gameOverText;

	struct myLine
	{
		public Vector3 StartPoint;
		public Vector3 EndPoint;
	}

	// Use this for initialization
	void Awake () {
		line = gameObject.AddComponent<LineRenderer> ();
		line = transform.GetComponent<LineRenderer> ();
		line.SetVertexCount (0);
		line.SetWidth (0.1f, 0.1f);
		line.SetColors (Color.blue, Color.blue);
		line.useWorldSpace = true;
		pointsList = new List<Vector3> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (score < maxScore && alreadyCollided == false && lives > 0) {
			if (Input.GetMouseButtonDown (0)) {
				mouseDown = true;
				line.SetVertexCount (0);
				pointsList.RemoveRange (0, pointsList.Count);
				line.SetColors (Color.blue, Color.blue);
			} else if (Input.GetMouseButtonUp (0)) {
				mouseDown = false;
			}

			if (mouseDown) {
				mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				mousePos.z = -3;
				if (!pointsList.Contains (mousePos)) {
					pointsList.Add (mousePos);
					line.SetVertexCount (pointsList.Count);
					line.SetPosition (pointsList.Count - 1, (Vector3)pointsList [pointsList.Count - 1]);
					score++;
				}
			}
		} else if (score >= maxScore && alreadyCollided == false) {
			line.SetColors (Color.green, Color.green);
			gameComplete ();
		} else if (alreadyCollided == true) {
			score = 0;
		} else if (lives <= 0) {
			gameOver ();
		}

		if (Input.GetMouseButtonUp(0)) 
		{
			alreadyCollided = false;
		}
	}

	void gameOver()
	{
		gameOverText.enabled = true;
		print ("game over");
	}

	void gameComplete()
	{
        //CODE FOR WHEN GAME IS OVER GOES HERE//
        ////////////////////////////////////////////////////////
        /// CODE TO RETURN A NUKE AND GO BACK TO WORLD SCENE ///

        //Set to 2 (returning) and clear current
        Debug.Log(FindObjectOfType<Persist>().current + " SET TO RETURNING");

        FindObjectOfType<Persist>().continent[FindObjectOfType<Persist>().current] = 2;
        FindObjectOfType<Persist>().current = "";
        SceneManager.LoadScene("WorldScene");

        ////////////////////////////////////////////////////////
    }

    void OnMouseEnter()
	{
		if (mouseDown == true)
		{
			line.SetColors (Color.red, Color.red);

			if (alreadyCollided == false) 
			{
				lives--;
			}

			alreadyCollided = true;
		}
	}

	//void OnMouseExit()
	//{
	//	if (mouseDown == false)
	//	{
	//		alreadyCollided = false;
	//	}
	//}
}

