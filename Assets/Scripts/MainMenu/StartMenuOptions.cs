using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuOptions : MonoBehaviour
{
    [SerializeField] private List<Player> players;
    [SerializeField] private int blueIndex;
    [SerializeField] private int redIndex;
    [SerializeField] private GameObject playerBlue;
    [SerializeField] private GameObject playerRed;

    [Header("SLIDERS")]
    [SerializeField] private Slider LeftStrengthSlider;
    [SerializeField] private Slider RightStrengthSlider;
    [SerializeField] private Slider LeftSpeedSlider;
    [SerializeField] private Slider RightSpeedSlider;

    [Header("SLIDER IMAGES")]
    [SerializeField] private Image LeftStrengthImage;
    [SerializeField] private Image RightStrengthImage;
    [SerializeField] private Image LeftSpeedImage;
    [SerializeField] private Image RightSpeedImage;

    private float maxSpeed = 0;
    private float maxStrength = 0;

    public void Start()
    {
        SetPlayer();
        foreach (var p in players)
        {
            if (p.SprintSpeed > maxSpeed)
            {
                maxSpeed = p.SprintSpeed;
            }
            if(p.Strength > maxStrength)
            {
                maxStrength = p.Strength;
            }
        }
        LeftStrengthSlider.maxValue = maxStrength;
        LeftSpeedSlider.maxValue = maxSpeed;
        RightStrengthSlider.maxValue = maxStrength;
        RightSpeedSlider.maxValue = maxSpeed;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("KlobyScene");
    }
    public void PlayerUpB()
    {
        blueIndex += 1;
        blueIndex %= players.Count;
        SetPlayer();
    }
    public void PlayerDownB()
    {
        blueIndex -= 1;
        while (blueIndex < 0)
            blueIndex += players.Count;
        SetPlayer();
    }
    public void PlayerUpO()
    {
        redIndex += 1;
        redIndex %= players.Count;
        SetPlayer();
    }
    public void PlayerDownO()
    {
        redIndex -= 1;
        while (redIndex < 0)
            redIndex += players.Count;
        SetPlayer();
    }
    private void SetPlayer()
    {
        Player lp = players[blueIndex];
        Player rp = players[redIndex];
        Game.leftPlayer = lp;
        Game.rightPlayer = rp;
        playerBlue.GetComponent<Image>().sprite = lp.sprite;
        playerRed.GetComponent<Image>().sprite = rp.sprite;

        LeftStrengthSlider.value = lp.Strength;
        RightStrengthSlider.value = rp.Strength;
        LeftSpeedSlider.value = lp.SprintSpeed;
        RightSpeedSlider.value = rp.SprintSpeed;

        Debug.Log(lp.color);
        Debug.Log(lp.SprintSpeed);
        LeftStrengthImage.color = lp.color;
        LeftSpeedImage.color = lp.color;
        RightStrengthImage.color = rp.color;
        RightSpeedImage.color = rp.color;
        
    }
}
