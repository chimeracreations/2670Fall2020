using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TurretBullet : MonoBehaviour
{
    public float lifeTime = 3f;
    public float bulletSpeed = 10f;
    public PlayerData player;
    private Rigidbody rBody;
    public GameObject bullet;
    private UnityEngine.AI.NavMeshAgent agent;
    private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();

    private IEnumerator Start()
    {
        player.madeNoise = true;
        rBody = GetComponent<Rigidbody>();
        rBody.AddRelativeForce(Vector3.forward * bulletSpeed);
        yield return new WaitForSeconds(lifeTime);
        player.madeNoise = false;
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision) 
    {
        player.madeNoise = false;
        Destroy(gameObject);
    }      
}
