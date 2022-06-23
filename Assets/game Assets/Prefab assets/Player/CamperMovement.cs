using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamperMovement : MonoBehaviour
{
    public bool IsDebugMode;
    [Header("move settings")]
    Rigidbody rb;
    public int speed = 1500;
    public int DashSpeed = 20;
    [Header("jump settings")]
    public float  jump = 10;
    public float GravityMultiplier;
    public bool grounded;
    private int lifecounter;
    Transform trns;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 5; // adds more friction , prevents sliding when moving

        trns = GetComponent<Transform>();
        lifecounter = 0;
    }

    void Update()
    {
        MovePlayer(); // checks input and moves player
        Jump();// checks input, makes player jump
        IncreaseGravityWhileJumping();
        EnterDebugMode();
    }


    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector3(speed * 10 *  Time.deltaTime, 0, 0));
            DashOnShift(-DashSpeed);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector3(-speed * 10 * Time.deltaTime, 0, 0));
            DashOnShift(DashSpeed);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            rb.AddForce(new Vector3(0, jump * 10 , 0), ForceMode.Impulse);
            
            grounded = false;
        }
    }

    void IncreaseGravityWhileJumping()
    {
        if(rb.velocity.y<-0.2f)
        {
            rb.velocity+= Vector3.up *Physics.gravity.y * (GravityMultiplier - 1) * Time.deltaTime;
        }
    }

    bool DashOnShift(int dashSpeed) // we can put a float variable in these brackets when we call this function to specify negetic speed or positive speed when dashing
    {
        if (Input.GetKey(KeyCode.LeftShift) )
        {
            rb.AddForce(new Vector3(dashSpeed  * Time.deltaTime, 0, 0), ForceMode.Impulse);
            return true;
        }
        return false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            grounded = true;

        }

        //ENEMY IF STATEMENTS

        if (collision.gameObject.tag == "basic enemy")
        {
            DecreasePlayerLife();
        }

        if (collision.gameObject.tag == "dead")
        {
            Destroy(collision.gameObject.transform.parent.gameObject);
        }

        if (collision.gameObject.tag == "spiky"  &&  !DashOnShift(DashSpeed)) // hitting spiky with no dash, makes player lose life
        {
            DecreasePlayerLife();
        }
        else if (collision.gameObject.tag == "spiky" && DashOnShift(DashSpeed))// hitting spiky with  dash, makes player get life
        {
            IncreaseplayerLife();
            Destroy(collision.gameObject);
        }
    }

    void DecreasePlayerLife() 
    {
        trns.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        lifecounter++;
        CheckForGameOver(); 
    }
    void IncreaseplayerLife()
    {
        trns.localScale = new Vector3(1, 1, 1);
        lifecounter--;
    }
    void CheckForGameOver() // destroy player if life counter is 2
    {
        if ( lifecounter == 2 && !IsDebugMode)
        {
            Destroy(gameObject);
        }
    }
    void EnterDebugMode() // allows player to reatsrt scene automaticly or by input
    {
        if (IsDebugMode) 
        {
            if (lifecounter == 2 || gameObject.transform.position.y < -5 || Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
