using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartGame : MonoBehaviour {
    public Button startGame;
	// Use this for initialization
	void Start () {
        Button btn = startGame.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void TaskOnClick()
    {
        Debug.Log("clicked!");
        SceneManager.LoadScene("WorldScene");
    }
}
