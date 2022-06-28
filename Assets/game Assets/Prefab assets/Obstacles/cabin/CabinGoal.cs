using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CabinGoal : MonoBehaviour
{
    public GameObject Camper;
    public TextMeshProUGUI textMeshPro;
   
    GameManager gameManager;

    void Start()
    {
        textMeshPro.enabled = false;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textMeshPro.enabled = true;
            gameManager.DecreaseAmountOfLevelsToPlay();
            gameManager.Invoke("LoadNextlevel",2);
            Debug.Log("Player finished level");
        }
        
    }
}
