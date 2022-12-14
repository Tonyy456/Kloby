using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [Header("PLAYER OBJECT")]
    [SerializeField] private GameObject playerObject;
    [SerializeField] private bool leftPlayer;
    public PullBehavior PullBehavior {
        get
        {
            return playerObject.transform.gameObject.GetComponent<PullBehavior>();
        }
    }

    [Header("SLIDERS")]
    [SerializeField] private Slider run;
    [SerializeField] private Slider hold;

    [Header("MOVEMENT")]
    private float walkingSpeed = 5;
    private float sprintSpeed = 10;
    private float rotationSpeed = 10000;

    [Header("FORCE CONTROL")]
    [SerializeField] private float springConstant = 0;
    [SerializeField] private float ballMaxDistance = 3;
    [SerializeField] private float pushForce = 100;
    [SerializeField] private bool flipRotation = false;

    [Header("MAX HOLD/SPRINT TIMERS")]
    [SerializeField] private float maxSprintTime = 100;
    [SerializeField] private float currSprintTime = 0;
    [SerializeField] private float maxPullTime = 100;
    [SerializeField] private float currPullTime = 0;

    private InputAction movementAction;
    private float movespeed;
    private GameObject colliderChecker;
    private bool isBoosting;

    public void Start()
    {
        Player p;
        if(leftPlayer)
        {
            p = Game.leftPlayer;
        } else
        {
            p = Game.rightPlayer;
        }
        playerObject.GetComponent<SpriteRenderer>().sprite = p.sprite;
        walkingSpeed = p.WalkSpeed;
        sprintSpeed = p.SprintSpeed;
    }

    public void SetInputAction(InputAction input, InputAction boost)
    {
        movespeed = walkingSpeed;
        movementAction = input;

        boost.performed += Boost;
        boost.canceled += Unboost;

        movementAction.Enable();
        boost.Enable();
    }

    void Boost(InputAction.CallbackContext context)
    {
        Vector3 scale = playerObject.transform.localScale;
        isBoosting = true;
        playerObject.transform.localScale = new Vector3(scale.x, scale.y * 6f / 7f, scale.z);
        movespeed = sprintSpeed;
    }

    void Unboost(InputAction.CallbackContext context)
    {
        Vector3 scale = playerObject.transform.localScale;
        playerObject.transform.localScale = new Vector3(scale.x, scale.y * 7f / 6f, scale.z);
        movespeed = walkingSpeed;
        isBoosting = false;
    }

    public void InitializeInput(InputAction pushAction, InputAction pullAction, GameObject collider)
    {
        pushAction.performed += Push;
        pullAction.performed += Pull;
        pushAction.Enable();
        pullAction.Enable();
        colliderChecker = collider;

        Game.OnPointChange += Reset;
    }

    private void Reset()
    {
        run.value = 1;
        hold.value = 1;
        playerObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Destroy(playerObject.GetComponent<PushBehavior>());
        Destroy(playerObject.GetComponent<PullBehavior>());
    }

    private void Push(InputAction.CallbackContext context)
    {
        if (playerObject.GetComponent<PushBehavior>() != null) return;
        GameObject outObj = Instantiate(colliderChecker);
        var pcc = outObj.AddComponent<PushColliderCheck>();
        pcc.callerObject = playerObject.gameObject;
        pcc.pushForce = pushForce;
        outObj.transform.position = playerObject.transform.position + Quaternion.AngleAxis(-90, new Vector3(0, 0, 1)) * playerObject.transform.up.normalized;
    }

    private void Pull(InputAction.CallbackContext context)
    {
        var component = playerObject.GetComponent<PullBehavior>();
        if (component != null)
        {
            Destroy(component);
            return;
        }
        GameObject outObj = Instantiate(colliderChecker);
        if (outObj.GetComponent<PullColliderCheck>() == null)
        {
            var comp = outObj.AddComponent<PullColliderCheck>();
            comp.callerObject = playerObject.gameObject;
            comp.flip = flipRotation;
            comp.maxDistance = ballMaxDistance;
        }

        outObj.transform.position = playerObject.transform.position + Quaternion.AngleAxis(-90, new Vector3(0, 0, 1)) * playerObject.transform.up.normalized;
    }

    public void Move(Vector2 v)
    {
        
    }

    public void Update()
    {
        if (!Game.GameRunning) return;
        Vector2 speedVector = movementAction.ReadValue<Vector2>();
        Vector2 dPosition = speedVector * movespeed * Time.deltaTime;

        //Limit the amount of time you can sprint
        if (!isBoosting) {
            if (currSprintTime > 0)
                currSprintTime -= Time.deltaTime;
        } else {
            if (currSprintTime < maxSprintTime)
                currSprintTime += Time.deltaTime;
            else
                movespeed = walkingSpeed;
        }

        //Rotate player in direction of speed vector
        if (playerObject.GetComponent<PullBehavior>() == null && speedVector != Vector2.zero)
        {
            Vector3 forward = Vector3.forward;
            if (flipRotation) forward = -forward;
            Quaternion toRotation = Quaternion.LookRotation(forward, Quaternion.Euler(0, 0, 90) * speedVector);
            playerObject.transform.rotation = Quaternion.RotateTowards(playerObject.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        //make sure player cant move too far from rope
        var pb = playerObject.GetComponent<PullBehavior>();
        if (pb != null)
        {
            if (currPullTime < maxPullTime) {
                var playerPos = playerObject.transform.position;
                Vector2 target = new Vector2(playerPos.x, playerPos.y) + dPosition;
                Vector2 forceToBall = -1 * target.normalized;

                //calculate
                var newPosition = Vector3.MoveTowards(playerObject.transform.position, target, 1000f);
                var distance = (newPosition - pb.pullObject.transform.position).magnitude;

                if (distance > ballMaxDistance) {
                    var rb = playerObject.GetComponent<Rigidbody2D>();
                    rb.AddForce(springConstant * forceToBall);
                }
                currPullTime += Time.deltaTime;
            } else {
                Destroy(pb);
            }
        } else {
            if (currPullTime > 0)
                currPullTime -= Time.deltaTime;
        }

        //Move the sliders accordingly
        run.value = (maxSprintTime - currSprintTime) / maxSprintTime;
        hold.value = (maxPullTime - currPullTime) / maxPullTime;
        playerObject.transform.Translate(dPosition, Space.World);
    }
}

