using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NCC : MonoBehaviour
{
    [Header("PLAYER")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject lPlayer;
    [SerializeField] private GameObject rPlayer;

    public bool LeftPlayer { get; set; } = true;
    public PullBehavior PullBehavior { get => player.GetComponent<PullBehavior>(); }

    [Header("FORCE CONTROL")]
    [SerializeField] private float springConstant = 0;
    [SerializeField] private float ballMaxDistance = 3;
    [SerializeField] private float pushForce = 100;
    [SerializeField] private bool flipRotation = false;
    [SerializeField] private float rotationSpeed = 10000f;

    //[Header("MAX HOLD/SPRINT TIMERS")]
    private float maxSprintTime = 100;
    private float currSprintTime = 0;
    private float maxPullTime = 100;
    private float currPullTime = 0;

    [SerializeField] private GameObject colliderChecker;

    private InputAction moveAction;

    private float movespeed;
    private bool isBoosting;
    private float walkingSpeed;
    private float sprintSpeed;
    private Vector3 normalScale;
    private Vector3 sprintScale;

    private static int playersJoined = 0;


    public Vector2 stickDirection
    {
        get
        {
            if(moveAction == null)
            {
                Debug.Log("Null movement action");
                Application.Quit();
            }
            Vector2 v =  moveAction.ReadValue<Vector2>();
            //Debug.Log(v);
            return v;
        }
    }

    public void Start()
    {
        playersJoined += 1;
        if (playersJoined == 2)
        {
            LeftPlayer = false;
        }
        Player p;
        if (LeftPlayer)
        {
            flipRotation = false;
            player = Instantiate(lPlayer);
            p = Game.leftPlayer;
        }
        else
        {
            flipRotation = true;
            player = Instantiate(rPlayer);
            p = Game.rightPlayer;
        }
        player.transform.parent = Game.playerParent.transform;
        player.GetComponent<SpriteRenderer>().sprite = p.sprite;

        walkingSpeed = p.WalkSpeed;
        sprintSpeed = p.SprintSpeed;
        maxPullTime = p.HoldTime;
        maxSprintTime = p.SprintTime;
        movespeed = walkingSpeed;

        normalScale = player.transform.localScale;
        sprintScale = new Vector3(normalScale.x, normalScale.y * 6f / 7f, normalScale.z);

        var actions = GetComponent<PlayerInput>().currentActionMap.actions;
        foreach(InputAction action in actions)
        {
            if (action.name == "Move")
            {
                moveAction = action;
                moveAction.Enable();
            }
            if (action.name == "Boost")
            {
                action.canceled += stx =>
                {
                    StopSprint();
                };
            }
        }
        Game.OnPointChange += Reset;
    }

    private int counter = 0;
    public void Update()
    {
        if (!Game.GameRunning) return;
        Vector2 speedVector = stickDirection;
        Vector2 dPosition = speedVector * movespeed * Time.deltaTime;

        //Limit the amount of time you can sprint
        if (!isBoosting)
        {
            if (currSprintTime > 0)
                currSprintTime -= Time.deltaTime;
        }
        else
        {
            if (currSprintTime < maxSprintTime)
                currSprintTime += Time.deltaTime;
            else
                movespeed = walkingSpeed;
        }

        //Rotate player in direction of speed vector
        if (player.GetComponent<PullBehavior>() == null && speedVector != Vector2.zero)
        {
            Vector3 forward = Vector3.forward;
            if (flipRotation) forward = -forward;
            Quaternion toRotation = Quaternion.LookRotation(forward, Quaternion.Euler(0, 0, 90) * speedVector);
            player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        //make sure player cant move too far from rope
        var pb = player.GetComponent<PullBehavior>();
        if (pb != null)
        {
            if (currPullTime < maxPullTime)
            {
                var playerPos = player.transform.position;
                Vector2 target = new Vector2(playerPos.x, playerPos.y) + dPosition;
                Vector2 forceToBall = -1 * target.normalized;

                //calculate
                var newPosition = Vector3.MoveTowards(player.transform.position, target, 1000f);
                var distance = (newPosition - pb.pullObject.transform.position).magnitude;

                if (distance > ballMaxDistance)
                {
                    var rb = player.GetComponent<Rigidbody2D>();
                    rb.AddForce(springConstant * forceToBall);
                }
                currPullTime += Time.deltaTime;
            }
            else
            {
                Destroy(pb);
            }
        }
        else
        {
            if (currPullTime > 0)
                currPullTime -= Time.deltaTime;
        }

        if(LeftPlayer)
        {
            Game.leftSpeed = (maxSprintTime - currSprintTime) / maxSprintTime;
            Game.leftStrength = (maxPullTime - currPullTime) / maxPullTime;
        }
        else
        {
            Game.rightSpeed = (maxSprintTime - currSprintTime) / maxSprintTime;
            Game.rightStrength = (maxPullTime - currPullTime) / maxPullTime;
        }
        player.transform.Translate(dPosition, Space.World);
        counter += 1;
        if(counter > 500)
        {
            counter = 0;
        }
    }

    public void Pull()
    {
        var component = player.GetComponent<PullBehavior>();
        if (component != null)
        {
            Destroy(component);
            return;
        }
        GameObject outObj = Instantiate(colliderChecker);
        if (outObj.GetComponent<PullColliderCheck>() == null)
        {
            var comp = outObj.AddComponent<PullColliderCheck>();
            comp.callerObject = player.gameObject;
            comp.flip = flipRotation;
            comp.maxDistance = ballMaxDistance;
        }

        outObj.transform.position = player.transform.position + Quaternion.AngleAxis(-90, new Vector3(0, 0, 1)) * player.transform.up.normalized;
    }

    public void Push()
    {
        if (player.GetComponent<PushBehavior>() != null) return;
        GameObject outObj = Instantiate(colliderChecker);
        var pcc = outObj.AddComponent<PushColliderCheck>();
        pcc.callerObject = player.gameObject;
        pcc.pushForce = pushForce;
        outObj.transform.position = player.transform.position + Quaternion.AngleAxis(-90, new Vector3(0, 0, 1)) * player.transform.up.normalized;
    }

    public void Sprint()
    {
        counter = 0;
        isBoosting = true;
        player.transform.localScale = sprintScale;        
        movespeed = sprintSpeed;
    }

    public void StopSprint()
    {
        player.transform.localScale = normalScale;
        movespeed = walkingSpeed;
        isBoosting = false;
    }

    private void Reset()
    {
        Game.leftSpeed = 1;
        Game.rightSpeed = 1;
        Game.leftStrength = 1;
        Game.rightStrength = 1;
        var rb = player.GetComponent<Rigidbody2D>();
        rb.angularVelocity = 0f;
        rb.velocity = Vector2.zero;
        
        Destroy(player.GetComponent<PushBehavior>());
        Destroy(player.GetComponent<PullBehavior>());
    }
}


//HELLO I AM THE SPPOKY GHOST OF CHRISTMAS EVE. YOU MUST GIVE ME FOOD TO LIVE
//--TAYLOR 