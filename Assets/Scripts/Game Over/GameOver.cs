﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Space"))
        {
            FindObjectOfType<Persist>().dest();
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }

    }
}
