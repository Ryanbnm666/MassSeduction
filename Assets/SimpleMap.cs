using UnityEngine;
using System.Collections;
using System.IO;

using UnityEngine.UI;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using System.Text;



public class SimpleMap : MonoBehaviour {

    public continentScript[] continent;

    public Persist persist;

    public Text destroyedConts;

    static float timer;

    // Use this for initialization
    void Start () {
        persist.current = "";

        foreach (continentScript cont in continent)
        {
            //cont.nuke = persist.continent[cont.continentName];
        }
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;


        foreach (continentScript cont in continent)
        {
            if (cont.nuke == 1)
            {
                break;
            }

                if (cont.nuke == 0)
            {
                cont.nuke = 1;
                persist.continent[cont.contName] = 1;
                break;
            }
        }


        int score = 0;
        foreach(continentScript cont in continent)
        {
            if(cont.nuke == 3)
            {
                score++;
            }
        }
        destroyedConts.text = score.ToString();
        

        if(score >= 6)
        {
            SceneManager.LoadScene("Win", LoadSceneMode.Single);
        }
	}
}
