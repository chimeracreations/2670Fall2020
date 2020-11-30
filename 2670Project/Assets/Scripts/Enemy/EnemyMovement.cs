﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyObject;
    private NavMeshAgent agent;
    private GameObject character;
    public bool canHunt;
    public List<Vector3> patrolPoints;
    public float patrolSpeed = 2f;
    public float huntSpeed = 3.5f;
    public PlayerData player;
    private bool detectNoise;
    public GameObject exclamation;
    public GameObject question;
    private WaitForSeconds wfs = new WaitForSeconds(1.5f);
    private bool wasShocked;
    private float count;
    private bool detected;
 

    private void Start()
    {
        character = GameObject.FindWithTag("Player");
        agent = enemy.GetComponent<NavMeshAgent>();
    }

    private void OnEnable() 
    {
        canHunt = false;
        enemyObject.SetActive(true);
        wasShocked = false;
    }
    
    private int i = 0;
    private void Update() 
    {
        detectNoise = player.madeNoise;
        if (canHunt == false || player.canControl == false || enemyObject.activeSelf == false)
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
        if (((detectNoise && other.tag == "Player") || other.tag == "TailStink") && enemyObject.activeSelf == true)
        {
            if (!wasShocked)
            {  
                detected = true;
                count += Time.deltaTime;
                exclamation.SetActive(true);
                agent.isStopped = true;
                if (count >= 2.5f)
                {
                    agent.isStopped = false;
                    exclamation.SetActive(false);
                    canHunt = true;
                    wasShocked = true;
                    count = 0f;
                    detected = false;
                }
            }
            else
            {
              canHunt = true;  
            }
        }

        if (detected && !wasShocked)
            {       
                count += Time.deltaTime;
                exclamation.SetActive(true);
                agent.isStopped = true;
                if (count >= 2.5f)
                {
                    agent.isStopped = false;
                    exclamation.SetActive(false);
                    canHunt = true;
                    wasShocked = true;
                    count = 0f;
                    detected = false;
                }
            }
    }

    private IEnumerator OnTriggerExit(Collider other)
    {   
        if (enemyObject.activeSelf == true && other.tag == "Player" && wasShocked == true)
        {
            exclamation.SetActive(false);
            canHunt = false;
            agent.isStopped = true;
            question.SetActive(true);
            yield return wfs;
            question.SetActive(false);
            agent.isStopped = false;
            agent.ResetPath();
            wasShocked = false;
            detected = false;
        }
    }


}