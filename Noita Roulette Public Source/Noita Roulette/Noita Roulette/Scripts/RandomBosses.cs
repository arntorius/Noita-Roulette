using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class RandomBosses : MonoBehaviour {

    public GameObject TextBox; 
    public int TheNumber;
    public Button RollButton;

    private int running;
    private int counter;

    void Start()
    {
        running = 0;
        counter = 0;

    }

    IEnumerator Runner() {
        while (running > 0)
        {
            TheNumber = Random.Range(0, 12);
            TextBox.GetComponent<Text>().text = "" + TheNumber;
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }


    public void TaskOnClick()
    {
        TheNumber = Random.Range(1, 12);
        TextBox.GetComponent<Text> ().text = "" + TheNumber;
        if (running == 0)
        {
            running = 1;
            StartCoroutine(Runner());
        } 
     }

    public void TaskOnClick2()
    {
            running = 0;

    }

}
