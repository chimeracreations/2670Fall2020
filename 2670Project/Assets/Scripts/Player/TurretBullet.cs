using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TurretBullet : MonoBehaviour
{
    public float lifeTime = 3f;
    public float bulletSpeed = 10f;
    private Rigidbody rBody;
    private GameObject enemy;
    private GameObject player;
    public GameObject bullet;
    private UnityEngine.AI.NavMeshAgent agent;
    private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    public float knockbackDuration = 0.25f;
    private Vector3 pushDirection;
    public float knockbackSpeed = 15f;

    private IEnumerator Start()
    {
        enemy = GameObject.FindWithTag("Enemy");
        player = GameObject.FindWithTag("Player");
        agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
        rBody = GetComponent<Rigidbody>();
        rBody.AddRelativeForce(Vector3.forward * bulletSpeed);
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    IEnumerator OnCollisionEnter(Collision collision) 
    {
        bullet.GetComponent<Renderer>().enabled = false;
        rBody.isKinematic = true;
        bullet.GetComponent<Collider>().enabled = false;

        if(collision.gameObject.tag == "Enemy")
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
            Destroy(gameObject);
        }
    }      
}
