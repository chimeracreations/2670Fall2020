using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTailAttack : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    private UnityEngine.AI.NavMeshAgent agent;
    private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    public float knockbackDuration = 0.25f;
    private Vector3 pushDirection;
    public float knockbackSpeed = 15f; 

    private void Start() 
    {
        enemy = GameObject.FindWithTag("Enemy");
        player = GameObject.FindWithTag("Player");
        agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private IEnumerator OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Enemy") //&& isKnockbacked == false)
        {
            pushDirection = new Vector3(0,0,-1);
            pushDirection = player.transform.position - agent.transform.position;
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
