using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject enemy;
    private NavMeshAgent agent;
    private GameObject character;
    public bool canHunt;
    public List<Vector3> patrolPoints;
    public float patrolSpeed = 2f;
    public float huntSpeed = 3.5f;
    private CharacterMover playerMovement;
    public PlayerData player;
    private bool detectNoise;
 

    private void Start()
    {
        character = GameObject.FindWithTag("Player");
        playerMovement = character.GetComponent<CharacterMover>();
        agent = enemy.GetComponent<NavMeshAgent>();
    }
    
    private int i = 0;
    private void Update() 
    {
        detectNoise = player.madeNoise;
        if (canHunt == false || playerMovement.enabled == false)
        {
            agent.speed = patrolSpeed;
            if (agent.pathPending || !(agent.remainingDistance < 2f)) return;
            agent.destination = patrolPoints[i];
            i = (i + 1) % patrolPoints.Count;
        }
        else if (canHunt == true)
        {
            agent.speed = huntSpeed;
            agent.destination = character.transform.position;
        }       
    }
    

    private void OnTriggerStay(Collider other)
    {
        if ((detectNoise && other.tag == "Player") || other.tag == "TailStink")
        canHunt = true;
    }
    private void OnTriggerExit(Collider other)
    {
        canHunt = false;
        agent.ResetPath();
    }
}