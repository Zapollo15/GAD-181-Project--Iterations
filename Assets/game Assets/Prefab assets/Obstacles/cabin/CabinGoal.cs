using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CabinGoal : MonoBehaviour
{
    public GameObject Camper;
    public TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro.enabled = false;
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textMeshPro.enabled = true;
            Debug.Log("Player finished level");
        }
        
    }
}
