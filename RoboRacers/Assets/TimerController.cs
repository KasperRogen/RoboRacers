using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{

    public static TimerController instance;
    TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        timerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTimer(int _timeleft)
    {
        StartCoroutine(Timer(_timeleft));
    }


    IEnumerator Timer(int _timeleft)
    {
        Debug.Log("Starting timer");
        while(_timeleft >= 0)
        {
            Debug.Log("Timer is " + _timeleft);
            timerText.text = _timeleft--.ToString();
            yield return new WaitForSeconds(1);
        }
    }

}
