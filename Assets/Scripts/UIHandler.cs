using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI blueScore;
    [SerializeField] private TextMeshProUGUI redScore;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI countdownTime;


    private int bs = 0;
    private int rs = 0;
    private float secondsleft = 0;
    private const float gameTime = 120f;

    public void Start()
    {
        secondsleft = gameTime;
        PointChangeHandler.OnPointChange += ResetTime;
    }

    public void RedScore()
    {
        rs += 1;
    }

    public void BlueScore()
    {
        bs += 1;
    }

    public void ResetTime()
    {
        secondsleft = gameTime;
    }



    // Update is called once per frame
    private void Update()
    {
        if (InputManager.countdown > 0.1f)
            countdownTime.text = "" + Mathf.FloorToInt(InputManager.countdown);
        else
            countdownTime.text = "";

        blueScore.text = "" + bs;
        redScore.text = "" + rs;

        if(secondsleft > 0f && InputManager.GameRunning)
            secondsleft -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(secondsleft / 60f);
        int seconds = Mathf.FloorToInt(secondsleft % 60);
        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);


    }
}
