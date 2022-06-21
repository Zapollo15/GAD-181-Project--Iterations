using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 offset;
    public GameObject camper;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - camper.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offset + camper.transform.position;
    }
}
