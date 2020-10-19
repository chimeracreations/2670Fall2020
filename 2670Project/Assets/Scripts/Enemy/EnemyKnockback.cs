using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    public GameObject enemy;
    private GameObject player;
    private UnityEngine.AI.NavMeshAgent agent;
    private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    public float knockbackDuration = 0.25f;
    private Vector3 pushDirection;
    public float knockbackSpeed = 15f; 

    private void Start() 
    {
        agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }

    private IEnumerator OnTriggerEnter(Collider other) 
    {
        if (other.tag == "TailStink")
        {
            agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
            pushDirection = new Vector3(0,0,-1);
            pushDirection = player.transform.position - enemy.transform.position;
            pushDirection =- pushDirection.normalized;
            pushDirection.y = 0;
            float i = 0;  

            while (i <= knockbackDuration)
            {
                yield return wffu;
                i += (1f * Time.deltaTime);
                agent.Move(pushDirection * Time.deltaTime * knockbackSpeed);
            }
        }
    }
}