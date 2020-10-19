using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject enemy;
    private NavMeshAgent agent;
    private GameObject player;
    public bool canHunt;
    public List<Vector3> patrolPoints;
    public float patrolSpeed = 2f;
    public float huntSpeed = 3.5f;
    private CharacterMover playerMovement;
 

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = enemy.GetComponent<NavMeshAgent>();
        playerMovement = player.GetComponent<CharacterMover>();
    }
    
    private int i = 0;
    private void Update() 
    {
        if (canHunt == false)
        {
            agent.speed = patrolSpeed;
            if (agent.pathPending || !(agent.remainingDistance < 2f)) return;
            agent.destination = patrolPoints[i];
            i = (i + 1) % patrolPoints.Count;
        }
        else if (canHunt == true)
        {
            agent.speed = huntSpeed;
            agent.destination = player.transform.position;
        }       
    }
    

    private void OnTriggerStay(Collider other)
    {
        if (playerMovement.madeNoise && other.tag == "Player")
        canHunt = true;
    }
    private void OnTriggerExit(Collider other)
    {
        canHunt = false;
        agent.ResetPath();
    }
}