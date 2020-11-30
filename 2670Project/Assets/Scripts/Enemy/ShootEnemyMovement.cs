using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootEnemyMovement : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyObject;
    private NavMeshAgent agent;
    private GameObject character;
    public bool canHunt;
    public List<Vector3> patrolPoints;
    public float patrolSpeed = 2f;
    public float huntSpeed = 3.5f;
    public float runAwayDistance = 5f;
    public PlayerData player;
    private bool detectNoise;
    public GameObject exclamation;
    public GameObject question;
    private WaitForSeconds wfs = new WaitForSeconds(1.5f);
    private WaitForSeconds wfshoot = new WaitForSeconds(0.5f);
    private bool wasShocked;
    private float count;
    private bool detected;
    private bool canMove;
    private bool canShoot;
    public GameObject instancer;
    public GameObject bullet;
 

    private void Start()
    {
        character = GameObject.FindWithTag("Player");
        agent = enemy.GetComponent<NavMeshAgent>();
    }

    private void OnEnable() 
    {
        canShoot = true;
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
            StopCoroutine(Shoot());
            agent.speed = patrolSpeed;
            if (agent.pathPending || !(agent.remainingDistance < 2f)) return;
            agent.destination = patrolPoints[i];
            i = (i + 1) % patrolPoints.Count;
        }
        else if (canHunt == true)
        {
            agent.speed = huntSpeed;
            float distance = Vector3.Distance(transform.position, character.transform.position);
            // if (canMove)
            // {
            //     if (distance < runAwayDistance)
            //     {
            //         Vector3 dirToPlayer = transform.position - character.transform.position;
            //         Vector3 newPos = transform.position + dirToPlayer;
            //         agent.SetDestination(newPos);
            //     }
            //     else
            //     {
                    agent.destination = character.transform.position;
                    if (canShoot)
                    {
                        StartCoroutine(Shoot());
                    }
                //}
            //}
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

    private IEnumerator Shoot()
    {
        canShoot = false;
        yield return wfs;
        yield return wfs;
        agent.isStopped = true;
        yield return wfshoot;
        var thisBullet = Instantiate(bullet, instancer.transform.position, instancer.transform.rotation);
        thisBullet.transform.parent =  enemy.transform;
        yield return wfshoot;
        agent.isStopped = false;
        agent.ResetPath();
        float distance = Vector3.Distance(transform.position, character.transform.position);
            if (canMove)
            {
                if (distance < runAwayDistance)
                {
                    Vector3 dirToPlayer = transform.position - character.transform.position;
                    Vector3 newPos = transform.position + dirToPlayer;
                    agent.SetDestination(newPos);
                }
                else
                {
                    agent.destination = character.transform.position;
                }
            }
        canShoot = true;
    }
}