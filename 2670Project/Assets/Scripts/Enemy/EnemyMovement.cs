using System;
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
    private float patrolSpeed;
    private float huntSpeed;
    public PlayerData player;
    private bool detectNoise;
    public GameObject exclamation;
    public GameObject question;
    private WaitForSeconds wfs = new WaitForSeconds(1.5f);
    private bool wasShocked;
    private float count;
    private bool detected;
    public bool dontSetActiveOnEnable;
    public EnemyData data;
 

    private void Start()
    {
        huntSpeed = data.enemySpeed;
        patrolSpeed = data.enemyPatrolSpeed;
        character = GameObject.FindWithTag("Player");
        agent = enemy.GetComponent<NavMeshAgent>();
    }

    private void OnEnable() 
    {
        canHunt = false;
        if (!dontSetActiveOnEnable) enemyObject.SetActive(true);
        wasShocked = false;
    }
    
    private int i = 0;
    private void Update() 
    {
        detectNoise = player.madeNoise;
        float dist = Vector3.Distance(character.transform.position, transform.position);
        if (dist > 20f && wasShocked == true)
        {
            StartCoroutine(StopHunt());
        }

        if (canHunt == false || player.canControl == false || enemyObject.activeSelf == false)
        {
            agent.speed = patrolSpeed;
            if (agent.pathPending || !(agent.remainingDistance < 2f)) return;
            agent.destination = patrolPoints[i];
            i = (i + 1) % patrolPoints.Count;
            agent.isStopped = false;
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
                if (count >= 3f)
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
                if (count >= 3f)
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

    private IEnumerator StopHunt()
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