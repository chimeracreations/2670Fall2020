using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SentinelMovement : MonoBehaviour
{
    public GameObject enemy;
    private NavMeshAgent agent;
    public List<Vector3> patrolPoints;
    private float patrolSpeed;
    public PlayerData player;
    private WaitForSeconds wfs = new WaitForSeconds(1.5f);
    private bool detected;
    public EnemyData data;
    public GameObject exclamation;
    private bool triggerOnce;
    [SerializeField] private UnityEvent callEvent;
 

    private void Start()
    {
        triggerOnce = false;
        patrolSpeed = data.enemyPatrolSpeed;
        agent = enemy.GetComponent<NavMeshAgent>();
    }

    private void OnEnable() 
    {
        exclamation.SetActive(false);
    }
    
    private int i = 0;
    private void Update() 
    {
        if (detected == false)
        {
            agent.speed = patrolSpeed;
            if (agent.pathPending || !(agent.remainingDistance < 2f)) return;
            agent.destination = patrolPoints[i];
            i = (i + 1) % patrolPoints.Count;
            agent.isStopped = false;
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && triggerOnce == false)
        {
            triggerOnce = true;
            detected = true;
            player.canControl = false;
            agent.isStopped = true;
            exclamation.SetActive(true);
            yield return wfs;
            callEvent?.Invoke();
            exclamation.SetActive(false);
            triggerOnce = false;
            agent.isStopped = false;
            agent.ResetPath();
            detected = false;
        }
    }

}