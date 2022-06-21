using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float JumpForceHeight;
    public float TimeTakenForObjectToBounce;
    public Animator mushroomAnimator;
    


    private void Start()
    {
        mushroomAnimator.enabled = false;
    }
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, JumpForceHeight, 0), ForceMode.Impulse);
            mushroomAnimator.enabled = true;
            mushroomAnimator.Play("Bounce",-1,0);
        }
    }


   
}
