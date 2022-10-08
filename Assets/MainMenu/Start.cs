using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Start : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField blue;
    [SerializeField] TMPro.TMP_InputField red;
    [SerializeField] TMPro.TMP_InputField duration;
    public void OnMouseUp()
    {
        Game.bluePlayer = blue.text;
        Game.redPlayer = red.text;
        try
        {
            int num = System.Int32.Parse(duration.text);
            Game.gameTime = num;
            if (num > 500)
            {
                Game.gameTime = 500;
            } 
            else if (num < 20)
            {
                Game.gameTime = 20;
            }
        } catch(System.FormatException)
        {
            Game.gameTime = 120f;
        }
        SceneManager.LoadScene("KlobyScene");
    }
}
