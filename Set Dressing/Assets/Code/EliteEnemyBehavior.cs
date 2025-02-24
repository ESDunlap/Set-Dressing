using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EliteEnemyBehavior : MonoBehaviour
{
    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations;
    public int locationIndex = 0;
    public bool visionSight = false;
    private NavMeshAgent agent;
    private bool continueChase = false;

    private int _lives = 9;
    public int EnemyLives
    {

        get
        {
            return _lives;
        }

        private set
        {
            _lives = value;

            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }
    void Update()
    {
        if (visionSight && !agent.pathPending)
        {
            agent.destination = player.position;
            visionSight = false;
        }
        if (agent.remainingDistance < 0.2f && !agent.pathPending && !continueChase)
        {
            MoveToNextPatrolLocation();
        }
        if (continueChase && !agent.pathPending)
        {
            agent.destination = player.position;
        }
    }

    void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }
    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position;
            continueChase = true;
            Debug.Log("Player Detected - attack");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            agent.destination = player.position;
            Debug.Log("Critical hit!");
        }
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }
}
