using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float ObjectLength;
    private float startPosition;
    private Camera Camera;
    [SerializeField] float ParallexSpeed;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        Camera = FindObjectOfType<Camera>();
        AssignObjectLenght();
    }



    // Update is called once per frame
    void Update()
    {
        StartParallaxEffect();
    }

    void StartParallaxEffect()
    {
        float ParallexDistance = Camera.transform.position.x * ParallexSpeed;
        transform.position = new Vector3(startPosition+ParallexDistance, transform.position.y, transform.position.z);
        MoveParallaxObject();
    }

    void MoveParallaxObject()
    {
        if(CurrentPositionOfCamera() > startPosition + ObjectLength)
        {
            startPosition += ObjectLength;
        }
        else if(CurrentPositionOfCamera() < startPosition - ObjectLength)
        {
            startPosition -= ObjectLength;
        }
    }

    float  CurrentPositionOfCamera()
    {
        float CamPosition = Camera.transform.position.x *(1- ParallexSpeed);
        return CamPosition;
    }

    void AssignObjectLenght()
    {
        ObjectLength = gameObject.GetComponent<BoxCollider>().bounds.size.x;
    }

}
