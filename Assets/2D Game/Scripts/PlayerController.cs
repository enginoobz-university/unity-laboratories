using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement vars:
    public float DefaultPosY = 0; //Default position to start the game with.

    //Game controls:
    public enum GameControls { UpAndDown = 0, RunAndJump = 1 } //Game Controls
    public GameControls GameControl = GameControls.UpAndDown & GameControls.RunAndJump;

    //Run and jump controls:
    bool IsJumping = false; //true if player is currently jumping.
    public float JumpForce = 10f; //Jump force.
    public float JumpingCoolDownTimer = 0.2f; //Time between two jumps in a row.
    float JumpingCoolDown = 0f;
    public float GroundedRadius = 0.05f;  //Radius of the circle that will determine if the player is grounded
    Transform GroundCheck;
    [HideInInspector]
    public bool Grounded = false; //Is the player grounded.
    public LayerMask GroundMask; //Mask to determine the ground to the player.
    public float PlayerX;

    //Up and down controls:
    public bool DeathLimitON = true; //Player dies when he reaches a specific height
    public float MaxY = 8; //Max screen height that the player can reach.
    public float MinY = -8; //Minimum screen height: Death limit if DeathLimitON is true; if false then its the height where the player stops moving down.
    [HideInInspector]
    public bool MovingUp = false; //True if the player is going up.
    [HideInInspector]
    public bool GoingDown = false; //True if the player hit an obstacle and he's falling down.
    [HideInInspector]
    public Vector3 CoinVelocity = Vector3.zero; //Coin velocity when the magnet is activated. Must be small so they can move fast.
                                                //Smooth movement:
    [HideInInspector]
    public float Velocity;
    public float DownSmooth = 0.7f; //Movement smooth time.
    public float UpSmooth = 0.8f;
    public bool Rotate = true; //Rotate the player on movement.
    public Quaternion DefaultRotation;
    public Quaternion UpRotation;
    public Quaternion DownRotation;
    [HideInInspector]
    public Quaternion CurrentRot;
    public float RotVelocity = 0.1f;
    [HideInInspector]
    public Transform MyTransform;

    //Revive:
    [HideInInspector]
    public int TimesToRevive = 1; //Times the player has used revive (-1 always).
    [HideInInspector]
    public bool Reviving = false; //Are we reviving the player?

    //CountDown variables:
    [HideInInspector]
    public float CountDown; //Before the game starts.
    [HideInInspector]
    public float LoosingCountDown; //When the player dies.


    //Player state:
    [HideInInspector]
    public bool IsRunning = false; //Game running.
    [HideInInspector]
    public bool IsPaused = false; //Player paused.
    [HideInInspector]
    public bool IsLoosing = false; //Player loosed and able to revive himself.
    [HideInInspector]
    public bool Lost = false; //Player lost and can't revive himself.
    [HideInInspector]
    public float Protection = 0; //Protection from obstacles after using some power ups.

    //GUI variables:
    [HideInInspector]
    public int Size; //This variable will be used to create most of the UI components.
    [HideInInspector]
    public bool LevelUP = false;
    [HideInInspector]
    public bool LevelingUP = false;


    // Start is called before the first frame update
    void Start()
    {
        //Set the player's transform.
        MyTransform = gameObject.transform;

        //Set the player state:
        IsRunning = false; //Still not running.
        IsPaused = false; //Not paused.
        IsLoosing = false; //Not loosing.
        TimesToRevive = 1; //No revive used.

        Size = Screen.height / 6;
    }

    // Update is called once per frame
    void Update()
    {
        float TempValue;

        if (GameControl == GameControls.UpAndDown)
        {
            //If the player touched the screen or pressed the mouse while he's not falling down (loosing).
            if ((Input.GetMouseButton(0) && !GoingDown) || (Input.GetKey(KeyCode.Space) && GoingDown == false))
            {
                //Move the player up and set the velocity to null to avoid glitches.
                MovingUp = true;
                if (Velocity < 0)
                    Velocity = 0;
            }
            else
            {
                //Move the player down and set the velocity to null to avoid glitches.
                MovingUp = false;
                if (Velocity > 0)
                    Velocity = 0;
            }

            if (MovingUp)
            {
                /*if(MyTransform.position.y < MaxY)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * 10, Space.World);
                }*/
                if (!GoingDown)
                {
                    //Smoothly move the player up.
                    TempValue = Mathf.SmoothDamp(MyTransform.position.y, MaxY, ref Velocity, UpSmooth);
                    MyTransform.position = new Vector3(MyTransform.position.x, TempValue, MyTransform.position.z);

                    //Change the player's rotation untill he reaches the maximum height.
                    if (MyTransform.position.y > MaxY - 1)
                        CurrentRot = DefaultRotation;
                    else
                        CurrentRot = UpRotation;
                }
                else //If the player is loosing.
                {
                    TempValue = Mathf.SmoothDamp(MyTransform.position.y, MinY - 5, ref Velocity, DownSmooth);
                    MyTransform.position = new Vector3(MyTransform.position.x, TempValue, MyTransform.position.z);
                }

                if (Rotate)
                {
                    Quaternion NewRot = Quaternion.Euler(CurrentRot.x, CurrentRot.y, CurrentRot.z);
                    transform.rotation = Quaternion.Slerp(transform.localRotation, NewRot, RotVelocity); //Updating the rotation
                }
            }
            else //If the player is moving down.
            {
                if (!GoingDown) //If the player is not loosing.
                {
                    //Smoothly move the player down.
                    if (DeathLimitON)
                        TempValue = Mathf.SmoothDamp(MyTransform.position.y, MinY - 5, ref Velocity, DownSmooth);
                    else
                        TempValue = Mathf.SmoothDamp(MyTransform.position.y, MinY, ref Velocity, DownSmooth);

                    MyTransform.position = new Vector3(MyTransform.position.x, TempValue, MyTransform.position.z);
                    //Change the player's rotation.
                    if (MyTransform.position.y < MinY + 1 && DeathLimitON == false)
                        CurrentRot = DefaultRotation;
                    else
                        CurrentRot = DownRotation;
                }
                else //If the player is loosing.
                {
                    TempValue = Mathf.SmoothDamp(MyTransform.position.y, MinY - 5, ref Velocity, DownSmooth);
                    MyTransform.position = new Vector3(MyTransform.position.x, TempValue, MyTransform.position.z);
                }

                if (Rotate)
                {
                    Quaternion NewRot2 = Quaternion.Euler(CurrentRot.x, CurrentRot.y, CurrentRot.z);
                    transform.rotation = Quaternion.Slerp(transform.localRotation, NewRot2, RotVelocity); //Updating the rotation
                }
            }
        }
    }
}
