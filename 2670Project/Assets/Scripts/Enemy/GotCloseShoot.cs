using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotCloseShoot : MonoBehaviour
{
    private ShootEnemyMovement enemyFarCollider;


    // Start is called before the first frame update
    void Start()
    {
        enemyFarCollider = transform.parent.GetComponent<ShootEnemyMovement>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        enemyFarCollider.canHunt = true;
    }


}
