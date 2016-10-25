using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class onclick : MonoBehaviour {
    public Button europe;
	// Use this for initialization
	void Start () {
        Button btn = europe.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void TaskOnClick()
    {
        Debug.Log("europe clicked");
    }
}
