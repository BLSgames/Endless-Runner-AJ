using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.PhysicsModule;
public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardVelocity = 2000f;
    private Vector3 movement;
    private float xSpeed = 20f;
    private float xMovement;
    public Animator playerAnim;
    // for Jump
    public float jumpVelocity = 20f;

    public float downAccel = 0.75f;
    private int JumpInput = 0;
    private int SlideInput = 0;
    private bool onGrounded = false;
    // for Collider Animation slide
    private CapsuleCollider _controller;
    private float _colliderSize;
    private float _colliderSizeJump;
    private float _colliderHeight;
    private Vector3 _colliderCenter;
    //
    private int difficultyLevel =1;
    private int maxDifficultyLevel = 10;
    private int scoreToNextLevel = 100;
    private int scoreTemp = 100;




    private void Start()
    {
       movement = Vector3.zero;

        _controller = GetComponent<CapsuleCollider>();
        _colliderHeight = _controller.height;
        _colliderCenter = _controller.center;
    }
    private void FixedUpdate()
    {
            scoreTemp = (int)transform.position.z - 3;

            TouchHandling();
            Run();
            CheckGrounded();
            ColliderHandling();
            JumpFn();
            MoveX();
            Slide();
            rb.velocity = movement;
            // _controller.Move(movement);

            //clamp the x position of player
            Vector3 playerPos = transform.position;
            playerPos.x = Mathf.Clamp(playerPos.x, -3f, 3f);
            playerPos.y = Mathf.Clamp(playerPos.y, -10f,10f);
            transform.position = playerPos;
            if(scoreTemp >= scoreToNextLevel)
            {
                LevelUp();
            }
    }
    private void LevelUp()
    {
        if(difficultyLevel ==  maxDifficultyLevel)
            return;

        scoreToNextLevel += 200;
        difficultyLevel++;
        forwardVelocity += difficultyLevel*100f;
    }
    
    private void Run()
    {
        // _velocity.z = forwardVelocity * Time.deltaTime;
        // _controller.Move(forwardVelocity*Time.deltaTime* Vector3.forward);
        movement.z = forwardVelocity *Time.deltaTime;

        
        
    }
    private void MoveX()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(xMovement, transform.position.y, transform.position.z), Time.deltaTime * xSpeed);
    }
    private void ColliderHandling()
    {
        //for slide animatin
        _colliderSize = playerAnim.GetFloat("ColliderSizeSlide");
        _colliderSizeJump = playerAnim.GetFloat("ColliderSizeJump");
        if (_colliderSize > 0.3f && _colliderSize < 1f)
        {
            _controller.height = 0.8f;
            _controller.center = new Vector3(_controller.center.x, 0.4f, _controller.center.z);
        }

        //for jump animation
        else if (_colliderSizeJump > 0.3f && _colliderSizeJump < 1f)
        {
            _controller.height = 0.8f;
            _controller.center = new Vector3(_controller.center.x, 1.4f, _controller.center.z);
        }
        else
        {
            _controller.height = _colliderHeight;
            _controller.center = _colliderCenter;
        }
    }

    private void JumpFn()
    {
        if (JumpInput == 1 && onGrounded)
        {
            movement.y = jumpVelocity * Time.deltaTime;
            // _controller.Move(Vector3.up * jumpVelocity*Time.deltaTime);
            // rb.AddForce(Vector3.up*Mathf.Sqrt(jumpVelocity* -2f*Physics.gravity.y), ForceMode.VelocityChange);
            playerAnim.SetTrigger("Jump");
        }
        else if (JumpInput == 0 && onGrounded)
        {
            movement.y = 0;
        }
        else
        {
            movement.y -= downAccel*Time.deltaTime;
        }
        JumpInput = 0;
    }
    private void CheckGrounded()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, 0.5f);
        onGrounded = false;
        rb.useGravity = true;
        foreach (var hit in hits)
        {
            if (!hit.collider.isTrigger)
            {
                if (movement.y <= 0)
                {
                    rb.position = Vector3.MoveTowards(rb.position, new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z), Time.deltaTime * 10);
                }
                rb.useGravity = false;
                onGrounded = true;
                break;
            }
        }
    }
    private void Slide()
    {

        if (SlideInput == 1 && onGrounded)
        {
            playerAnim.SetTrigger("Slide");

            SlideInput = 0;
        }
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
                        else if (Yswipe > 0) //slide
                        {
                            SlideInput = 1;
                        }
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