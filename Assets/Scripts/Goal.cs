using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] ResetHandler resetHandler;
    [SerializeField] UIHandler UIhandler;
    [Range(1, 2)]
    [SerializeField] private int team;


    private float triggerCD = 0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" && triggerCD <= 0f)
        {
            Debug.Log("Collision");
            triggerCD = 2f;
            resetHandler.Reset();
            PointChangeHandler.PointChange();
            switch (team)
            {
                case 1:
                    this.UIhandler.RedScore();
                    break;
                case 2:
                    this.UIhandler.BlueScore();
                    break;
                default:
                    throw new System.Exception("No Team found");
            }
        } 

    }

    private void Update()
    {
        if(triggerCD > 0f)
        {
            triggerCD -= Time.deltaTime;
        }
    }
}
