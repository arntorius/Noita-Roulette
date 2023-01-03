using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberCounter : MonoBehaviour
{
    public Text field;
    public float targetScore;
    public float currentDisplayScore = 0;


    void Start()
    {
        StartCoroutine(CountUpToTarget());
    }

    IEnumerator CountUpToTarget()
    {
        while (currentDisplayScore < targetScore)
        {
            currentDisplayScore += Time.deltaTime; // or whatever to get the speed you like
            currentDisplayScore = Mathf.Clamp(currentDisplayScore, 0f, targetScore);
            field.text = currentDisplayScore + "";
            yield return null;
        }
    }

}
