using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField] public KeptData data;
    public float TimeLeft;
    public bool TimerOn = false;

    public Text TimerText;
    // Start is called before the first frame update
    void Start()
    {
        TimerOn = true;
        if (data.timeBought == false){
            TimeLeft = 60;
        }
        if (data.timeBought == true){
            TimeLeft = 120;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(TimerOn){
            if(TimeLeft > 0){
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else{
                TimeLeft = 0;
                TimerOn = false;
                TimerText.enabled = false;
            }
        }
    }

    public void updateTimer(float currentTime){
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
