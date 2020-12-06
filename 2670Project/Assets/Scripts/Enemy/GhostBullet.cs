using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GhostBullet : MonoBehaviour
{
    public float lifeTime = 3f;
    public float bulletSpeed = 10f;
    private Rigidbody rBody;
    public GameObject bullet;
    private UnityEngine.AI.NavMeshAgent agent;
    private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();

    private IEnumerator Start()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.AddRelativeForce(Vector3.forward * bulletSpeed);
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision) 
    {
        Destroy(gameObject);
    }  

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }   
}
