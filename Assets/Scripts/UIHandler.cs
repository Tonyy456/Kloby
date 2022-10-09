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
    [SerializeField] private TextMeshProUGUI bluePlayerName;
    [SerializeField] private TextMeshProUGUI redPlayerName;

    [SerializeField] private Slider lst;
    [SerializeField] private Slider rst;
    [SerializeField] private Slider lsp;
    [SerializeField] private Slider rsp;


    private int bs = 0;
    private int rs = 0;
    private float secondsleft = 0;
    private float gameTime = 120f;

    public void Start()
    {
        gameTime = Game.gameTime;
        secondsleft = gameTime;
        bluePlayerName.text = Game.bluePlayer;
        redPlayerName.text = Game.redPlayer;
        Game.OnGameOver += Reset;
    }

    public void RedScore()
    {
        rs += 1;
    }

    public void BlueScore()
    {
        bs += 1;
    }

    private void Reset()
    {
        secondsleft = gameTime;
    }

    // Update is called once per frame
    private void Update()
    {
        //Set game countdown
        if (Game.countdown > 0.1f)
            countdownTime.text = "" + Mathf.FloorToInt(Game.countdown);
        else
            countdownTime.text = "";

        //Display scores
        blueScore.text = "" + bs;
        redScore.text = "" + rs;

        //Update time left
        if(secondsleft > 0f && Game.GameRunning)
            secondsleft -= Time.deltaTime;
        if(secondsleft < 0f)
            Game.GameEnded();

        //Display the time left
        int minutes = Mathf.FloorToInt(secondsleft / 60f);
        int seconds = Mathf.FloorToInt(secondsleft % 60);
        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        //Move the sliders accordingly
        lsp.value = Game.leftSpeed;
        lst.value = Game.leftStrength;
        rsp.value = Game.rightSpeed;
        rst.value = Game.rightStrength;
    }
}
