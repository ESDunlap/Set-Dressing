using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBehavior : MonoBehaviour
{
    private EliteEnemyBehavior _elite;

    void Start()
    {
        _elite = GameObject.Find("Elite Enemy").GetComponent<EliteEnemyBehavior>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            _elite.visionSight = true;
            Debug.Log("THE TOWER HAS DETECTED YOU ELITE SENT");
        }
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player lost. Return to Regular patrol");
        }
    }
}
