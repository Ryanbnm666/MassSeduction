using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonScript2 : MonoBehaviour
{
    public float theNumber;
    public bool myBool;
    public string traitPos = "Outgoing.";
    public string traitNeg = "Introvert.";
    public string currentTrait;
    public GameObject nuclear;
    public Traits nukeTrait;
    public Dialogue dialogue;
    public Button yourButton;
    // Use this for initialization
    void Start()
    {
        theNumber = Random.value;
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (theNumber > 0.5f)
        {
            myBool = true;
        }
        else
        {
            myBool = false;
        }

        if (myBool == true)
        {
            currentTrait = traitPos;
        }
        else if (myBool == false)
        {
            currentTrait = traitNeg;
        }
    }

    void TaskOnClick()
    {
        Debug.Log("clicked!");
        if (currentTrait == nukeTrait.trait2)
        {
            nukeTrait.like = (nukeTrait.like + 1);
            dialogue.Success();
            print("Success.");
        }
        else if (currentTrait != nukeTrait.trait2)
        {
            nukeTrait.like = (nukeTrait.like - 1);
            dialogue.Failure();
            print("Failure.");
        }
        theNumber = Random.value;

    }
}
