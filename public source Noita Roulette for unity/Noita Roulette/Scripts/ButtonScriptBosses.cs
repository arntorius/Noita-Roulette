using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonScriptBosses : MonoBehaviour
{
    // Set these in the Inspector
    public Button rollButton;
    public Text rollText;
    public Button[] numberButtons;
    public Button stopButton;
    public Text valueText;

    private int maxValue;




    private int running;
    private int counter;
    private int roll;

    IEnumerator Runner()
    {
        while (running > 0)
        {
           
            roll = Random.Range(0, maxValue + 1);
            rollText.text = roll.ToString();

            yield return new WaitForSecondsRealtime(0.1f);
        }
    }








    void Start()
    {




        running = 0;
        counter = 0;





        maxValue = 20; // default max value

        // Set the value of each number button
        for (int i = 0; i < numberButtons.Length; i++)
        {
            int value = i + 1;
            numberButtons[i].GetComponentInChildren<Text>().text = value.ToString();
            numberButtons[i].onClick.AddListener(() => OnNumberButtonClick(value));
        }

        // Set the onClick listener for the roll button
        rollButton.onClick.AddListener(OnRollButtonClick);
        stopButton.onClick.AddListener(TaskOnClick2);
    }

    void OnNumberButtonClick(int value)
    {
        // Set the max value for the random range
        maxValue = value;

        // Set the selected value to the text component
        SetValueText(value);
    }

    void OnRollButtonClick()
    {

        rollButton.gameObject.SetActive(false);
        stopButton.gameObject.SetActive(true);

        // Generate a random number within the specified range
        roll = Random.Range(0, maxValue + 1);

        // Set the rollText to the roll value
        rollText.text = roll.ToString();

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
  
    // Set the selected value to the text component
    public void SetValueText(int value)
    {
        valueText.text = value.ToString();
    }

}
