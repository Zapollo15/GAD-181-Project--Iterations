using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallingplatform : MonoBehaviour
{
    [Range(0,200)]
    public float frequency;
    [Range(0,10)]
    public float amplitude;
    [Range(0, 5)]
    public float SecondsToShake;
    [Range(0, 500)]
    public float DownwardForce;

    float time = 0;
    bool StartDroping = false;
    Rigidbody rb;
    MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        DropPlatform();
        DestroyPlatform();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag== "Player")
        {
            StartDroping = true;
        }
    }

    void DropPlatform()
    {
        if(StartDroping)
        {
            if(!ShakePlatform())
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(0, DownwardForce * Time.deltaTime, 0);
        }
    }
    bool ShakePlatform()
    {
        time += Time.deltaTime;
        if(time < SecondsToShake)
        {
            float x = amplitude * Mathf.Sin(Time.time * frequency);
            float y = amplitude * Mathf.Cos(Time.time * frequency);
            transform.localPosition = new Vector3(x, y, 0);
            LerpPlatformColor();
            return true;
        }
        else
        {
            return false;
        }
        
    }

    void LerpPlatformColor()
    {
        float lerpvalue = time/SecondsToShake;
        mesh.material.color = Color.Lerp(mesh.material.color, Color.red,lerpvalue);
    }

    void DestroyPlatform()
    {
        if(gameObject.transform.position.y<-10)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }



}
