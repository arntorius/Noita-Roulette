using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeMe : MonoBehaviour
{
    public Text ButtonText;   
    private int running;
    private int counter;
  
    //texts list
    public string[] Texts = new string[] { "Shuffle wands only!", "No holy mountains!", "Middle perk everytime", "No Wand tinkering", "New Game +", "New Game ++", "New Game +++", "No flasks allowed!" };


    void Start()
    {
        running = 0;
        counter = 0;

    }

    IEnumerator Runner()
    {
        while (running > 0)
        {
            int textIndex = Random.Range(0, Texts.Length);
            ButtonText.text = Texts[textIndex];
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }


    public void TaskOnClick()
    {
        int textIndex = Random.Range(0, Texts.Length);
        ButtonText.text = Texts[textIndex];
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