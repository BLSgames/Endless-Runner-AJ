using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.PhysicsModule;
public class PlayerController : MonoBehaviour
{

    private CharacterController _controller;
    public float forwardVelocity = 5f;
    private Vector3 movement;
    private float xMovement;
    private Animator playerAnim;
    // for Jump
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float Gravity = -15f;
    private int JumpInput = 0;
    private bool onGrounded = false;
    public float GroundedOffset = -0.14f; //for rough ground
    public float GroundedRadius = 0.28f;  //ground check radius same as cc
    public LayerMask GroundLayers;
    // for Difficulty
    private int difficultyLevel =1;
    private int maxDifficultyLevel = 10;
    private int scoreToNextLevel = 100;
    private int scoreTemp = 100;

    private void Awake() {
        _controller = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
    }


    private void Start()
    {
        movement = Vector3.zero;

    }
    private void Update()
    {
            scoreTemp = (int)transform.position.z - 3;

            TouchHandling();
            CheckGrounded();
            JumpFn();
            MoveX();
        
            //clamp the x position of player
            Vector3 playerPos = transform.position;
            playerPos.x = Mathf.Clamp(playerPos.x, -3f, 3f);
            playerPos.y = Mathf.Clamp(playerPos.y, -10f,10f);
            transform.position = playerPos;
            
            // move character controller
            _controller.Move(forwardVelocity*Time.deltaTime* Vector3.forward + new Vector3(0f, movement.y*Time.deltaTime, 0f));
            
            if(scoreTemp >= scoreToNextLevel)
            {
                LevelUp();
            }

    }
    private void LevelUp()
    {
        if(difficultyLevel ==  maxDifficultyLevel)
            return;

        scoreToNextLevel += 300;
        difficultyLevel++;
        forwardVelocity += difficultyLevel*2f;
    }
    
    private void MoveX()
    {
        // transform.position = Vector3.MoveTowards(transform.position, new Vector3(xMovement, transform.position.y, transform.position.z), Time.deltaTime * xSpeed);
        Vector3 targetpos = Vector3.right* xMovement;
        Vector3 Currentpo = new Vector3 (transform.position.x, 0f, 0f);
        Vector3 diff = targetpos - Currentpo;
        Vector3 moveDir = diff.normalized *25f* Time.deltaTime;
        if(moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            _controller.Move(moveDir);
        }
        else
            _controller.Move(diff);
    
    }
    
    private void JumpFn()
    {
        if (JumpInput == 1 && onGrounded)
        {
            
            movement.y = Mathf.Sqrt(jumpHeight * -2f * Gravity) ;
            
            playerAnim.SetTrigger("Jump");
        }
        else if (JumpInput == 0 && onGrounded)
        {
            movement.y = -1f;
        }
        else
        {
            movement.y += Gravity*Time.deltaTime;
        }
        JumpInput = 0;
    }
    private void CheckGrounded()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        onGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }
    

    // Touch Controle
    private Touch sTouch;
    private bool hasSwiped = false;
    private void TouchHandling()
    {

        foreach (Touch t in Input.touches)
        {
            //begin
            if (t.phase == TouchPhase.Began)
            {
                sTouch = t;
            }
            //Moves
            else if (t.phase == TouchPhase.Moved && !hasSwiped)
            {
                float Xswipe = sTouch.position.x - t.position.x;
                float Yswipe = sTouch.position.y - t.position.y;
                float Distance = Mathf.Sqrt(Xswipe * Xswipe) + Mathf.Sqrt(Yswipe * Yswipe);
                bool isVertical = false;
                if (Mathf.Abs(Xswipe) < Mathf.Abs(Yswipe))
                {
                    isVertical = true;
                }
                if (Distance > 5f)
                {
                    if (isVertical)
                    {
                        if (Yswipe < 0) // Jump
                        {
                            JumpInput = 1;
                        }
                        // else if (Yswipe > 0) //slide
                        // {
                        //     SlideInput = 1;
                        // }
                    }
                    else if (!isVertical)
                    {
                        if (Xswipe < 0) //Right move
                        {
                            if (xMovement == 0)
                            {
                                xMovement = 3f;

                            }
                            else if (xMovement == -3)
                            {
                                xMovement = 0f;
                            }

                        }
                        else if (Xswipe > 0) //Left Move
                        {
                            if (xMovement == 0)
                            {
                                xMovement = -3;
                            }
                            else if (xMovement == 3)
                            {
                                xMovement = 0f;
                            }


                        }

                    }
                    hasSwiped = true;
                }
            }
            //End
            else if (t.phase == TouchPhase.Ended)
            {
                sTouch = new Touch();
                hasSwiped = false;
            }
        }
    }
}