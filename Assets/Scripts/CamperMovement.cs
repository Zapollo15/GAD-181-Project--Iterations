using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamperMovement : MonoBehaviour
{
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
            trns.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            lifecounter++;
        }

        if (collision.gameObject.tag == "basic enemy" && lifecounter == 2)
        {
            DestroyGameObject();
        }

        if (collision.gameObject.tag == "dead")
        {
            Destroy(collision.gameObject.transform.parent.gameObject);
        }

        if (collision.gameObject.tag == "spiky")
        {
            trns.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            lifecounter++;
        }

        if (collision.gameObject.tag == "spiky" && lifecounter == 2 &&  !DashOnShift(10))
        {
            DestroyGameObject();

        }

        if (collision.gameObject.tag == "spiky" && DashOnShift(10))
        {
            trns.localScale = new Vector3(1, 1, 1);
            Destroy(collision.gameObject);
            //Debug.Log("Collided");
        }

        void DestroyGameObject()
        {
           Destroy(gameObject);
        }

    }
}
