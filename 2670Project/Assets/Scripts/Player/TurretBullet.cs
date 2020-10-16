using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TurretBullet : MonoBehaviour
{
    public float lifeTime = 3f;
    public float bulletSpeed = 10f;
    private Rigidbody rBody;

    void OnCollisionEnter(Collision collision) 
    {
        Destroy(gameObject);
    }   
    private IEnumerator Start()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.AddRelativeForce(Vector3.forward * bulletSpeed);
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }    
}
