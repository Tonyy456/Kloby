using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Unnamed_Player", menuName = "Player")]
public class Player : ScriptableObject
{
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float strength = 5f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private Color accentColor;

    public Color color
    {
        get => accentColor;
        set
        {
            accentColor = value;
        }
    }

    public Sprite sprite
    {
        get => playerSprite;
    }

    public float Strength
    {
        get => strength;
    }


    public float HoldTime
    {
        get => strength / 4;
    }

    public float SprintTime
    {
        get => walkSpeed;
    }

    public float WalkSpeed
    {
        get => walkSpeed;
    }

    public float SprintSpeed
    {
        get => speed;
    }



}
